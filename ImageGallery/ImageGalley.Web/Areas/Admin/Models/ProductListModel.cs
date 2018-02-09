using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageGalley.Web.Areas.Admin.Models
{
    public class ProductListModel
    {
        public Guid Id { get; set; }
        public string MainImage { get; set; }
        public string Name { get; set; }
        public int StockQuantity { get; set; }
        public bool Published { get; set; }
    }
}
