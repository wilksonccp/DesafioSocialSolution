namespace TesteTecnicoImobiliaria.Regra
{
    internal static class Helpers
    {
        public static string? Mascarar(this string? objValor, string mascara)
        {
            string valor = "";

            if (objValor == null)
            {
                return "";
            }

            valor = objValor.Trim();

            if (valor.Length == 0)
            {
                return "";
            }

            // Valor já contém máscara
            if (mascara.Length == valor.Length)
            {
                return valor;
            }

            string novoValor = string.Empty;
            int posicao = 0;

            for (int i = 0; mascara.Length > i; i++)
            {
                if (mascara[i] == '#')
                {
                    if (valor.Length > posicao)
                    {
                        novoValor = novoValor + valor[posicao];
                        posicao++;
                    }
                    else
                        break;
                }
                else
                {
                    if (valor.Length > posicao)
                        novoValor = novoValor + mascara[i];
                    else
                        break;
                }
            }
            return novoValor;
        }

        public static string? LimparMascara(this string? valor)
        {
            if (valor == null)
            {
                return null;
            }

            valor = valor.Replace("(", "");
            valor = valor.Replace(")", "");
            valor = valor.Replace("-", "");
            valor = valor.Replace("_", "");
            valor = valor.Replace(" ", "");
            valor = valor.Replace(".", "");
            valor = valor.Replace("/", "");
            valor = valor.Replace(":", "");

            return valor;
        }
    }
}
