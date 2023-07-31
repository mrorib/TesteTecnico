 namespace TesteTecnico.Business
{
    public class DesafioTecnicoServico
    {
        public string GetNumeroPorExtenso(int numero)
        {
            if (numero < 0)
                throw new BadHttpRequestException("Número inválido.");
            else if (numero = 0)
                return "zero";

            string retorno = string.Empty;

        }

        private string GetCentenaPorExtenso(int numero)
        {
            string retorno = string.Empty;
            
            string numeroString = Convert.ToString(numero);

            switch (numeroString.Length)
            {
                case 3:
                    {
                        switch (numeroString[0]) 
                        { 

                        }
                    }
                case 2:
                    {

                    }
                case 1:
                    {

                    }

            }
        }
    }
}
