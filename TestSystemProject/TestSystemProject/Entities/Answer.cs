namespace TestSystemProject.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Answer")]
    public partial class Answer
    {
        public int AnswerId { get; set; }

        [Required]
        [StringLength(50)]
        public string Text { get; set; }

        public bool IsRight { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
