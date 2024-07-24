using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkAssignment
{
    internal class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName ="nvarchar")]
        [MaxLength(50)]
        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public virtual ICollection<BookAuthor> nvgAuthorBooks { get; set; }


    }
}
