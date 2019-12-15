using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SPArte.UI.Controllers
{
    
    public class ExposicaoController : Controller
    {
        public string urlprod = "http://sp-arte2020.eastus.cloudapp.azure.com";    
        public string erro = string.Empty;
        
        public ActionResult Index()
        {
            return View();
        }
        [JsonRequestBehavior]
        public JsonResult ExposicaoInfo()
        {
            var serializer = new JavaScriptSerializer();
            using (var _client = new HttpClient())
            {
                var response = _client.GetAsync($"{urlprod}/api/v1/Exposicao").Result;
                _client.Dispose();
                if (response.IsSuccessStatusCode)
                {
                    string JSONString = response.Content.ReadAsStringAsync().Result;
                    object json = serializer.Deserialize(JSONString, typeof(object));
                    return Json(new { data = json }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                     erro = response.StatusCode.ToString();
                }

            }

            return Json(new { result = new { success = true, message = erro } });


        }
        [JsonRequestBehavior]
        public JsonResult ObrasExpostas()
        {
           
            var serializer = new JavaScriptSerializer();
            using (var _client = new HttpClient())
            {
                var response = _client.GetAsync($"{urlprod}/api/v1/Exposicao/obrasexpostas").Result;
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

        [JsonRequestBehavior]
        public JsonResult RemoveObrasExpostas()
        {

            var serializer = new JavaScriptSerializer();
            using (var _client = new HttpClient())
            {
                var response = _client.DeleteAsync($"{urlprod}/api/v1/Exposicao/removerobrasexpostas").Result;
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

        //correção get no azure
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