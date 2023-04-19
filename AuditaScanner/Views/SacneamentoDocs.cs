namespace AuditaScanner.Views;

using AuditaScanner.Controllers.ScannerControllers;
using AuditaScanner.Controllers.TipoDocumentoControllers;
using AuditaScanner.Models;
using AuditaScanner.Models.TipoDocumentoModels;
using System.Net.Http.Headers;
using WIA;

public partial class SacneamentoDocs : Form
{
    public string CnpjPrestadora { get; set; }
    public int IdPrestadora { get; set; }
    public SacneamentoDocs()
    {
        InitializeComponent();

        numeroAtendimento.LostFocus += IdEntered;
        AddTipoDocComboBox();
    }

    private async void AddTipoDocComboBox()
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
        catch (Exception)
        {
            throw;
        }
    }

    private void ListarScanners()
    {
        listBox1.Items.Clear();
        var deviceManager = new DeviceManager();
        int count = deviceManager.DeviceInfos.Count;

        for (int i = 1; i <= count; i++)
        {
            if (deviceManager.DeviceInfos[i].Type == WiaDeviceType.ScannerDeviceType)
            {
                listBox1.Items.Add(
                    new Scanner(deviceManager.DeviceInfos[i])
                );
            }
        }
    }

    private void SacneamentoDocs_Load(object sender, EventArgs e)
    {
        ListarScanners();
    }

    public void Scanear()
    {
        Scanner device = null;

        this.Invoke(new MethodInvoker(delegate ()
        {
            device = listBox1.SelectedItem as Scanner;
            if (device == null)
            {
                MessageBox.Show("Selcione um scanner antes de começar a scanear",
                                "warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            else
            {
                ImageFile image = new ImageFile();
                string imageExtension = "";

                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        image = device.ScanearDocumento(WIA.FormatID.wiaFormatPNG);
                        imageExtension = ".png";
                        break;
                    case 1:
                        image = device.ScanearDocumento(WIA.FormatID.wiaFormatJPEG);
                        imageExtension = ".jpeg";
                        break;
                    case 2:
                        image = device.ScanearDocumento(WIA.FormatID.wiaFormatBMP);
                        imageExtension = ".bmp";
                        break;
                    case 3:
                        image = device.ScanearDocumento(WIA.FormatID.wiaFormatGIF);
                        imageExtension = ".gif";
                        break;
                    case 4:
                        image = device.ScanearDocumento(WIA.FormatID.wiaFormatTIFF);
                        imageExtension = ".tiff";
                        break;
                    case 5:
                        image = device.ScanearDocumento(WIA.FormatID.wiaFormatJPEG);
                        imageExtension = ".jpeg";
                        break;
                }
                string local = Path.Combine(localTemp.Text, GerarNomeArquivo() + imageExtension);
                PDFController pdf = new PDFController(localTemp.Text, GerarNomeArquivo());
                if (File.Exists(local))
                    File.Delete(local);

                image.SaveFile(local);
                pdf.GerarPDF(local);
                visualizarScan.SizeMode = PictureBoxSizeMode.StretchImage;
                visualizarScan.Image = Image.FromFile(local);
            }
        }));
    }

    private string GerarNomeArquivo()
    {
        string NomeArquivo = IdPrestadora + "-" + numeroAtendimento.Text;
        return NomeArquivo;
    }

    private void IdEntered(object sender, EventArgs e)
    {
        nomeArquivo.Text = GerarNomeArquivo();
    }

    private void btnNovoScan_Click(object sender, EventArgs e)
    {
        Task.Factory.StartNew(Scanear).ContinueWith(result => MessageBox.Show("Scan concluído"));
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
}
