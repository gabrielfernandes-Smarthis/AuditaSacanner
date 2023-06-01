namespace AuditaScanner.Views;

using AuditaScanner.Controllers.PedidoExameControllers;
using AuditaScanner.Controllers.ScannerControllers;
using AuditaScanner.Controllers.TipoDocumentoControllers;
using AuditaScanner.Models.PedidosExameModels;
using AuditaScanner.Models.TipoDocumentoModels;
using AuditaScanner.Models.UploadModels;
using AuditaScanner.Services;
using System.Drawing.Imaging;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows.Forms;

public partial class SacneamentoDocs : Form
{
    public string UserCpf { get; set; }
    public string UserEmail { get; set; }
    public string UserName { get; set; }
    public string UserNickname { get; set; }
    public string UserPassword { get; set; }
    public string NomePaciente { get; set; }

    public string AppToken { get; set; }

    public List<string> DocNumber { get; set; }
    public List<string> DocCode { get; set; }

    public string FrentePath { get; set; }
    public string VersoPath { get; set; }
    public string PdfPath { get; set; }

    private int _currentImage = 0;
    private int _currentPage = 0;

    private ImageFormat format = null;

    private string path = string.Empty;
    private string tempPath = string.Empty;

    private bool isPdf = false;

    ImageCodecInfo _tiffCodecInfo;
    TwainSession _twain;

    bool _stopScan;

    private SemaphoreSlim _processingSemaphore = new SemaphoreSlim(1, 1);

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

        SetupTwain();
        btnNovoScan.Enabled = false;

        vScrollBar1.Enabled = false;

