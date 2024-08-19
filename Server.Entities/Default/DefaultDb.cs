using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace System.Entities
{
    public class DefaultDb
    {

        [Key]
        [JsonIgnore]
        public int Id { get; set; }


        [JsonIgnore]
        public Guid UniqueId { get; set; } = Guid.NewGuid();

        [JsonIgnore]
        public bool IsDeleted { get; set; } = false;

        [JsonIgnore]
        public DateTime LastUpdate { get; set; } = DateTime.MinValue;

        [JsonIgnore]
        public DateTime DeleteDate { get; set; } = DateTime.MinValue;

        public virtual bool Validate()
        {

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(this, null, null);

            bool isValid = Validator.TryValidateObject(this, validationContext, validationResults, true);

            return isValid;

        }
    }
}
