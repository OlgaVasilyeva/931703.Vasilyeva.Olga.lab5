using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Backend5.Models
{
    public class DoctorModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите специальность")]
        [Display(Name = "Speciality")]
        public string Speciality { get; set; }

    }
}
