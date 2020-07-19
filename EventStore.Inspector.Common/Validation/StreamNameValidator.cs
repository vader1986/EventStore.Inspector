using System;

namespace EventStore.Inspector.Common.Validation
{
    public class StreamNameValidator : IValidator<string>
    {
        public string Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value;
        }
    }
}
