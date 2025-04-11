﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }



        public int BrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }//Navigational prop



        public int TypeId { get; set; }
        public ProductType ProductType { get; set; }
    }
}
