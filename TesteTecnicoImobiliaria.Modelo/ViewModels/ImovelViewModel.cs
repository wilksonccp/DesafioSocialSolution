namespace TesteTecnicoImobiliaria.Modelo.ViewModels
{
    public class ImovelViewModel
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string NomeDoCliente { get; set; }
        public int IdTipoImovel { get; set; }
        public string ValorImovel { get; set; }
        public string DataPublicacao { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
    }
}