        localTemp.Text = "C:\\Temp\\AuditaScanner";

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
        _twain.DataTransferred += async (s, e) =>
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
                    await Task.Delay(500);
                    images = new List<Image> { Image.FromStream(stream) };
                }
            }
            else if (!string.IsNullOrEmpty(e.FileDataPath))
            {
                images = new List<Image> { new Bitmap(e.FileDataPath) };
            }

            if (images != null)
            {
                this.BeginInvoke(new Action(async () =>
                {
                    await _processingSemaphore.WaitAsync();
                    try
                    {
                        tempPath = localTemp.Text;

                        foreach (var img in images)
                        {
                            string fileName = await GerarNomeArquivoAsync(_currentPage % 2 == 0);
                            _currentPage++;

                            switch (comboBox1.SelectedIndex)
                            {
                                case 0:
                                    isPdf = true;
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
                            if (!Directory.Exists(tempPath))
                            {
                                Directory.CreateDirectory(tempPath);
                            }
                            path = Path.Combine($"{tempPath}\\{fileName}.{format.ToString().ToLower()}");
                            img.Save(path, format);
                        }

                        if (visualizarScan.Image != null)
                        {
                            visualizarScan.Image.Dispose();
                            visualizarScan.Image = null;
                        }
                        visualizarScan.SizeMode = PictureBoxSizeMode.Zoom;
                        visualizarScan.Image = images.Last();
                    }
                    finally
                    {
                        _processingSemaphore.Release();
                    }
                }));
            }
        };
        _twain.SourceDisabled += (s, e) =>
        {
            PlatformInfo.Current.Log.Info("Source disabled event on thread " + Thread.CurrentThread.ManagedThreadId);
            this.BeginInvoke(new Action(async () =>
            {
                if (chkDuplex.Checked)
                    await PdfDuplex();
                else
                    await PdfSimples();

                _currentPage = 0;
                btnNovoScan.Enabled = true;

                var extensao = Path.GetExtension(PdfPath);
                var nomeArquivo = Path.GetFileNameWithoutExtension(PdfPath);
                var base64 = FileToBase64(PdfPath);
                UploadBodyModel uploadBody = new UploadBodyModel
                {
                    Files = new List<FileModel>
                    {
                        new FileModel
                        {
                            Extension = extensao,
                            FileHash = base64,
                            Name = nomeArquivo,
                            PedidoExameId = Int32.Parse(numeroAtendimento.Text),
                            ProcedureId = null,
                            AtendimentoId = null,
                            FileTypeId = 1,
                            Paciente = NomePaciente
                        }
                    },
                    Sender = new SenderModel
                    {
                        Cpf = UserCpf,
                        Email = UserEmail,
                        Name = UserName,
                        Nickname = UserNickname,
                        Password = UserPassword
                    }
                };

                UploadArquivos upload = new UploadArquivos(uploadBody, AppToken);

                if (await upload.UploadArquivo())
                {
                    if (chkDuplex.Checked)
                        File.Delete(VersoPath);

                    File.Delete(FrentePath);
                    File.Delete(PdfPath);
                }
                else
                {
                    MessageBox.Show("Erro ao enviar arquivo para o servidor. Tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

    public string GetMd5Hash(string senha)
    {
        MD5 md5Hash = MD5.Create();
        // Converte a entrada para um array de bytes e calcula o hash.
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(senha));

        // Cria um novo Stringbuilder para coletar os bytes
        // e criar uma string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop por cada byte e formata como uma string hexadecimal
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Retorna o hash como uma string
        return sBuilder.ToString();
    }

    public string FileToBase64(string path)
    {
        byte[] bytes = File.ReadAllBytes(path);
        return Convert.ToBase64String(bytes);
    }

    private async Task PdfDuplex()
    {
        string front = await GerarNomeArquivoAsync(true);
        FrentePath = Path.Combine($"{tempPath}\\{front}.{format.ToString().ToLower()}");

        string back = await GerarNomeArquivoAsync(false);
        VersoPath = Path.Combine($"{tempPath}\\{back}.{format.ToString().ToLower()}");

        string fileName = await GerarNomeArquivo();
        PdfController pdf = new(tempPath, fileName);
        PdfPath = pdf.GerarPDF(FrentePath, VersoPath);
    }

    private async Task PdfSimples()
    {
        string front = await GerarNomeArquivoAsync(true);
        FrentePath = Path.Combine($"{tempPath}\\{front}.{format.ToString().ToLower()}");

        string fileName = await GerarNomeArquivo();
        PdfController pdf = new(tempPath, fileName);
        PdfPath = pdf.GerarPDF(FrentePath);
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
    private async void SacneamentoDocs_LoadAsync(object sender, EventArgs e)
    {
        await AddTipoDocComboBox();
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
                var ativou = _twain.CurrentSource.Capabilities.CapDuplexEnabled.GetCurrent();
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
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AppToken);
            //Add the token
            httpClient.DefaultRequestHeaders.Add("token", AppToken);

            TipoDocumentoController tipoDocumento = new TipoDocumentoController(httpClient);
            List<TipoDocumentoModel> tipoDocumentosResponse = await tipoDocumento.GetTiposDocumentos<TipoDocumentoModel>();

            tipoDocCb.DataSource = tipoDocumentosResponse;
            tipoDocCb.DisplayMember = "Nome";
            tipoDocCb.ValueMember = "CodigoDominio";
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

    private async Task<string> GerarNomeArquivo()
    {
        int LastNumber = await PedidoExameRequest();

        string NomeArquivo = $"{numeroAtendimento.Text}-{tipoDocCb.SelectedValue.ToString()}-{LastNumber + 1}";
        return NomeArquivo;
    }

    private async Task<int> PedidoExameRequest()
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            //Add the bearer authorization header
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AppToken);
            //Add the token
            httpClient.DefaultRequestHeaders.Add("token", AppToken);

            PedidoExameController pedidoExame = new PedidoExameController(httpClient);
            RetornoPedidoExame retornoPedidoExames = await pedidoExame.GetPedidosExames<RetornoPedidoExame>(Int32.Parse(numeroAtendimento.Text));

            if (retornoPedidoExames.TipoArquivo != null)
            {
                NomePaciente = retornoPedidoExames.PedidoExame.Registro.Paciente.Name;
                var arquivo = retornoPedidoExames.TipoArquivo.FirstOrDefault(a => a.CodeDominio == tipoDocCb.SelectedValue.ToString());
                if (arquivo != null)
                    return Int32.Parse(arquivo?.LastNumber);

                return 0;
            }
            else
            {
                return 0;
            }
        }
        catch (HttpRequestException ex)
        {
            // Handle the exception related to HttpClient
            MessageBox.Show("Ocorreu um erro ao se comunicar com o servidor. Por favor, verifique sua conexão com a internet e tente novamente.");
            return -1;
        }
        catch (JsonException ex)
        {
            // Handle deserialization issues
            MessageBox.Show("Ocorreu um erro ao processar a resposta do servidor. Por favor, tente novamente.");
            return -1;
        }
        catch (Exception ex)
        {
            // Handle any other exception
            MessageBox.Show("Ocorreu um erro inesperado. Por favor, tente novamente.");
            return -1;
        }
    }

    private async Task<string> GerarNomeArquivoAsync(bool isFront)
    {
        int LastNumber = await PedidoExameRequest();

        string NomeArquivo = $"{numeroAtendimento.Text}-{tipoDocCb.SelectedValue.ToString()}-{LastNumber + 1}";
        if (chkDuplex.Checked)
        {
            if (isFront)
                NomeArquivo += "-Frente";
            else
                NomeArquivo += "-Verso";
        }
        return NomeArquivo;
    }

    private async void IdEntered(object sender, EventArgs e)
    {
        nomeArquivo.Text = await GerarNomeArquivo();
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

    private async void vScrollBar1_ValueChanged(object sender, EventArgs e)
    {
        _currentImage++;

        string fileName = await GerarNomeArquivoAsync(_currentImage % 2 == 0);
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
