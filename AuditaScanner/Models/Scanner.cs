namespace AuditaScanner.Models;

internal class Scanner
{
    const string WIA_SCAN_COLOR_MODE = "6146";
    const string WIA_HORIZONTAL_SCAN_RESOLUTION_DPI = "6147";
    const string WIA_VERTICAL_SCAN_RESOLUTION_DPI = "6148";
    const string WIA_HORIZONTAL_SCAN_START_PIXEL = "6149";
    const string WIA_VERTICAL_SCAN_START_PIXEL = "6150";
    const string WIA_HORIZONTAL_SCAN_SIZE_PIXELS = "6151";
    const string WIA_VERTICAL_SCAN_SIZE_PIXELS = "6152";
    const string WIA_SCAN_BRIGHTNESS_PERCENTS = "6154";
    const string WIA_SCAN_CONTRAST_PERCENTS = "6155";

    private readonly DeviceInfo _deviceInfo;

    private int resolucao = 300;
    private int larguraPixel = 2480;
    private int alturaPixel = 3508;
    private int tipoCor = 1;
    public Scanner(DeviceInfo deviceInfo)
    {
        this._deviceInfo = deviceInfo;
    }

    public ImageFile ScanearDocumento(string formatoImagem)
    {
        var device = this._deviceInfo.Connect();
        CommonDialogClass commonDialog = new CommonDialogClass();
        var item = device.Items[1];

        try
        {
            configurarScanner(item, resolucao, 0, 0, larguraPixel, alturaPixel, 0, 0, tipoCor);
            object resultadoScan = commonDialog.ShowTransfer(item, formatoImagem, true);

            if (resultadoScan != null)
            {
                return (ImageFile)resultadoScan;
            }
        }
        catch (COMException e)
        {
            Console.WriteLine(e.ToString());
            uint codigoErro = (uint)e.ErrorCode;
            if (codigoErro == 0x80210006)
            {
                MessageBox.Show("O scanner está ocupado");
            }
            else if (codigoErro == 0x80210064)
            {
                MessageBox.Show("O scaneamento foi cancelado");
            }
            else
            {
                MessageBox.Show("Um erro inesperado aconteceu, chame um especialista", "Error", MessageBoxButtons.OK);
            }
        }

        return new ImageFile();
    }

    private void propriedadesWia(IProperties propriedades, object nomeProp, object valorProp)
    {
        Property prop = propriedades.get_Item(ref nomeProp);

        try
        {
            prop.set_Value(ref valorProp);
        }
        catch
        {
            if (nomeProp.ToString() == WIA_HORIZONTAL_SCAN_RESOLUTION_DPI || nomeProp.ToString() == WIA_VERTICAL_SCAN_RESOLUTION_DPI)
            {
                foreach (object t in prop.SubTypeValues)
                {
                    prop.set_Value(t);
                }
            }
        }

    }

    private void configurarScanner(IItem scannnerItem, int scanResolutionDPI, int scanStartLeftPixel, int scanStartTopPixel, int scanWidthPixels, int scanHeightPixels, int brightnessPercents, int contrastPercents, int colorMode)
    {
        propriedadesWia(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);
        propriedadesWia(scannnerItem.Properties, WIA_VERTICAL_SCAN_RESOLUTION_DPI, scanResolutionDPI);
        propriedadesWia(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_START_PIXEL, scanStartLeftPixel);
        propriedadesWia(scannnerItem.Properties, WIA_VERTICAL_SCAN_START_PIXEL, scanStartTopPixel);
        propriedadesWia(scannnerItem.Properties, WIA_HORIZONTAL_SCAN_SIZE_PIXELS, scanWidthPixels);
        propriedadesWia(scannnerItem.Properties, WIA_VERTICAL_SCAN_SIZE_PIXELS, scanHeightPixels);
        propriedadesWia(scannnerItem.Properties, WIA_SCAN_BRIGHTNESS_PERCENTS, brightnessPercents);
        propriedadesWia(scannnerItem.Properties, WIA_SCAN_CONTRAST_PERCENTS, contrastPercents);
        propriedadesWia(scannnerItem.Properties, WIA_SCAN_COLOR_MODE, colorMode);
    }

    public override string ToString()
    {
        return (string)this._deviceInfo.Properties["Name"].get_Value();
    }
}
