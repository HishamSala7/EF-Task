using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkAssignment
{
    internal class BookAuthor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [ForeignKey("nvgBook")]
        public int? fkBookId { get; set; }

        [ForeignKey("nvgAuthor")]
        public int? fkAuthorId { get; set; }

        public virtual Book nvgBook { get; set; }
        
        public virtual Author nvgAuthor { get; set; }



    }
}
