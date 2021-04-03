using System;

namespace ApiHub.ApplicationAttribute
{
    public class IncludeInSwaggerAttribute : Attribute
    {
        public bool IsEnable { get; }

        public IncludeInSwaggerAttribute(bool isEnable = true)
        {
            IsEnable = isEnable;
        }
    }
}