using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniProject4.Domain.Entities;

[Table("employees")]
public partial class Employee
{
    [Key]
    [Column("empno")]
    public int Empno { get; set; }

    [Column("fname")]
    public string? Fname { get; set; }

    [Column("lname")]
    public string? Lname { get; set; }

    [Column("address")]
    public string? Address { get; set; }

    public DateOnly? Dob { get; set; }

    [Column("sex", TypeName = "character varying")]
    public string? Sex { get; set; }

    [Column("position")]
    public string? Position { get; set; }

    [Column("deptno")]
    public int? Deptno { get; set; }

    [InverseProperty("MgrempnoNavigation")]
    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    [ForeignKey("Deptno")]
    [InverseProperty("Employees")]
    public virtual Department? DeptnoNavigation { get; set; }

    [InverseProperty("EmpnoNavigation")]
    public virtual ICollection<Workson> Worksons { get; set; } = new List<Workson>();
}
