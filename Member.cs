using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkAssignment
{
    internal class Member
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName ="nvarchar")]
        [MaxLength(50)]
        public string Name { get; set; }

        //[EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public DateTime? MemershipDate { get; set; }
        public virtual ICollection<Borrow> nvgBorrows { get; set; }


    }
}
