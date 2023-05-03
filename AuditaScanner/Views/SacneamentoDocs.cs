namespace AuditaScanner.Views;

using AuditaScanner.Controllers.ScannerControllers;
using AuditaScanner.Controllers.TipoDocumentoControllers;
using AuditaScanner.Models.TipoDocumentoModels;
using System.Drawing.Imaging;
using System.Net.Http.Headers;
using System.Reflection;
using System.Windows.Forms;

public partial class SacneamentoDocs : Form
{
    public string CnpjPrestadora { get; set; }
    public int IdPrestadora { get; set; }
    private int _currentImage = 0;
    private int _currentPage = 0;
    private ImageFormat format = null;
    private string path = string.Empty;
    private string tempPath = string.Empty;

    ImageCodecInfo _tiffCodecInfo;
    TwainSession _twain;

    bool _stopScan;
    bool _loadingCaps;

    public SacneamentoDocs()
    {
        InitializeComponent();

        if (NTwain.PlatformInfo.Current.IsApp64Bit)
        {
            Text = Text + " (64bit)";
        }
        else
        {
            Text = Text + " (32bit)";
        }
        foreach (var enc in ImageCodecInfo.GetImageEncoders())
        {
            if (enc.MimeType == "image/tiff") { _tiffCodecInfo = enc; break; }
        }

        numeroAtendimento.LostFocus += IdEntered;
        AddTipoDocComboBox();

        SetupTwain();
        btnNovoScan.Enabled = false;

        vScrollBar1.Enabled = false;

        vScrollBar1.ValueChanged += vScrollBar1_ValueChanged;
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        SetupTwain();
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        if (_twain != null)
        {
            if (e.CloseReason == CloseReason.UserClosing && _twain.State > 4)
            {
                e.Cancel = true;
            }
            else
            {
                CleanupTwain();
            }
        }
        base.OnFormClosing(e);
    }

