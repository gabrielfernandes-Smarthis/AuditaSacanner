using AuditaScanner.Controllers.LoginControllers;
using AuditaScanner.Controllers.PrestadoraControllers;
using AuditaScanner.Models.LoginModels;
using AuditaScanner.Models.PrestadoraModels;
using AuditaScanner.Views;
using System.Net.Http.Headers;

namespace AuditaScanner;

public partial class Login : Form
{
    public Login()
    {
        InitializeComponent();

        CenterControl(loginTxt);
        int X = loginTxt.Location.X;
        int Y = loginTxt.Location.Y;

        prestadorasCb.Location = new Point(X, Y + 70);
        senhaTxt.Location = new Point(X, Y + 140);

        LabelPosition(loginTxt, loginLabel);
        LabelPosition(senhaTxt, senhaLabel);
        LabelPosition(prestadorasCb, cnpjLabel);

        CenterX(auditaLabel);

        AddPrestadorasComboBox();
    }

    private async void AddPrestadorasComboBox()
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            //Add the bearer authorization header
            string bearerToken = Constants.PrestadoraToken;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            //Add the token
            httpClient.DefaultRequestHeaders.Add("token", Constants.PrestadoraToken);

            PrestadoraController prestadoras = new PrestadoraController(httpClient);
            List<PrestadoraModel> prestadoraResponse = await prestadoras.GetPrestadora<PrestadoraModel>();
            List<PrestadoraComboBoxItens> comboBoxItens = prestadoraResponse.Select(p => new PrestadoraComboBoxItens
            (
                p.Id,
                p.Fantasia,
                p.Cnpj
            )).ToList();

            prestadorasCb.DataSource = comboBoxItens;
            prestadorasCb.DisplayMember = "Fantasia";
            prestadorasCb.ValueMember = "Cnpj";
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void LabelPosition(Control control, Control controelLabel)
    {
        int x = control.Location.X;
        int y = control.Location.Y;
        controelLabel.Location = new Point(x, y - 25);
    }

    private void CenterControl(Control control)
    {
        int x = (this.ClientSize.Width - control.Width) / 2;
        int y = (this.ClientSize.Height - control.Height) / 2;
        control.Location = new Point(x, y - 20);
    }

    private void CenterX(Control control)
    {
        int x = (this.ClientSize.Width - control.Width) / 2;
        int y = control.Location.Y;
        control.Location = new Point(x, y);
    }

    private async void loginBtn_Click(object sender, EventArgs e)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            //Add the bearer authorization header
            string bearerToken = Constants.LoginBearerToken;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            //Add the token
            httpClient.DefaultRequestHeaders.Add("token", Constants.HeaderToken);

            LoginController login = new LoginController(httpClient);

            string cpf = loginTxt.Text.Replace(".", "").Replace("-", "");
            PrestadoraComboBoxItens slectedItem = (PrestadoraComboBoxItens)prestadorasCb.SelectedItem;
            string cnpj = slectedItem.Cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            //Check if the CPF or CNPJ is valid
            if (login.ValidarCpf(cpf) || login.ValidarCnpj(cnpj))
            {
                LoginRequest loginRequest = new LoginRequest
                {
                    Login = cpf,
                    Senha = login.GetMd5Hash(senhaTxt.Text),
                    Cnpj = cnpj
                };

                string LoginRequestedJson = JsonConvert.SerializeObject(loginRequest);

                //Attempt to login
                LoginModel loginModel = await login.LoginAsync<LoginModel>(LoginRequestedJson);
                //Check if the login was successful
                if (loginModel.Message == null)
                {
                    //If the login was successful, open the main form
                    Hide();
                    SacneamentoDocs sacneamentoDocs = new SacneamentoDocs();
                    sacneamentoDocs.CnpjPrestadora = cnpj;
                    sacneamentoDocs.IdPrestadora = slectedItem.Id;
                    sacneamentoDocs.Show();
                }
                else
                    MessageBox.Show(loginModel.Message);
            }
            else if (!login.ValidarCpf(cpf))
            {
                MessageBox.Show("Cpf invalido");
            }
            else
            {
                MessageBox.Show("Cnpj invalido");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erro ao realizar login" + ex.Message);
        }
    }
}