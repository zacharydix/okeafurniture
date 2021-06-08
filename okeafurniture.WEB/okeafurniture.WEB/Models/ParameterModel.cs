using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace okeafurniture.WEB.Models
{
    public class ParameterModel
    {
        [Required (ErrorMessage = "Please select a start date!")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please select an end date!")]
        public DateTime EndDate { get; set; }

        public ParameterModel() { }
    }
}
