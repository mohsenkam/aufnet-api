using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aufnet.Backend.Data.Models
{
    //[Serializable]
    public abstract class ValidatableObject : BaseObject
    {
        [NotMapped]
        public virtual IList<ValidationResult> Errors { get; private set; }

        public virtual bool IsValid()
        {
            this.Validate();
            return Errors.Count == 0;
        }

        public virtual ICollection<ValidationResult> ValidationResults()
        {
            return this.Errors;
        }

        public virtual IEnumerable<ValidationResult> Validate()
        {
            Errors = new List<ValidationResult>();
            Validator.TryValidateObject(this, new ValidationContext(this, null, null), Errors, true);

            return Errors;
        }
    }
}