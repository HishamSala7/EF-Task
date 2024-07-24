using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkAssignment
{
    internal class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName ="nvarchar")]
        [MaxLength(60)]
        public string Title { get; set; }
        
        public int? ISBN { get; set; }
        public int? PublicationYear { get; set; }
        public virtual ICollection<BookAuthor> nvgBookAuthors { get; set; }
        public virtual ICollection<Borrow>  nvgBorrows { get; set; }



    }
}
