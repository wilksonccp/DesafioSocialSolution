using Dapper.Contrib.Extensions;

namespace TesteTecnicoImobiliaria.Modelo.Models
{
    [Table("CLIENTE")]
    public class ClienteModel
    {
        [Key]
        public int CD_CLIENTE { get; set; }
        public string NM_CLIENTE { get; set; }
        public string DS_EMAIL { get; set; }
        public string? NR_CPF { get; set; }
        public string? NR_CNPJ { get; set; }
        public bool FL_ATIVO { get; set; }
    }
}
