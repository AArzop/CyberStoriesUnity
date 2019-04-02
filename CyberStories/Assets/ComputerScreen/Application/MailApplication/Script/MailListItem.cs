using System.Collections;
using System.Collections.Generic;
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
            sourceText.text = mail.Source;
            dateText.text = mail.dateTime.ToShortDateString() + ", " + mail.dateTime.ToShortTimeString();
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
