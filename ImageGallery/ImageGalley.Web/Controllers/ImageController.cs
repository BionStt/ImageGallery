using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageGallery.Core.Interface.Service;
using ImageGallery.Core.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ImageGalley.Web.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }
        public IActionResult Index()
        {
            return View();
        }

        // GET: /ImageManager/GetAllImages
        public IActionResult GetAllImages()
        {
            // get all image from database
            var imageList = _imageService.GetAllImages();
            var model = new List<ImageViewModel>();

            foreach (var image in imageList)
            {
                var imageModel = new ImageViewModel()
                {
                    Id = image.Id,
                    FileName = image.FileName
                };
                model.Add(imageModel);
            }
            return Json(model);
        }
    }
}