using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Models.RequestModels
{
    public class HotPotRequestModel
    {
    }

    public class CreateHotPotRequestModel
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int FlavorID { get; set; }
        public int TypeID { get; set; }
    }

    public class UpdateHotPotRequestModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int FlavorID { get; set; }
        public int TypeID { get; set; }
    }
}
