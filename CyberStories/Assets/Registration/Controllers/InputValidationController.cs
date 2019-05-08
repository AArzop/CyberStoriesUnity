using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CyberStories.Registration.Controllers
{
    public class InputValidationController : MonoBehaviour
    {
        public InputField PseudoInput;
        public InputField EmailInput;
        public Text ErrorLabel;

        // TODO: Localization
        private static readonly string ErrorEmailMessage = "Email non valide";
        private static readonly string ErrorPseudoMessage = "Pseudo non valide";

        public void ValidateClick()
        {
            // Check if the inputs (email & pseudo are valid)
            if (!ValidateEmailInput())
                ErrorLabel.text = ErrorEmailMessage;
            else if (string.IsNullOrWhiteSpace(PseudoInput.text))
                ErrorLabel.text = ErrorPseudoMessage;
            else
            {
                // Clear Error text display
                ErrorLabel.text = "";

                // TODO: Send pseudo & mail to database and save it as global
                SceneManager.LoadScene("MenuScene");
            }
        }

        /// <summary>
        /// Validate email input using Microsoft Regex
        /// </summary>
        /// <returns>True if the email is valid. Else, false.</returns>
        private bool ValidateEmailInput()
        {
            if (string.IsNullOrWhiteSpace(EmailInput.text))
                return false;
            var email = EmailInput.text;
            try
            {
                // https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}