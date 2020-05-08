using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo01.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgBase64 { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
