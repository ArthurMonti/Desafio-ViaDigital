using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICore.Models
{
    public class Container
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IDUser { get; set; }
        public int Position { get; set; }

        public List<Card> Cards { get; set; }
    }
}
