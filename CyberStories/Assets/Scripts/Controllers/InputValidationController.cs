using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CyberStories.Controllers.Register
{
    public class InputValidationController : MonoBehaviour
    {
        public InputField EmailInput;
        public Text ErrorLabel;

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
                return new System.Net.Mail.MailAddress(email).Address == email;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}