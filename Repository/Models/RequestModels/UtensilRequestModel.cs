using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.RequestModels
{
    public class UtensilRequestModel
    {
    }

    public class CreateUtensilRequestModel
    {
        public string Name { get; set; }
        public string Material { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string ImageUrl { get; set; }
    }

    public class UpdateUtensilRequestModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Material { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string ImageUrl { get; set; }
    }

}
