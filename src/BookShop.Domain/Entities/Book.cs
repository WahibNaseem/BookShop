using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Domain.Common;

namespace BookShop.Domain.Entities
{
    public class Book : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public int CategoryId { get; set; }

        /* EF Relation */
        public Category Category { get; set; }
    }


}