using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageGallery.Core.Interface.Base;
using ImageGallery.Core.Interface.Service;
using ImageGallery.Core.Model;
using ImageGalley.Data.AppDbContext;
using Microsoft.EntityFrameworkCore;

namespace ImageGalley.Data.Service
{
    public class ImageService : IImageService
    {
        private readonly IRepository<ImageModel> _imageRepository;
        private readonly IRepository<ProductImageMappingModel> _productImageRepository;
     
        public ImageService(IRepository<ImageModel> imageRepository, IRepository<ProductImageMappingModel> productImageRepository)
        {
            _imageRepository = imageRepository;
            _productImageRepository = productImageRepository;
            
        }

        public IList<ImageModel> GetAllImages()
        {
            return _imageRepository.All()
                .OrderBy(x => x.FileName)
                .ToList();
        }

        public ImageModel GetImageById(Guid id)
        {
            return _imageRepository.FindByExpression(x => x.Id == id);
        }

        public IList<ImageModel> SearchImages(string keyword)
        {
            return _imageRepository.FindMany(x => x.FileName.Contains(keyword))
                .OrderBy(x => x.FileName)
                .ToList();
        }

        public void InsertImages(IList<ImageModel> images)
        {
            if (images == null)
                throw new ArgumentNullException("images");

            foreach (var image in images)
                _imageRepository.Insert(image);

            _imageRepository.SaveChanges();
        }

        public void DeleteImages(IList<Guid> ids)
        {
            if (ids == null)
                throw new ArgumentNullException("ids");

            foreach (var id in ids)
                _imageRepository.Delete(GetImageById(id));

            _imageRepository.SaveChanges();
        }

        public void InsertProductImageMappings(IList<ProductImageMappingModel> productImageMappings)
        {
            if (productImageMappings == null)
                throw new ArgumentNullException("productImageMappings");

            foreach (var mapping in productImageMappings)
                _productImageRepository.Insert(mapping);

            _productImageRepository.SaveChanges();
        }

        public void DeleteAllProductImageMappings(Guid productId)
        {
            if (productId == null)
                throw new ArgumentNullException("productImageMappings");

            var mappings = _productImageRepository.FindMany(x => x.ProductId == productId);

            foreach (var mapping in mappings)
                _productImageRepository.Delete(mapping);

            _productImageRepository.SaveChanges();
        }
    }
}
