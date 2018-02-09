using System;
using System.Collections.Generic;
using ImageGallery.Core.Model;

namespace ImageGallery.Core.Interface.Service
{
    public interface IImageService
    {
        IList<ImageModel> GetAllImages();
        ImageModel GetImageById(Guid id);
        IList<ImageModel> SearchImages(string keyword);
        void InsertImages(IList<ImageModel> images);
        void DeleteImages(IList<Guid> ids);
        void InsertProductImageMappings(IList<ProductImageMappingModel> productImageMappings);
        void DeleteAllProductImageMappings(Guid productId);
    }
}
