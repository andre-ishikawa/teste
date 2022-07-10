using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using ServicoCotacaoMoeda.Models;
using System;
using System.Net;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using CsvHelper;
using System.IO;
using ServicoCotacaoMoeda.Util;

namespace ServicoCotacaoMoeda
{
    class Program
    {
        static void Main()
        {
            Timer _timer = null;
            _timer = new Timer(TimerCallback, null, 0, 120000);
            Console.ReadLine();
        }

        private static void TimerCallback(object state)
        {
            Console.WriteLine("In TimerCallback: " + DateTime.Now);
       
            try
            { 
                System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                var client = new RestClient("https://localhost:44320/");
                var request = new RestRequest("/getItemFila", Method.Get);
                request.AddHeader("accept", "application/json");
                request.AddHeader("Content-Type", "application/json");
                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var retorno = response.Content.Substring(1);
                    retorno = retorno.Substring(0, (retorno.Length - 1)).Replace("\\","");
                    
                    var itemFila = new ItemFila();
                    itemFila = JsonConvert.DeserializeObject<ItemFila>(retorno);

                    var dadosMoeda = ReadMoedasCSV();
                    var dadosMoedaList = new List<DadosMoeda>();
                    dadosMoedaList = dadosMoeda.Where(x => x.DATA_REF >= itemFila.Data_Inicio && x.DATA_REF <= itemFila.Data_Fim).ToList();

                    if (dadosMoeda.Count() > 0)
                    {
                        var dadosCotacao = ReadCotacaoCSV();
                        
                        List<NewDadosMoeda> newDadosMoedas = dadosMoedaList.Select(x => new NewDadosMoeda
                        {
                            ID_MOEDA = x.ID_MOEDA,
                            DATA_REF = x.DATA_REF,
                            COD_MOEDA = (int)RetornaID.GetEnumValue(x.ID_MOEDA)
                        }).ToList();

                        var listDadosCotacao = new List<DadosCotacao>();
                        listDadosCotacao = dadosCotacao.Where(x => x.cod_cotacao == newDadosMoedas.Select(y => y.COD_MOEDA).FirstOrDefault()).ToList();

                        var result = newDadosMoedas.Select(x => new 
                        {
                            ID_MOEDA = x.ID_MOEDA,
                            DATA_REF = x.DATA_REF,
                            VLR_COTACAO = listDadosCotacao.Where(
                                y => y.cod_cotacao == x.COD_MOEDA
                            ).FirstOrDefault() != null ? listDadosCotacao.Where(
                                y => y.cod_cotacao == x.COD_MOEDA
                            ).FirstOrDefault().vlr_cotacao : 0
                        }).ToList();

                        var diretorio = @"C:\ProjetoEstudo\testeAPI\ItemFila\Resultado_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";

                        //var headers = new[] { "ID_MOEDA", "DATA_REF", "VLR_COTACAO" }.ToList();
                        //using (var writer = new StreamWriter(diretorio))
                        //using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                        //{
                        //    csvWriter.WriteHeader<ResultadoFinal>();
                        //    csvWriter.WriteRecords(result);
                        //}

                        using (TextWriter writer = new StreamWriter(diretorio, false, System.Text.Encoding.UTF8))
                        {
                            var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                            csv.WriteRecords(listDadosCotacao);
                        }

                    }
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //$"Solicitação HangupEventRequestCall: {(int)HttpStatusCode.BadRequest} {ex.Message}";
            }
        }


        public static List<DadosMoeda> ReadMoedasCSV() {
            using var streamReader = File.OpenText("C:\\ProjetoEstudo\\TesteServico\\DadosMoeda.csv");
            using var csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);

            var itemFilas = csvReader.GetRecords<DadosMoeda>();

            //foreach (var itemFila in itemFilas)
            //{
            //    Console.WriteLine(itemFila);
            //}
            return itemFilas.ToList();
        }

        public static List<DadosCotacao> ReadCotacaoCSV()
        {
            using var streamReader = File.OpenText("C:\\ProjetoEstudo\\TesteServico\\DadosCotacao.csv");
            using var csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);

            var itemFilas = csvReader.GetRecords<DadosCotacao>();

            return itemFilas.ToList();
        }
    }
}
