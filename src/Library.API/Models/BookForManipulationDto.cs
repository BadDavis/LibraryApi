using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Models
{
    public abstract class BookForManipulationDto
    {
        [Required(ErrorMessage = "Powinieneś wypełnić pole tytuł")]
        [MaxLength(100, ErrorMessage = "Tytuł może zawierać maksymalnie 100 znaków")]
        public string Title { get; set; }

        [MaxLength(500, ErrorMessage = "Opis może zawierać maksymalnie 500 znaków")]
        public virtual string Description { get; set; }
    }
}
