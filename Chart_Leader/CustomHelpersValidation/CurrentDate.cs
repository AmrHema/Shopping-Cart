using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Chart_Leader.CustomHelpersValidation
{
    public class CurrentDate:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime currentdate = Convert.ToDateTime(value);
            return currentdate>=DateTime.Now;
        }
    }


   

}