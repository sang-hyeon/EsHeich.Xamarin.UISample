
namespace EsHeichSample.Client.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Hero : IBaseEntity
    {
        public int ID { get; set; }

        [MaxLength(100)] 
        public string HeroName { get; set; }

        [MaxLength(100)]
        public string RealName { get; set; }

        public string HeroImg { get; set; }

        public string SuperPower { get; set; }

        public string Summary { get; set; }

        public HeroRole Role { get; set; }

        public string SignatureColor_Hex { get; set; }
    }
}
