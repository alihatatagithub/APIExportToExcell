using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace APIExportToExcell.Validation
{
    public class ValidDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                 "MM.dd.yyyy",
                 CultureInfo.CurrentCulture,
                 DateTimeStyles.None,
                 out dateTime);

            return isValid ;
        }
    
    }
}
