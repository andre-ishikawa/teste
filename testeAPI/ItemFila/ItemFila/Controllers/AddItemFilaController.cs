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
    public class AddItemFilaController : ApiController
    {

        [HttpPost]
        [Route("addItemFila", Name = "addItemFila")]
        public IHttpActionResult AddItemFilas(List<Models.ItemFila> listItemFila)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string messages = string.Join("; ", ModelState.Values);

                    throw new Exception($"JSON recebido inválido - {messages}");
                }

                var texto = "";

                var lines = File.ReadAllLines(ConfigurationManager.AppSettings["PathItemFila"]);
                foreach (var line in lines)
                    texto = line;

                var deserializedItemFila = new List<Models.ItemFila>();

                if(!string.IsNullOrWhiteSpace(texto) && texto != "[]")
                    deserializedItemFila = JsonConvert.DeserializeObject<List<Models.ItemFila>>(texto.ToString());

                deserializedItemFila.AddRange((IEnumerable<Models.ItemFila>)listItemFila);

                StreamWriter sw = new StreamWriter(ConfigurationManager.AppSettings["PathItemFila"]);
                sw.WriteLine(JsonConvert.SerializeObject(deserializedItemFila));
                sw.Close();

                return Ok("OK");
            }
            catch (Exception ex)
            {
                var message = ex.Message;

                if (ex.Message.Equals("Non-static method requires a target."))
                    message = ex.Message + " Verifique se o Item Fila informado é valido.";

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