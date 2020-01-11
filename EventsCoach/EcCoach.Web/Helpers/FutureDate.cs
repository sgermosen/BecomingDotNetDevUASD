using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace EcCoach.Web.Helpers
{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime date;
            var isValid = DateTime.TryParseExact(value.ToString(),
                "dd/MM/yyyy", CultureInfo.CurrentCulture,
                DateTimeStyles.None, out date);

            return (isValid && date > DateTime.Now);
        }
    }
}
