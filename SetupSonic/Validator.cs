using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace SetupSonic
{
    public class SonicValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var profile = (value as BindingGroup).Items[0] as SonicProfile;

            try
            {
                if (profile.CharName.Length > 16)
                {
                    return new ValidationResult(false, "Character name can't exceed 16 characters");
                }

                if (profile.Account.Length > 16)
                {
                    return new ValidationResult(false, "Account can't exceed 16 characters");
                }

                if (profile.Profile != "all" && !profile.Profile.ToLower().Contains("sonic"))
                {
                    return new ValidationResult(false, "D2Bot# Profile must include the term 'sonic' in the profile");
                }
            } catch
            {

            }


            return ValidationResult.ValidResult;
        }
    }
}
