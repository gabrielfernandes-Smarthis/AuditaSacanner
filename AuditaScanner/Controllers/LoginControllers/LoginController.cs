using AuditaScanner.Models;
using Refit;
using System.Net.Http.Json;
using System.Security.Cryptography;

namespace AuditaScanner.Controllers.LoginControllers;

public class LoginController : ILoginController
{
    private readonly HttpClient _httpClient;

    public LoginController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TResponse> LoginAsync<TResponse>(string request)
    {
        try
        {
            var content = new StringContent(request, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(Constants.LoginUrl, content);
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Erro na resposta HTTP: " + e.Message);
            }

            return await response.Content.ReadFromJsonAsync<TResponse>();
        }
        catch (Exception e)
        {
            throw new Exception("Erro ao efetuar o login" + e.Message);
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

    public bool ValidarCpf(string cpf)
    {
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string tempCpf;
        string digito;
        int soma;
        int resto;

        cpf = cpf.Trim();
        cpf = cpf.Replace(".", "").Replace("-", "");

        if (cpf.Length != 11)
            return false;

        tempCpf = cpf.Substring(0, 9);
        soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = resto.ToString();

        tempCpf = tempCpf + digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return cpf.EndsWith(digito);
    }

    public bool ValidarCnpj(string cnpj)
    {
        int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int soma;
        int resto;
        string digito;
        string tempCnpj;

        cnpj = cnpj.Trim();
        cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

        if (cnpj.Length != 14)
            return false;

        tempCnpj = cnpj.Substring(0, 12);
        soma = 0;

        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

        resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = resto.ToString();

        tempCnpj = tempCnpj + digito;
        soma = 0;

        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

        resto = (soma % 11);
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return cnpj.EndsWith(digito);
    }
}
