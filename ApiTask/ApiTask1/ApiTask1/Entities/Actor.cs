using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTask1.Entities
{
    public class Actor:BaseEntity
    {
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
    }
}
