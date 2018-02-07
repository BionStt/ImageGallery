using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ImageGallery.Core.Model
{
    [Table("ProductImageMapping")]
    public class ProductImageMappingModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ImageId { get; set; }

        public int SortOrder { get; set; }
        public int Position { get; set; }

        public virtual ProductModel Product { get; set; }
        public virtual ImageModel Image { get; set; }
    }
}