    private void SetupTwain()
    {
        var appId = TWIdentity.CreateFromAssembly(DataGroups.Image, Assembly.GetEntryAssembly());
        _twain = new TwainSession(appId);
        _twain.StateChanged += (s, e) =>
        {
            PlatformInfo.Current.Log.Info("State changed to " + _twain.State + " on thread " + Thread.CurrentThread.ManagedThreadId);
            if (_twain.State == 7)
                vScrollBar1.Enabled = true;
        };
        _twain.TransferError += (s, e) =>
        {
            PlatformInfo.Current.Log.Info("Got xfer error on thread " + Thread.CurrentThread.ManagedThreadId);
        };
        _twain.DataTransferred += (s, e) =>
        {
            PlatformInfo.Current.Log.Info("Transferred data event on thread " + Thread.CurrentThread.ManagedThreadId);

            // example on getting ext image info
            var infos = e.GetExtImageInfo(ExtendedImageInfo.Camera).Where(it => it.ReturnCode == ReturnCode.Success);
            foreach (var it in infos)
            {
                var values = it.ReadValues();
                PlatformInfo.Current.Log.Info(string.Format("{0} = {1}", it.InfoID, values.FirstOrDefault()));
                break;
            }

            // handle image data
            IList<Image> images = null;
            if (e.NativeData != IntPtr.Zero)
            {
                var stream = e.GetNativeImageStream();
                if (stream != null)
                {
                    images = new List<Image> { Image.FromStream(stream) };
                }
            }
            else if (!string.IsNullOrEmpty(e.FileDataPath))
            {
                images = new List<Image> { new Bitmap(e.FileDataPath) };
            }

            if (images != null)
            {
                this.BeginInvoke(new Action(() =>
                {
                    tempPath = localTemp.Text;

                    foreach (var img in images)
                    {
                        string fileName = GerarNomeArquivo(_currentPage % 2 == 0);
                        _currentPage++;

                        bool isPdf = false;
                        switch (comboBox1.SelectedIndex)
                        {
                            case 0:
                                format = ImageFormat.Png;
                                break;
                            case 1:
                                format = ImageFormat.Jpeg;
                                break;
                            case 2:
                                format = ImageFormat.Bmp;
                                break;
                            case 3:
                                format = ImageFormat.Gif;
                                break;
                            case 4:
                                format = ImageFormat.Tiff;
                                break;
                            case 5:
                                isPdf = true;
                                format = ImageFormat.Png;
                                break;
                            default:
                                break;
                        }
                        if (format == null && !isPdf)
                        {
                            MessageBox.Show("Formato de imagem não selecionado. O escaneamento será interrompido.", "Formato inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            _stopScan = true;
                            return;
                        }

                        path = Path.Combine($"{tempPath}\\{fileName}.{format.ToString().ToLower()}");
                        img.Save(path, format);

                        if (isPdf)
                        {
                            PDFController pdf = new(localTemp.Text, GerarNomeArquivo(_currentPage % 2 == 0));
                            pdf.GerarPDF(path);
                        }
                    }

                    if (visualizarScan.Image != null)
                    {
                        visualizarScan.Image.Dispose();
                        visualizarScan.Image = null;
                    }
                    visualizarScan.SizeMode = PictureBoxSizeMode.Zoom;
                    visualizarScan.Image = images.Last();
                }));
            }
        };
        _twain.SourceDisabled += (s, e) =>
        {
            PlatformInfo.Current.Log.Info("Source disabled event on thread " + Thread.CurrentThread.ManagedThreadId);
            this.BeginInvoke(new Action(() =>
            {
                btnNovoScan.Enabled = true;
                //LoadSourceCaps();
            }));
        };
        _twain.TransferReady += (s, e) =>
        {
            PlatformInfo.Current.Log.Info("Transferr ready event on thread " + Thread.CurrentThread.ManagedThreadId);
            e.CancelAll = _stopScan;
        };

        // either set sync context and don't worry about threads during events,
        // or don't and use control.invoke during the events yourself
        PlatformInfo.Current.Log.Info("Setup thread = " + Thread.CurrentThread.ManagedThreadId);
        _twain.SynchronizationContext = SynchronizationContext.Current;
        if (_twain.State < 3)
        {
            // use this for internal msg loop
            _twain.Open();
            // use this to hook into current app loop
            //_twain.Open(new WindowsFormsMessageLoopHook(this.Handle));
        }
    }

    private void CleanupTwain()
    {
        if (_twain.State == 4)
        {
            _twain.CurrentSource.Close();
        }
        if (_twain.State == 3)
        {
            _twain.Close();
        }

        if (_twain.State > 2)
        {
            // normal close down didn't work, do hard kill
            _twain.ForceStepDown(2);
        }
    }

    private void btnReload_Click(object sender, EventArgs e)
    {
        ListarScanners();
    }

    private void ListarScanners()
    {
        if (_twain.State >= 3)
        {
            listBox1.Items.Clear();

            foreach (var src in _twain)
            {
                listBox1.Items.Add(src.Name);
            }
        }
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (listBox1.SelectedIndex >= 0)
        {
            string selectedScannerName = listBox1.SelectedItem.ToString();
            SelectScannerByName(selectedScannerName);
        }
    }

    private void SelectScannerByName(string scannerName)
    {
        if (_twain.State > 4) { return; }

        if (_twain.State == 4) { _twain.CurrentSource.Close(); }

        var selectedSource = _twain.FirstOrDefault(src => src.Name == scannerName);

        if (selectedSource != null && selectedSource.Open() == ReturnCode.Success)
        {
            btnNovoScan.Enabled = true;
        }
    }
    private void SacneamentoDocs_Load(object sender, EventArgs e)
    {
        ListarScanners();
    }

    private void BtnNovoScan_Click(object sender, EventArgs e)
    {
        Scanear();
    }

    public void Scanear()
    {
        if (_twain.State == 4)
        {
            if (_twain.CurrentSource.Capabilities.CapDuplex.IsSupported && chkDuplex.Checked)
            {
                _twain.CurrentSource.Capabilities.CapDuplexEnabled.SetValue(BoolType.True);
            }
            _stopScan = false;

            if (_twain.CurrentSource.Capabilities.CapUIControllable.IsSupported)
            {
                // Ocultar a interface do usuário do scanner, se possível
                _twain.CurrentSource.Enable(SourceEnableMode.NoUI, false, this.Handle);
            }
            else
            {
                // Mostrar a interface do usuário do scanner
                _twain.CurrentSource.Enable(SourceEnableMode.ShowUI, true, this.Handle);
            }
        }
    }

    private async Task AddTipoDocComboBox()
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            //Add the bearer authorization header
            string bearerToken = Constants.TipoDocumentoToken;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            //Add the token
            httpClient.DefaultRequestHeaders.Add("token", Constants.TipoDocumentoToken);

            TipoDocumentoController tipoDocumento = new TipoDocumentoController(httpClient);
            List<TipoDocumentoModel> tipoDocumentosResponse = await tipoDocumento.GetTiposDocumentos<TipoDocumentoModel>();

            tipoDocCb.DataSource = tipoDocumentosResponse;
            tipoDocCb.DisplayMember = "Nome";
            tipoDocCb.ValueMember = "Id";
        }
        catch (HttpRequestException ex)
        {
            // Handle the exception related to HttpClient
            MessageBox.Show("Ocorreu um erro ao se comunicar com o servidor. Por favor, verifique sua conexão com a internet e tente novamente.");
        }
        catch (JsonException ex)
        {
            // Handle deserialization issues
            MessageBox.Show("Ocorreu um erro ao processar a resposta do servidor. Por favor, tente novamente.");
        }
        catch (Exception ex)
        {
            // Handle any other exception
            MessageBox.Show("Ocorreu um erro inesperado. Por favor, tente novamente.");
        }
    }

    private string GerarNomeArquivo()
    {
        string NomeArquivo = IdPrestadora + "-" + numeroAtendimento.Text;
        return NomeArquivo;
    }

    private string GerarNomeArquivo(bool isFront)
    {
        string NomeArquivo = IdPrestadora + "-" + numeroAtendimento.Text;
        if (chkDuplex.Checked)
        {
            if (isFront)
                NomeArquivo += "-Frente";
            else
                NomeArquivo += "-Verso";
        }
        return NomeArquivo;
    }

    private void IdEntered(object sender, EventArgs e)
    {
        nomeArquivo.Text = GerarNomeArquivo();
    }

    private void btnLocalTemp_Click(object sender, EventArgs e)
    {
        FolderBrowserDialog pastaTemp = new FolderBrowserDialog();
        pastaTemp.ShowNewFolderButton = true;
        DialogResult result = pastaTemp.ShowDialog();

        if (result == DialogResult.OK)
            localTemp.Text = pastaTemp.SelectedPath;
    }

    private void SacneamentoDocs_FormClosed(object sender, FormClosedEventArgs e)
    {
        Application.Exit();
    }

    private void vScrollBar1_ValueChanged(object sender, EventArgs e)
    {
        _currentImage++;

        string fileName = GerarNomeArquivo(_currentImage % 2 == 0);
        path = Path.Combine($"{tempPath}\\{fileName}.{format.ToString().ToLower()}");
        if (File.Exists(path))
        {
            try
            {
                using (Image imagem = Image.FromFile(path))
                {
                    visualizarScan.Image = new Bitmap(imagem);
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar a imagem: " + ex.Message);
            }
        }
        else
        {
            MessageBox.Show("Arquivo não encontrado");
        }
    }
}
