using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkAssignment
{
    internal class Borrow
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public DateTime? BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        [ForeignKey("nvgBook")]
        public int? fkBookId { get; set; }
        [ForeignKey("nvgMember")]
        public int? fkMemberId   { get; set; }


        public virtual Book nvgBook { get; set; }

        public virtual Member nvgMember { get; set; }

    }
}
