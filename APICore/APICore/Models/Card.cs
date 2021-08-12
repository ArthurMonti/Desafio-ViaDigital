using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICore.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public string Description { get; set; }
        public int ContainerID { get; set; }
    }
}
