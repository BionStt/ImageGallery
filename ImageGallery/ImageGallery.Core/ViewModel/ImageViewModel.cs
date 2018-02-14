using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGallery.Core.ViewModel
{
    public class ImageViewModel
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public int SortOrder { get; set; }
    }
}
