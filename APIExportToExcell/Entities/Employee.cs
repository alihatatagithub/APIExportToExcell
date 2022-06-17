using APIExportToExcell.Validation;
using System.ComponentModel.DataAnnotations;

namespace APIExportToExcell.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [ValidDate(ErrorMessage ="Enter Date In Format MM.DD.YYYY")]
        public string HiringDate { get; set; }

    }
}
