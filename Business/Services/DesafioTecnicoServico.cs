namespace TesteTecnico.Business.Services
{
    public class DesafioTecnicoServico
    {
        private static string[] arrayUnidade = new string[] { "", "Um", "Dois", "Três", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove" };
        private static string[] arrayDezenaDez = new string[] { "Dez", "Onze", "Doze", "Treze", "Quatorze", "Quinze", "Dezesseis", "Dezessete", "Dezoito", "Dezenove" };
        private static string[] arrayDezena = new string[] { "", "", "Vinte", "Trinta", "Quarenta", "Cinquenta", "Sessenta", "Setenta", "Oitenta", "Noventa" };
        private static string[] arrayCentena = new string[] { "", "Cem", "Duzentos", "Trezentos", "Quatrocentos", "Quinhentos", "Seiscentos", "Setecentos", "Oitocentos", "Novecentos" };

        public string GetNumeroPorExtenso(long numero)
        {
            if (numero < 0)
                throw new BadHttpRequestException("Número inválido.");
            else if (numero > 999999999999999)
                throw new BadHttpRequestException("Número Maior do que o suportado, ultrapassou a cada de trilhão. Max: 999.999.999.999.999");
            else if (numero == 0)
                return "zero";

            string retorno = string.Empty;

            string numeroString = numero.ToString("000000000000000");

            while (numeroString.Length > 0)
            {
                if(numeroString.Length > 12)
                {
                    int centenaTrilhao = Convert.ToInt32(numeroString.Substring(0, 3));

                    if(centenaTrilhao > 0)
                    {
                        retorno += GetCentenaPorExtenso(centenaTrilhao);

                        if (Convert.ToInt32(numeroString.Substring(0, 3)) == 1)
                            retorno += " trilhão" + ((Convert.ToInt32(numeroString.Substring(3)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(numeroString.Substring(0, 3)) > 1)
                            retorno += " trilhões" + ((Convert.ToInt32(numeroString.Substring(3)) > 0) ? " e " : string.Empty);
                    }
                    numeroString = numeroString.Remove(0, 3);
                }
                if (numeroString.Length > 9)
                {
                    int centenaBilhao = Convert.ToInt32(numeroString.Substring(0, 3));

                    if (centenaBilhao > 0)
                    {
                        retorno += GetCentenaPorExtenso(centenaBilhao);

                        if (Convert.ToInt32(numeroString.Substring(0, 3)) == 1)
                            retorno += " bilhão" + ((Convert.ToInt32(numeroString.Substring(3)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(numeroString.Substring(0, 3)) > 1)
                            retorno += " bilhões" + ((Convert.ToInt32(numeroString.Substring(3)) > 0) ? " e " : string.Empty);
                    }
                    numeroString = numeroString.Remove(0, 3);
                }
                if (numeroString.Length > 6)
                {
                    int centenaMilhao = Convert.ToInt32(numeroString.Substring(0, 3));

                    if (centenaMilhao > 0)
                    {
                        retorno += GetCentenaPorExtenso(centenaMilhao);

                        if (Convert.ToInt32(numeroString.Substring(0, 3)) == 1)
                            retorno += " milhão" + ((Convert.ToInt32(numeroString.Substring(3)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(numeroString.Substring(0, 3)) > 1)
                            retorno += " milhões" + ((Convert.ToInt32(numeroString.Substring(3)) > 0) ? " e " : string.Empty);
                    }
                    numeroString = numeroString.Remove(0, 3);
                }
                if (numeroString.Length > 3)
                {
                    int centenaMilhar = Convert.ToInt32(numeroString.Substring(0, 3));

                    if (centenaMilhar > 0)
                    {
                        retorno += GetCentenaPorExtenso(centenaMilhar);
                        retorno += " mil" + ((Convert.ToInt32(numeroString.Substring(3)) > 0) ? " e " : string.Empty);
                    }
                    numeroString = numeroString.Remove(0, 3);
                }
                
                int centena = Convert.ToInt32(numeroString.Substring(0, 3));

                if (centena > 0)
                {
                    retorno += GetCentenaPorExtenso(centena);
                }
                numeroString = string.Empty;
            }

            return retorno;
        }

        private string GetCentenaPorExtenso(int numero)
        {
            if (numero <= 0)
                return string.Empty;

            string retorno = string.Empty;

            string numeroString = Convert.ToString(numero);

            if (numeroString.Length == 3)
            {
                retorno += arrayCentena[Convert.ToInt32(numeroString.Substring(0, 1))];
                numeroString = numeroString.Remove(0, 1);
            }

            if (numeroString.Length == 2)
            {
                if (!string.IsNullOrWhiteSpace(retorno) && numeroString.Substring(0, 1) != "0")
                    retorno += " e ";

                if(numeroString.Substring(0, 1) == "1")
                {
                    retorno += arrayDezenaDez[Convert.ToInt32(numeroString.Substring(1, 1))];
                    numeroString = string.Empty;
                }
                else
                {
                    retorno += arrayDezena[Convert.ToInt32(numeroString.Substring(0, 1))];
                    numeroString = numeroString.Remove(0, 1);
                }
            }

            if (numeroString.Length == 1)
            {
                if (!string.IsNullOrWhiteSpace(retorno) && numeroString.Substring(0, 1) != "0")
                    retorno += " e ";

                retorno += arrayUnidade[Convert.ToInt32(numeroString.Substring(0, 1))];
                numeroString = string.Empty;
            }

            return retorno;
        }
    }
}