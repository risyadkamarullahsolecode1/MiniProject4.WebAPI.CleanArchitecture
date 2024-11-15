using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProject4.Domain.Entities;

[Table("projects")]
public partial class Project
{
    [Key]
    [Column("projno")]
    public int Projno { get; set; }

    [Column("projname")]
    public string? Projname { get; set; }

    [Column("deptno")]
    public int? Deptno { get; set; }

    [ForeignKey("Deptno")]
    [InverseProperty("Projects")]
    public virtual Department? DeptnoNavigation { get; set; }

    [InverseProperty("ProjnoNavigation")]
    public virtual ICollection<Workson> Worksons { get; set; } = new List<Workson>();
}
