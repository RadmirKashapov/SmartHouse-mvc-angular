using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SmartHouse.WEB.Models
{
    public class House
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "House name")]
        public string Name { get; set; }
    }
}
