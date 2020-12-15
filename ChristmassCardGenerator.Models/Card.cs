using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmassCardGenerator.Models
{
    public class Card
    {
        public int ID { get; set; }
        public string CardTitle { get; set; }
        public string FromTitle { get; set; }
        public string ImageName { get; set; }
        [NotMapped]
        public string ShortImageName => ImageName?.Split(".")[0];
        public string Message { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
