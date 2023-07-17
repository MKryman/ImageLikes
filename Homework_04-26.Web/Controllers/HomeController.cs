using Homework_04_26.Data;
using Homework_04_26.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Homework_04_26.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;
        private IWebHostEnvironment _environment;


        public HomeController(IConfiguration config, IWebHostEnvironment environment)
        {
            _connectionString = config.GetConnectionString("ConStr");
            _environment = environment;
        }

        public IActionResult Index()
        {
            var repo = new ImagesRepository(_connectionString);
            return View(new HomePageViewModel
            {
                Images = repo.GetImages().OrderByDescending(d => d.DateUploaded).ToList()
            });
        }

        public IActionResult UploadImg()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadImg(Image img, IFormFile imageFile)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            string fullPath = Path.Combine(_environment.WebRootPath, "Uploads", fileName);
            using var stream = new FileStream(fullPath, FileMode.CreateNew);
            imageFile.CopyTo(stream);

            img.FileName = fileName;
            img.DateUploaded = DateTime.Now;
            var repo = new ImagesRepository(_connectionString);
            repo.AddImage(img);
            return Redirect("/");
        }

        public IActionResult ViewImage(int id)
        {
            var repo = new ImagesRepository(_connectionString);
            var idsInSession = HttpContext.Session.Get<List<int>>("ids");
            var vm = new ViewImageViewModel()
            {
                Img = repo.GetById(id)
            };

            if (idsInSession != null && idsInSession.Contains(id))
            {
                vm.Liked = true;
            }

            return View(vm);
        }

        public IActionResult GetImage(int id)
        {
            var repo = new ImagesRepository(_connectionString);
            return Json(repo.GetById(id));
        }

        [HttpPost]
        public void UpdateLikes(int id)
        {
            var repo = new ImagesRepository(_connectionString);
            repo.UpdateLikes(id);

            var idsInSession = HttpContext.Session.Get<List<int>>("ids");

            if (idsInSession == null)
            {
                idsInSession = new();
            }

            idsInSession.Add(id);
            HttpContext.Session.Set<List<int>>("ids", idsInSession);
        }


    }
}