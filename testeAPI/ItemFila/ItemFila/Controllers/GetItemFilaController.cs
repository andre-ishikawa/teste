using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.IO;
using System.Web;
using ItemFila.Models;
using Newtonsoft.Json;

namespace ItemFila.Controllers
{
    public class GetItemFilaController : ApiController
    {
        [Route("getItemFila", Name = "getItemFila")]
        public IHttpActionResult GetItemFilaList()
        {
            try
            {
                var texto = "";

                var lines = File.ReadAllLines(ConfigurationManager.AppSettings["PathItemFila"]);

                foreach (var line in lines)
                    texto = line;

                var deserializedItemFila = new List<Models.ItemFila>();
                if (!string.IsNullOrWhiteSpace(texto) && texto != "[]")
                    deserializedItemFila = JsonConvert.DeserializeObject<List<Models.ItemFila>>(texto.ToString());
                else
                {
                    return BadRequest("Não existem dados para serem retornados");
                }

                var lastResult = JsonConvert.SerializeObject(deserializedItemFila.LastOrDefault());
                var count = deserializedItemFila.Count() - 1;
                deserializedItemFila.RemoveAt(count);

                StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["PathItemFila"]);
                sw.WriteLine(JsonConvert.SerializeObject(deserializedItemFila));
                sw.Close();

                return Ok(lastResult);
            }
            catch (Exception ex)
            {
                var message = ex.Message;

                var resultado = new
                {
                    codHttp = (int)HttpStatusCode.BadRequest,
                    error = message
                };

                return BadRequest(resultado.ToString());
            }
        }
    }
}