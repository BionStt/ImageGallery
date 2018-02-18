using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using ImageGallery.Core.Interface.Service;
using ImageGallery.Core.Model;
using ImageGallery.Core.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace ImageGalley.Web.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ImageController(IImageService imageService, IHostingEnvironment hostingEnvironment)
        {
            _imageService = imageService;
            _hostingEnvironment = hostingEnvironment;
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

        // POST: /ImageManager/UploadImages
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadImages()
        {
            var files = Request.Form.Files;

            if (files.Count > 0)
            {
                var imageList = new List<ImageModel>();
                var dir = Path.Combine(_hostingEnvironment.WebRootPath, "images/app");
                Directory.CreateDirectory(dir);

                try
                {
                    foreach (var file in files)
                    {
                        var imagePath = Path.Combine(dir, file.FileName);
                        var fileNameWithoutExt = Path.GetFileNameWithoutExtension(imagePath);
                        var ext = Path.GetExtension(imagePath);
                        var imageFileName = fileNameWithoutExt + "." + Guid.NewGuid().ToString().Substring(0, 8) + ext;
                        imagePath = Path.Combine(dir, imageFileName);

                        var productImage = new ImageModel
                        {
                            Id = Guid.NewGuid(),
                            FileName = "/images/app/" + imageFileName
                        };

                        // save image to local disk
                        using (FileStream fs = System.IO.File.Create(Path.Combine(dir, imagePath)))
                        {
                            await file.CopyToAsync(fs);
                            imageList.Add(productImage);
                        }
                    }

                    // save image info to database
                    _imageService.InsertImages(imageList);
                    return new NoContentResult();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return Json("error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteImages(List<string> Ids)
        {
            try
            {
                var imageToDelete = new List<Guid>();

                foreach (var id in Ids)
                {
                    var image = _imageService.GetImageById(Guid.Parse(id));

                    if (image != null)
                    {
                        // delete image from local disk
                        var dir = Path.Combine(_hostingEnvironment.WebRootPath, "images/app");
                        var imagePath = Path.Combine(dir, image.FileName);

                        if (System.IO.File.Exists(imagePath))
                            System.IO.File.Delete(imagePath);

                        imageToDelete.Add(image.Id);
                    }
                }

                // delete image from database
                _imageService.DeleteImages(imageToDelete);

                return new NoContentResult();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: /ImageManager/SearchImage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchImage(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return new NoContentResult();

            var imageList = _imageService.SearchImages(keyword);
            var model = new List<ImageModel>();
            foreach (var image in imageList)
            {
                var imageModel = new ImageModel
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