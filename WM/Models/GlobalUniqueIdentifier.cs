using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WM.Api.Models
{
    [Table("GlobalUniqueIdentifier")]
    public partial class GlobalUniqueIdentifier
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("guid")]
        [StringLength(50)]
        public string Guid { get; set; } = null!;
        [Column("expire", TypeName = "date")]
        public DateTime Expire { get; set; }
        [Column("usr")]
        [StringLength(100)]
        public string Usr { get; set; } = null!;
    }
}
