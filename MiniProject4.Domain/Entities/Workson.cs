using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProject4.Domain.Entities;

[Table("workson")]
public partial class Workson
{
    [Key]
    [Column("empno")]
    public int Empno { get; set; }

    [Key]
    [Column("projno")]
    public int Projno { get; set; }

    [Column("dateworked", TypeName = "timestamp without time zone")]
    public DateTime? Dateworked { get; set; }

    [Column("hoursworked")]
    public int? Hoursworked { get; set; }

    [ForeignKey("Empno")]
    [InverseProperty("Worksons")]
    public virtual Employee? EmpnoNavigation { get; set; } = null!;

    [ForeignKey("Projno")]
    [InverseProperty("Worksons")]
    public virtual Project? ProjnoNavigation { get; set; } = null!;
}
