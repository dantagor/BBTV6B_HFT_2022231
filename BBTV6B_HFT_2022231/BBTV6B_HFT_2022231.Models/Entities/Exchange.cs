using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBTV6B_HFT_2022231.Models.Entities
{
    public class Exchange
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string NameShort { get; set; }

        public string Name { get; set; }

        public string Region { get; set; }

        [NotMapped]
        public virtual ICollection<Stock> Stocks { get; set; }

        public Exchange()
        {
            Stocks = new HashSet<Stock>();
        }
        public Exchange(int id, string nameShort, string name, string region)
        {
            Id = id;
            NameShort = nameShort;
            Name = name;
            Region = region;
            Stocks = new HashSet<Stock>();
        }

        public override string ToString()
        {
            return $"#{Id}-EXCHANGE: {NameShort}- {Name}, Region = {Region}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Exchange)
            {
                Exchange value = (Exchange)obj;
                return this.Id == value.Id;
            }
            else
            {
                return false;
            }
        }
    }
}
