using System.Linq;

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

            // Valor ja contem mascara
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
                    {
                        break;
                    }
                }
                else
                {
                    if (valor.Length > posicao)
                    {
                        novoValor = novoValor + mascara[i];
                    }
                    else
                    {
                        break;
                    }
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

        public static bool EhCpfValido(this string? valor)
        {
            var cpf = valor.LimparMascara();

            if (string.IsNullOrWhiteSpace(cpf))
            {
                return false;
            }

            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
            {
                return false;
            }

            if (cpf.Distinct().Count() == 1)
            {
                return false;
            }

            var numeros = cpf.Select(c => c - '0').ToArray();

            var soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += numeros[i] * (10 - i);
            }

            var resto = soma % 11;
            var primeiroDigito = resto < 2 ? 0 : 11 - resto;

            if (numeros[9] != primeiroDigito)
            {
                return false;
            }

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += numeros[i] * (11 - i);
            }

            resto = soma % 11;
            var segundoDigito = resto < 2 ? 0 : 11 - resto;

            if (numeros[10] != segundoDigito)
            {
                return false;
            }

            return true;
        }

        public static bool EhCnpjValido(this string? valor)
        {
            var cnpj = valor.LimparMascara();

            if (string.IsNullOrWhiteSpace(cnpj))
            {
                return false;
            }

            if (cnpj.Length != 14 || !cnpj.All(char.IsDigit))
            {
                return false;
            }

            if (cnpj.Distinct().Count() == 1)
            {
                return false;
            }

            var numeros = cnpj.Select(c => c - '0').ToArray();

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            var soma = 0;
            for (int i = 0; i < 12; i++)
            {
                soma += numeros[i] * multiplicador1[i];
            }

            var resto = soma % 11;
            var primeiroDigito = resto < 2 ? 0 : 11 - resto;

            if (numeros[12] != primeiroDigito)
            {
                return false;
            }

            soma = 0;
            for (int i = 0; i < 13; i++)
            {
                soma += numeros[i] * multiplicador2[i];
            }

            resto = soma % 11;
            var segundoDigito = resto < 2 ? 0 : 11 - resto;

            if (numeros[13] != segundoDigito)
            {
                return false;
            }

            return true;
        }
    }
}
