using System;
using System.Collections.Generic;
using System.Text;
using ImageGallery.Core.Model;

namespace ImageGallery.Core.Interface
{
    public interface IImageRepository
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
