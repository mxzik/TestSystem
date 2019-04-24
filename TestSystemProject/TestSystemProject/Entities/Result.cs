namespace TestSystemProject.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Result")]
    public partial class Result
    {
        public int ResultId { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfTest { get; set; }

        public int TotalScore { get; set; }

        public int TestId { get; set; }

        public int UserId { get; set; }

        public virtual Test Test { get; set; }

        public virtual User User { get; set; }
    }
}
