using System;
using System.ComponentModel.DataAnnotations;

namespace VXDesign.Store.CarWashSystem.Server.Core.Validations
{
    public class AgeRestrictionAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (!(value is DateTime birthday)) return true;
            return new DateTime((DateTime.Today - birthday).Ticks).Year >= 18;
        }
    }
}