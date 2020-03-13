using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartHouse.WEB.Models
{
    public class Record
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Date")]
        public DateTimeOffset Date { get; set; }

        [Display(Name = "Value of sensor")]
        public int Data { get; set; }

        [Display(Name = "Sensor Id")]
        public int SensorId { get; set; }


        public Record()
        {
            Date = DateTime.Now;
        }
    }
}
