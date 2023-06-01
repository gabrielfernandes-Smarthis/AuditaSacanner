using AuditaScanner.Controllers.LoginControllers;
using AuditaScanner.Controllers.PrestadoraControllers;
using AuditaScanner.Models.LoginModels;
using AuditaScanner.Models.PrestadoraModels;
using AuditaScanner.Views;
using Microsoft.VisualBasic.Logging;
using System.Net.Http.Headers;

namespace AuditaScanner;

public partial class Login : Form
{
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Nickname { get; set; }
    public string Password { get; set; }

    public Login()
    {
        InitializeComponent();

        CenterControl(loginTxt);
        int X = loginTxt.Location.X;
        int Y = loginTxt.Location.Y;

        senhaTxt.Location = new Point(X, Y + 70);
        prestadorasCb.Location = new Point(X, Y + 140);

        LabelPosition(loginTxt, loginLabel);
        LabelPosition(prestadorasCb, prestadoraLabel);
        LabelPosition(senhaTxt, senhaLabel);

        CenterX(auditaLabel);

        prestadorasCb.Enabled = false;
        loginBtn.Enabled = false;

        senhaTxt.LostFocus += VerificarLogin;
    }

    private void AddPrestadorasComboBox(List<CompaniesModel> companies)
    {
        prestadorasCb.Enabled = true;

        try
        {
            foreach (var i in companies)
            {
                prestadorasCb.Items.Add(i);
            }
        }
        catch (Exception e)
        {
            MessageBox.Show("Erro ao adicionar prestadoras ao combobox" + e.Message);
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

    private async void VerificarLogin(object sender, EventArgs e)
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

            //Check if the CPF or CNPJ is valid
            if (login.ValidarCpf(cpf))
               await EfetuarLogin(cpf, login);
            else if (!login.ValidarCpf(cpf))
                MessageBox.Show("Cpf invalido");
            else
                MessageBox.Show("Cnpj invalido");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Erro ao realizar login" + ex.Message);
        }
    }

    private async Task EfetuarLogin(string cpf, LoginController login)
    {
        LoginRequest loginRequest = new LoginRequest
        {
            Login = cpf,
            Senha = login.GetMd5Hash(senhaTxt.Text),
        };

        string LoginRequestedJson = JsonConvert.SerializeObject(loginRequest);

        //Attempt to login
        LoginModel loginModel = await login.LoginAsync<LoginModel>(LoginRequestedJson);
        //Check if the login was successful
        if (loginModel.Message == null)
        {
            Cpf = loginModel.Cpf;
            UserName = loginModel.Name;
            Nickname = loginModel.Nickname;
            Password = loginModel.Password;

            List<CompaniesModel> companies = loginModel.Companies;
            AddPrestadorasComboBox(companies);
        }
        else
        {
            prestadorasCb.Enabled = false;
            prestadorasCb.Items.Clear();
            loginBtn.Enabled = false;
            MessageBox.Show(loginModel.Message);
        }
    }

    private void loginBtn_Click(object sender, EventArgs e)
    {
        Hide();
        SacneamentoDocs scaneamentoDocs = new SacneamentoDocs();
        scaneamentoDocs.UserCpf = Cpf;
        scaneamentoDocs.UserName = UserName;
        scaneamentoDocs.UserNickname = Nickname;
        scaneamentoDocs.UserPassword = Password;
        var selectedPrestadora = (CompaniesModel)prestadorasCb.SelectedItem;
        scaneamentoDocs.AppToken = selectedPrestadora.AppToken;
        scaneamentoDocs.Show();
    }

    private void Login_Load(object sender, EventArgs e)
    {
        loginTxt.Focus();
    }

    private void prestadorasCb_SelectedIndexChanged(object sender, EventArgs e)
    {
        loginBtn.Enabled = true;
    }
}