using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CyberStories.Registration.Controllers
{
    public class InputValidationController : MonoBehaviour
    {
        public InputField EmailInput;
        public Text ErrorLabel;

        // TODO: Localization
        private static readonly string ErrorMessage = "Email non valide";

        public void ValidateClick()
        {
            ErrorLabel.text = "";
            if (!ValidateInput())
                ErrorLabel.text = ErrorMessage;
            else
            {
                // TODO: Send mail to database and save it as global
                SceneManager.LoadScene("MenuScene");
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(EmailInput.text))
                return false;
            var email = EmailInput.text;
            try
            {
                // return new System.Net.Mail.MailAddress(email).Address == email;

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