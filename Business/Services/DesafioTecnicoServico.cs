﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text.Json;

namespace TesteTecnico.Business.Services
{
    public class DesafioTecnicoServico
    {
        private static string[] _arrayUnidade = new string[] { "", "Um", "Dois", "Três", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove" };
        private static string[] _arrayDezenaDez = new string[] { "Dez", "Onze", "Doze", "Treze", "Quatorze", "Quinze", "Dezesseis", "Dezessete", "Dezoito", "Dezenove" };
        private static string[] _arrayDezena = new string[] { "", "", "Vinte", "Trinta", "Quarenta", "Cinquenta", "Sessenta", "Setenta", "Oitenta", "Noventa" };
        private static string[] _arrayCentena = new string[] { "", "Cento", "Duzentos", "Trezentos", "Quatrocentos", "Quinhentos", "Seiscentos", "Setecentos", "Oitocentos", "Novecentos" };
        private static List<string> _listaOperadores = new List<string> { "/", "*", "-", "+" };

        public string GetNumeroPorExtenso(long numero)
        {
            if (numero < 0)
                throw new BadHttpRequestException("Número inválido.");
            else if (numero > 999999999999999)
                throw new BadHttpRequestException("Número Maior do que o suportado, ultrapassou a cada de trilhão. Max: 999.999.999.999.999");
            else if (numero == 0)
                return "Zero";

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
                            retorno += " Trilhão" + ((Convert.ToInt32(numeroString.Substring(3)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(numeroString.Substring(0, 3)) > 1)
                            retorno += " Trilhões" + ((Convert.ToInt32(numeroString.Substring(3)) > 0) ? " e " : string.Empty);
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
                            retorno += " Bilhão" + ((Convert.ToInt32(numeroString.Substring(3)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(numeroString.Substring(0, 3)) > 1)
                            retorno += " Bilhões" + ((Convert.ToInt32(numeroString.Substring(3)) > 0) ? " e " : string.Empty);
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
                            retorno += " Milhão" + ((Convert.ToInt32(numeroString.Substring(3)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(numeroString.Substring(0, 3)) > 1)
                            retorno += " Milhões" + ((Convert.ToInt32(numeroString.Substring(3)) > 0) ? " e " : string.Empty);
                    }
                    numeroString = numeroString.Remove(0, 3);
                }
                if (numeroString.Length > 3)
                {
                    int centenaMilhar = Convert.ToInt32(numeroString.Substring(0, 3));

                    if (centenaMilhar > 0)
                    {
                        retorno += GetCentenaPorExtenso(centenaMilhar);
                        retorno += " Mil" + ((Convert.ToInt32(numeroString.Substring(3)) > 0) ? " e " : string.Empty);
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

        public long GetSomaArrayInteiros(int[] arrayInteiros)
        {
            long retorno = 0;

            if(arrayInteiros.Length > 1000000) 
            {
                throw new BadHttpRequestException("Array maior do que um milhão");
            }

            //var listaInteiros = new List<int>(arrayInteiros);

            //Stopwatch watch = Stopwatch.StartNew();

            //foreach (int i in listaInteiros)
            //{
            //    retorno += i;
            //}

            //watch.Stop();
            //Console.WriteLine("List/foreach: {0}ms ({1})", watch.ElapsedMilliseconds, retorno);

            //retorno = 0;
            //watch = Stopwatch.StartNew();

            foreach (int i in arrayInteiros)
            {
                retorno += i;
            }

            //watch.Stop();
            //Console.WriteLine("Array/foreach: {0}ms ({1})", watch.ElapsedMilliseconds, retorno);

            return retorno;
        }

        public double GetResultadoExpressao(string expressao)
        {
            var arrayImputs = expressao.Split(' ').Select(x => x.Trim()).ToArray();
            var listaImputsOrdenados = new List<string>();
            var listaNumeros = new List<double>();
            var listaOperadores = new List<string>();

            //Cria Notação polonesa inversa
            for (int i = 0; i < arrayImputs.Length; i++)
            {
                if (int.TryParse(arrayImputs[i], out int o))
                {
                    listaImputsOrdenados.Add(arrayImputs[i]);
                }
                else if (_listaOperadores.Contains(arrayImputs[i]) || arrayImputs[i] == "%2F")
                {
                    if (arrayImputs[i] == "%2F")
                        arrayImputs[i] = "/";

                    if (listaOperadores.Count == 0)
                    {
                        listaOperadores.Add(arrayImputs[i]);
                    }
                    else
                    {
                        if(_listaOperadores.IndexOf(arrayImputs[i]) > _listaOperadores.IndexOf(listaOperadores.Last()))
                        {
                            listaImputsOrdenados.Add(listaOperadores.Last());
                            listaOperadores.RemoveAt(listaOperadores.Count - 1);
                        }

                        listaOperadores.Add(arrayImputs[i]);
                    }
                }
                else
                    throw new Exception("Expressão inválida, utilize números e os operadores(\"/\", \"*\", \"-\", \"+\") separados por espaço");
            }

            //Adiciona os Operadores faltantes
            while(listaOperadores.Count > 0)
            {
                listaImputsOrdenados.Add(listaOperadores.Last());
                listaOperadores.RemoveAt(listaOperadores.Count - 1);
            }

            //Executa a Notação polonesa inversa
            foreach (var item in listaImputsOrdenados)
            {
                if (int.TryParse(item, out int i))
                {
                    listaNumeros.Add(i);
                }
                else
                {
                    double valor2 = listaNumeros.LastOrDefault();
                    listaNumeros.RemoveAt(listaNumeros.Count - 1);

                    double valor1 = listaNumeros.LastOrDefault();
                    listaNumeros.RemoveAt(listaNumeros.Count - 1);

                    switch (item) 
                    {
                        case "+":
                            listaNumeros.Add(valor1 + valor2);
                            break;
                        case "-":
                            listaNumeros.Add(valor1 - valor2);
                            break;
                        case "*":
                            listaNumeros.Add(valor1 * valor2);
                            break;
                        case "/":
                            if(valor2 == 0)
                                throw new ArithmeticException("Divisão por Zero");
                            listaNumeros.Add(valor1 / valor2);
                            break;
                    }
                }
            }

            return listaNumeros.LastOrDefault();
        }

        public List<JsonElement> GetObjetosUnicos(List<JsonElement> listaObjetos)
        {
            return listaObjetos.DistinctBy(x => Convert.ToString(x) + x.ValueKind).ToList();
        }

        private string GetCentenaPorExtenso(int numero)
        {
            if (numero <= 0)
                return string.Empty;

            string retorno = string.Empty;

            string numeroString = Convert.ToString(numero);

            if (numeroString.Length == 3)
            {
                if (numeroString.Substring(0, 1) == "1")
                {
                    if(Convert.ToInt32(numeroString.Substring(1, 2)) > 0)
                    {
                        retorno += _arrayCentena[Convert.ToInt32(numeroString.Substring(0, 1))];
                        numeroString = numeroString.Remove(0, 1);
                    }
                    else
                    {
                        retorno += "Cem";
                        numeroString = numeroString.Remove(0, 1);

                    }
                }
                else
                {
                    retorno += _arrayCentena[Convert.ToInt32(numeroString.Substring(0, 1))];
                    numeroString = numeroString.Remove(0, 1);
                }
            }

            if (numeroString.Length == 2)
            {
                if (!string.IsNullOrWhiteSpace(retorno) && numeroString.Substring(0, 1) != "0")
                    retorno += " e ";

                if(numeroString.Substring(0, 1) == "1")
                {
                    retorno += _arrayDezenaDez[Convert.ToInt32(numeroString.Substring(1, 1))];
                    numeroString = string.Empty;
                }
                else
                {
                    retorno += _arrayDezena[Convert.ToInt32(numeroString.Substring(0, 1))];
                    numeroString = numeroString.Remove(0, 1);
                }
            }

            if (numeroString.Length == 1)
            {
                if (!string.IsNullOrWhiteSpace(retorno) && numeroString.Substring(0, 1) != "0")
                    retorno += " e ";

                retorno += _arrayUnidade[Convert.ToInt32(numeroString.Substring(0, 1))];
                numeroString = string.Empty;
            }

            return retorno;
        }
    }
}