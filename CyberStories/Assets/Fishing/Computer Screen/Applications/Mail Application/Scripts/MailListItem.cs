using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MailListItem : MonoBehaviour
{
    [FormerlySerializedAs("mail")] public Mail Mail;
    [FormerlySerializedAs("style")] public GameObject Style;
    [FormerlySerializedAs("sourceText")] public Text SourceText;
    [FormerlySerializedAs("dateText")] public Text DateText;
    [FormerlySerializedAs("objectText")] public Text ObjectText;

    [FormerlySerializedAs("app")] public MailApplication App;

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