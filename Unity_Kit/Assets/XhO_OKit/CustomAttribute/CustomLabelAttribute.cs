using UnityEngine;

namespace XhO_OKit
{

    public class CustomLabelAttribute : PropertyAttribute
    {
        public string customLabelName;

        public CustomLabelAttribute(string labelName)
        {
            customLabelName = labelName;
        }
    }
}