using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ImageGallery.Core.Model;
using ImageGalley.Web.Controllers;

namespace ImageGalley.Web.Helpers
{
    public class AutoMapperProfileConfig : Profile
    {
        public AutoMapperProfileConfig()
        {
            CreateMap<ProductModel, ProductListModel>();
        }
    }
}
