using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace LeoLuz
{
    public class InputAxesListDropdownAttribute : PropertyAttribute
    {
        public bool hideLabel;
        public InputAxesListDropdownAttribute(bool hideLabel = false)
        {
            this.hideLabel = hideLabel;
        }
    }
}
