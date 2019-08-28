using UnityEngine;
using UnityEngine.UI;

public class MailListItem : MonoBehaviour
{
    public Mail mail;
    public GameObject style;
    public Text sourceText;
    public Text dateText;
    public Text objectText;

    public MailApplication app;

    private void Start()
    {
        ChangeMail(mail);
    }

    public void ChangeMail(Mail newMail)
    {
        mail = newMail;
        if (mail != null)
        {
            gameObject.SetActive(true);
            sourceText.text = mail.Source.Substring(0, mail.Source.IndexOf('@')).Replace('.', ' ');
            dateText.text = mail.DateTime.ToShortDateString() + ", " + mail.DateTime.ToShortTimeString();
            objectText.text = mail.Object;
        }
        else
            gameObject.SetActive(false);
    }

    public void SelectMail()
    {
        app.SelectMail(mail);
    }
}