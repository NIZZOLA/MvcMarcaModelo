using MvcMarcaModelo.Models;
using System.Text.Json;

namespace ImportaDados.IntegrationServices;

public class MarcasService
{
    public async Task<List<MarcasResponse>> GetMarcas()
    {
        using (HttpClient client = new HttpClient())
        {
            string apiUrl = "https://parallelum.com.br/fipe/api/v1/carros/marcas";
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<MarcasResponse> marcas = JsonSerializer.Deserialize<List<MarcasResponse>>(responseBody, options);

                    //foreach (MarcasResponse marca in marcas)
                    //{
                    //    Console.WriteLine($"MARCA --> Código: {marca.Codigo}, Nome: {marca.Nome}");
                    //  //  var modelos = await GetModelos(marca.Codigo);
                    //}
                    return marcas;
                }
                else
                {
                    Console.WriteLine($"Falha na solicitação: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao fazer a solicitação: {ex.Message}");
            }
        }
        return null;
    }

    public async Task<List<ModeloModel>> GetModelos(string codigoMarca, int idMarca)
    {
        var listaModelos = new List<ModeloModel>();
        using (HttpClient client = new HttpClient())
        {
            string apiUrl = $"https://parallelum.com.br/fipe/api/v1/carros/marcas/{codigoMarca}/modelos";
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var modelos = JsonSerializer.Deserialize<ModeloResponse>(responseBody, options);

                                        // Agora você pode acessar os dados desserializados
                    foreach (var modelo in modelos.Modelos)
                    {
                        Console.WriteLine($"Código: {modelo.Codigo}, Nome: {modelo.Nome}");
                        listaModelos.Add(new ModeloModel() { Nome = modelo.Nome, MarcaId = idMarca });
                    }
                    //foreach (ModeloResponse modelo in modelos)
                    //{
                    //    Console.WriteLine($"Código: {modelo.}, Nome: {modelo.Nome}");
                    //}
                }
                else
                {
                    Console.WriteLine($"Falha na solicitação: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao fazer a solicitação: {ex.Message}");
            }
        }
        return listaModelos;
    }

}
