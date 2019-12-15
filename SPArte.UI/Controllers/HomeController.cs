using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SPArte.UI.Controllers
{
    public class HomeController : Controller
    {
        public string urlprod = "http://sp-arte2020.eastus.cloudapp.azure.com";
        
        public ActionResult Index()
        {            
            return View();
        }

        [JsonRequestBehavior]
        public JsonResult Obras()
        {
            var serializer = new JavaScriptSerializer();
            string erro = String.Empty;
            using (var _client = new HttpClient())
            {
                var response = _client.GetAsync($"{urlprod}/api/v1/Obras").Result;
                _client.Dispose();
                if (response.IsSuccessStatusCode)
                {
                    string JSONString = response.Content.ReadAsStringAsync().Result;
                    object json = serializer.Deserialize(JSONString, typeof(object));
                    return Json(new { data = json });
                }
                else
                {
                    erro = response.StatusCode.ToString();
                }

            }

            return Json(new { result = new { success = true, message = erro } });


    }

        public JsonResult GravarObras(string json)
        {

            string status= PostApi($"{urlprod}/api/v1/Obras/gravarobras",json);
            return Json(new { result = new { success = true, message = status } });

        }

        private string PostApi( string url,string json)
        {
            using (var _client = new HttpClient())
            {
                var response = _client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    dynamic content = response.Content.ReadAsStringAsync().Result;
                    string retorno = (JsonConvert.DeserializeObject(content)).result.message;
                    return retorno;
                }
                else
                {
                    return "Falha ao enviar o as obras para o banco de dados";
                }
            }
        }

        public class JsonRequestBehaviorAttribute : ActionFilterAttribute
        {
            private JsonRequestBehavior Behavior { get; set; }
            public JsonRequestBehaviorAttribute()
            {
                Behavior = JsonRequestBehavior.AllowGet;
            }

            public override void OnResultExecuting(ResultExecutingContext filterContext)
            {
                var result = filterContext.Result as JsonResult;

                if (result != null)
                {
                    result.JsonRequestBehavior = Behavior;
                }
            }
        }

    }

}