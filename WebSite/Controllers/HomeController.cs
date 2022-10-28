using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormCollection form)
        {
            if (form.Files.Count == 0)
            {
                ModelState.AddModelError("", "No file!");
                return View();
            }

            //Récupération du fichier
            IFormFile file = form.Files[0];
            //Récupération du nom fichier
            string fileName = file.FileName;

            //Récupération du contenu
            Stream stream = file.OpenReadStream();
            byte[] content = new byte[stream.Length];
            stream.Read(content, 0, content.Length);

            //Envoi vers l'api
            using (HttpClient client = new HttpClient())
            {
                var apiForm = new { Name = fileName, Content = Convert.ToBase64String(content) };

                HttpContent httpContent = new StringContent(JsonSerializer.Serialize(apiForm));
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                HttpResponseMessage responseMessage = client.PostAsync("https://localhost:7105/api/File", httpContent).Result;

                if(!responseMessage.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", responseMessage.ReasonPhrase ?? "Error with Api");
                    return View();
                }
            }

            ViewBag.Message = "File Sended";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}