using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailApplication : BaseApplication
{
    public List<Mail> newMails;
    public List<Mail> archMails;
    public List<Mail> delMails;

    public int nbMailDisplayed = 6;
    public List<MailListItem> mailItemUI;

    private Filter currentFilter;

    private int currentIndexInList = 0;
    private List<Mail> currentMailList;

    private Mail mailSelected;


    public GameObject bodyCanvas;
    public Text mailHeaderObjectText;
    public Text mailHeaderDateText;
    public Text mailHeaderSourceText;
    public Text mailBodyText;
    public Button mailLinkButton;
    public Text mailLinkText;

    private enum Filter
    {
        newMail,
        ArchivedMail,
        DeletedMail
    };

    public override void ResetApplication()
    {}

    // Start is called before the first frame update
    void Start()
    {
        currentFilter = Filter.newMail;
        currentMailList = newMails;
        currentIndexInList = 0;
        LoadMailItem(currentMailList, currentIndexInList);

        mailSelected = null;
        SelectMail(null);
    }

    private void LoadMailItem(List<Mail> mails, int firstIndex)
    {
        for (int i = 0; i < nbMailDisplayed; i++)
        {
            Mail mail = firstIndex + i < mails.Count ? mails[firstIndex + i] : null;
            mailItemUI[i].ChangeMail(mail);
        }
    }

    private void ChangeFilter(Filter newFilter)
    {
        currentFilter = newFilter;
        currentIndexInList = 0;
        SelectMail(null);

        switch (newFilter)
        {
            case Filter.newMail:
                currentMailList = newMails;
                LoadMailItem(newMails, 0);
                break;

            case Filter.ArchivedMail:
                currentMailList = archMails;
                LoadMailItem(archMails, 0);
                break;

            case Filter.DeletedMail:
                currentMailList = delMails;
                LoadMailItem(delMails, 0);
                break;

            default:
                break;
        }
    }

    public void SelectMail(Mail mail)
    {
        mailSelected = mail;

        if (mail != null)
        {
            bodyCanvas.SetActive(true);
            mailHeaderSourceText.text = mail.Source;
            mailHeaderObjectText.text = mail.Object;
            mailHeaderDateText.text = mail.dateTime.ToShortDateString();
            mailBodyText.text = mail.body;

            if (mail.isThereLink)
            {
                mailLinkButton.gameObject.SetActive(true);
                mailLinkText.text = mail.link;
            }
            else
            {
                mailLinkButton.gameObject.SetActive(false);
                mailLinkText.text = "";
            }
        }
        else
        {
            bodyCanvas.SetActive(false);
            mailHeaderSourceText.text = "";
            mailHeaderObjectText.text = "";
            mailHeaderDateText.text = "";
            mailBodyText.text = "";
            mailLinkButton.gameObject.SetActive(false);
            mailLinkText.text = "";
        }
    }

    #region ButtonFunction

    public void NavButtonUp()
    {
        if (currentIndexInList == 0)
            return;

        currentIndexInList -= nbMailDisplayed;
        if (currentIndexInList < 0)
            currentIndexInList = 0;

        LoadMailItem(currentMailList, currentIndexInList);
    }

    public void NavButtonDown()
    {
        if (currentIndexInList + nbMailDisplayed >= currentMailList.Count)
            return;

        currentIndexInList += nbMailDisplayed;
        if (currentIndexInList >= currentMailList.Count)
            currentIndexInList = currentMailList.Count - nbMailDisplayed;

        LoadMailItem(currentMailList, currentIndexInList);
    }

    public void FilterButtonNewMail()
    {
        if (currentFilter != Filter.newMail)
            ChangeFilter(Filter.newMail);
    }

    public void FilterButtonArchive()
    {
        if (currentFilter != Filter.ArchivedMail)
            ChangeFilter(Filter.ArchivedMail);
    }

    public void FilterButtonDelete()
    {
        if (currentFilter != Filter.DeletedMail)
            ChangeFilter(Filter.DeletedMail);
    }

    public void SelectedMailButtonArchive()
    {
        if (mailSelected && currentFilter != Filter.ArchivedMail)
        {
            currentMailList.Remove(mailSelected);
            archMails.Add(mailSelected);
            archMails.Sort((m1, m2) => m2.dateTime.CompareTo(m1.dateTime));
            ChangeFilter(currentFilter);
        }
    }

    public void SelectedMailButtonDelete()
    {
        if (mailSelected && currentFilter != Filter.DeletedMail)
        {
            currentMailList.Remove(mailSelected);
            delMails.Add(mailSelected);
            delMails.Sort((m1, m2) => m2.dateTime.CompareTo(m1.dateTime));
            ChangeFilter(currentFilter);
        }
    }

    public void LinkButton()
    {
        GlobalScreen.LinkClicked(mailSelected);
    }
    #endregion
}
