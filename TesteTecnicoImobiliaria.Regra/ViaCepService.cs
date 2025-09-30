using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace TesteTecnicoImobiliaria.Regra
{
    internal class ViaCepService : IViaCepService
    {
        private readonly HttpClient httpClient;

        public ViaCepService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ViaCepEndereco?> ObterEnderecoPorCepAsync(string cep, CancellationToken cancellationToken = default)
        {
            using var response = await httpClient.GetAsync($"/ws/{cep}/json/", cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            var endereco = await JsonSerializer.DeserializeAsync<ViaCepResponse>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }, cancellationToken);

            if (endereco == null || endereco.Erro)
            {
                return null;
            }

            return new ViaCepEndereco
            {
                Cep = endereco.Cep,
                Logradouro = endereco.Logradouro,
                Complemento = endereco.Complemento,
                Bairro = endereco.Bairro,
                Localidade = endereco.Localidade,
                Uf = endereco.Uf
            };
        }
    }

    internal class ViaCepEndereco
    {
        public string Cep { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Localidade { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;
    }

    internal class ViaCepResponse
    {
        public string Cep { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Localidade { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;
        public bool Erro { get; set; }
    }
}
