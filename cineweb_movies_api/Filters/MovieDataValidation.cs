using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cineweb_movies_api.Filters
{
    public class MovieDataValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var data = Convert.ToDateTime(value);
            return data > DateTime.Now;
        }
    }
}
