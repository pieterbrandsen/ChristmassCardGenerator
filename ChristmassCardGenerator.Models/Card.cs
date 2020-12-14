using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmassCardGenerator.Models
{
    public enum FontTypes
    {
        Ariel
    }
    public enum ImageSize
    {
        Medium
    }
    public enum TextSize
    {
        Medium
    }

    public enum CardCategories
    {
        Santa, Reindeer
    }

    public class Card
    {
        public int ID { get; set; }
        public CardCategories CardCategory { get; set; }
        public TextSize TextSize { get; set; }
        public FontTypes Font { get; set; }
        public ImageSize ImageSize { get; set; }
        public int TimesUsed { get; set; }
    }
}
