using UnityEngine;
using UnityEngine.UI;

public class MailListItem : MonoBehaviour
{
    public Mail Mail;
    public GameObject Style;
    public Text SourceText;
    public Text DateText;
    public Text ObjectText;

    public MailApplication App;

    private void Start()
    {
        ChangeMail(Mail);
    }

    public void ChangeMail(Mail newMail)
    {
        Mail = newMail;
        if (Mail != null)
        {
            gameObject.SetActive(true);
            SourceText.text = Mail.Source.Substring(
                0,
                Mail.Source.IndexOf('@')
            ).Replace('.', ' ');
            DateText.text = Mail.DateTime.ToShortDateString() + ", " + Mail.DateTime.ToShortTimeString();
            ObjectText.text = Mail.Object;
        }
        else
            gameObject.SetActive(false);
    }

    public void SelectMail()
    {
        App.SelectMail(Mail);
    }
}