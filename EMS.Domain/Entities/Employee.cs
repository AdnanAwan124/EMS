using EMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        [MaxLength(5)]
        public string Title { get; set; } = null!;
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        [MaxLength(100)]
        public string Email { get; set; } = null!;
        public DateTime DOB { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; } = null!;

    }
}
