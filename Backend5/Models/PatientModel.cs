using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Backend5.Models
{
    public class PatientModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите диагноз")]
        [Display(Name = "Disease")]
        public string Disease { get; set; }
    }
}
