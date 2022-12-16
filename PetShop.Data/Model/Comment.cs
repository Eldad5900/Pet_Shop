using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PetShop.Data.Model
{
    [Table("Comment")]
    public partial class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Content { get; set; } = null!;
        public int? AnimelId { get; set; }

        [ForeignKey("AnimelId")]
        [InverseProperty("Comments")]
        public virtual Animal? Animel { get; set; }
    }
}
