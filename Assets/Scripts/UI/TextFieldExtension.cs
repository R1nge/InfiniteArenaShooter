using UnityEngine.UIElements;

namespace UI
{
    public static class TextFieldExtension
    {
        public static void SetPlaceholderText(this TextField textField, string placeholder, bool isPassword)
        {
            string placeholderClass = TextField.ussClassName + "__placeholder";

            OnFocusOut();
            textField.RegisterCallback<FocusInEvent>(_ => OnFocusIn());
            textField.RegisterCallback<FocusOutEvent>(_ => OnFocusOut());

            void OnFocusIn()
            {
                if (textField.ClassListContains(placeholderClass))
                {
                    textField.value = string.Empty;
                    textField.RemoveFromClassList(placeholderClass);
                    textField.isPasswordField = isPassword;
                }
            }

            void OnFocusOut()
            {
                if (string.IsNullOrEmpty(textField.text))
                {
                    textField.SetValueWithoutNotify(placeholder);
                    textField.AddToClassList(placeholderClass);
                    textField.isPasswordField = false;
                }
            }
        }
    }
}