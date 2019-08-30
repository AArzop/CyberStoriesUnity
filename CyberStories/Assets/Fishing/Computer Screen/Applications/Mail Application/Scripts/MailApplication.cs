using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MailApplication : BaseApplication
{
    // List of mails, one by filter
    [FormerlySerializedAs("newMails")] public List<Mail> NewMails;
    [FormerlySerializedAs("archMails")] public List<Mail> ArchMails;
    [FormerlySerializedAs("delMails")] public List<Mail> DelMails;

    [FormerlySerializedAs("sendMessageOnArchiv")] public bool SendMessageOnArchiv = false;
    [FormerlySerializedAs("sendMessageOnDelete")] public bool SendMessageOnDelete = false;

    [FormerlySerializedAs("messageDestination")] public BaseQuestManager MessageDestination;

    // Nb item in UI
    private const int NbMailDisplayed = 6;
    [FormerlySerializedAs("mailItemUI")] public List<MailListItem> MailItemUI;

    // Current state of the application
    private Filter currentFilter;
    private int currentIndexInList = 0;
    private List<Mail> currentMailList;

    // Current mail which is displayed
    private Mail mailSelected;

    // Display warning logo
    [FormerlySerializedAs("displayWarningLogo")] public bool DisplayWarningLogo = false;

    #region UI
        [FormerlySerializedAs("bodyCanvas")] public GameObject BodyCanvas;
        [FormerlySerializedAs("mailHeaderObjectText")] public Text MailHeaderObjectText;
        [FormerlySerializedAs("mailHeaderDateText")] public Text MailHeaderDateText;
        [FormerlySerializedAs("mailHeaderSourceText")] public Text MailHeaderSourceText;
        [FormerlySerializedAs("mailBodyText")] public Text MailBodyText;
        [FormerlySerializedAs("mailLinkButton")] public Button MailLinkButton;
        [FormerlySerializedAs("mailLinkText")] public Text MailLinkText;
        [FormerlySerializedAs("warningLogo")] public Image WarningLogo;
    #endregion

    private enum Filter
    {
        NewMail,
        ArchivedMail,
        DeletedMail
    };

    public override void ResetApplication()
    {}

    // Called on scene load, set all text
    private void Awake()
    {
        // Replace text by loca
        const int NbFilter = 3;
        for (int i = 1; i <= NbFilter; ++i)
        {
            GameObject filterCanvas = GameObject.Find("Filter" + i + "Button");
            if (!filterCanvas) continue;
            Text t = filterCanvas.gameObject.GetComponentInChildren<Text>();
            switch (i)
            {
                case 1:
                    t.text = GlobalManager.GetLocalization("Loca_New");
                    break;
                case 2:
                    t.text = GlobalManager.GetLocalization("Loca_MailArchive");
                    break;
                default:
                    t.text = GlobalManager.GetLocalization("Loca_MailDelete");
                    break;
            }
        }
    }

    // Start is called before the first frame update, set default state of the application
    void Start()
    {
        currentFilter = Filter.NewMail;
        currentMailList = NewMails;
        currentIndexInList = 0;
        LoadMailItem(currentMailList, currentIndexInList);

        mailSelected = null;
        SelectMail(null);
    }

    // Assign a mail (or null) on each mail Item in list
    private void LoadMailItem(List<Mail> mails, int firstIndex)
    {
        for (int i = 0; i < NbMailDisplayed; i++)
        {
            Mail mail = firstIndex + i < mails.Count ? mails[firstIndex + i] : null;
            MailItemUI[i].ChangeMail(mail);
        }
    }

    // Change the current filter
    private void ChangeFilter(Filter newFilter)
    {
        currentFilter = newFilter;
        currentIndexInList = 0;
        SelectMail(null);

        switch (newFilter)
        {
            case Filter.NewMail:
                currentMailList = NewMails;
                LoadMailItem(NewMails, 0);
                break;

            case Filter.ArchivedMail:
                currentMailList = ArchMails;
                LoadMailItem(ArchMails, 0);
                break;

            case Filter.DeletedMail:
                currentMailList = DelMails;
                LoadMailItem(DelMails, 0);
                break;
            default:
                // Todo use assert to avoid crash in production
                throw new ArgumentOutOfRangeException(nameof(newFilter), newFilter, null);
        }
    }

    // Select a mail which is going to be displayed on the right side
    public void SelectMail(Mail mail)
    {
        mailSelected = mail;

        if (mail != null)
        {
            BodyCanvas.SetActive(true);
            MailHeaderSourceText.text = mail.Source;
            MailHeaderObjectText.text = mail.Object;
            MailHeaderDateText.text = mail.DateTime.ToShortDateString();
            MailBodyText.text = mail.Body;

            if (mail.IsThereLink)
            {
                MailLinkButton.gameObject.SetActive(true);
                MailLinkText.text = mail.Link;
            }
            else
            {
                MailLinkButton.gameObject.SetActive(false);
                MailLinkText.text = "";
            }

            if (DisplayWarningLogo && mail.IsFishingMail)
                WarningLogo.gameObject.SetActive(true);
            else
                WarningLogo.gameObject.SetActive(false);
        }
        else
        {
            BodyCanvas.SetActive(false);
            MailHeaderSourceText.text = "";
            MailHeaderObjectText.text = "";
            MailHeaderDateText.text = "";
            MailBodyText.text = "";
            MailLinkButton.gameObject.SetActive(false);
            MailLinkText.text = "";
            WarningLogo.gameObject.SetActive(false);
        }
    }

    // A function for each button
    #region ButtonFunction

    public void NavButtonUp()
    {
        if (currentIndexInList == 0)
            return;

        currentIndexInList -= NbMailDisplayed;
        if (currentIndexInList < 0)
            currentIndexInList = 0;

        LoadMailItem(currentMailList, currentIndexInList);
    }

    public void NavButtonDown()
    {
        if (currentIndexInList + NbMailDisplayed >= currentMailList.Count)
            return;

        currentIndexInList += NbMailDisplayed;
        if (currentIndexInList >= currentMailList.Count)
            currentIndexInList = currentMailList.Count - NbMailDisplayed;

        LoadMailItem(currentMailList, currentIndexInList);
    }

    public void FilterButtonNewMail()
    {
        if (currentFilter != Filter.NewMail)
            ChangeFilter(Filter.NewMail);
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
        if (!mailSelected || currentFilter == Filter.ArchivedMail) return;
        
        currentMailList.Remove(mailSelected);
        ArchMails.Add(mailSelected);
        ArchMails.Sort((m1, m2) => m2.DateTime.CompareTo(m1.DateTime));

        if (SendMessageOnArchiv)
            MessageDestination.gameObject.SendMessage("OnArchivedMail", mailSelected);

        ChangeFilter(currentFilter);
    }

    public void SelectedMailButtonDelete()
    {
        if (!mailSelected || currentFilter == Filter.DeletedMail) return;
        
        currentMailList.Remove(mailSelected);
        DelMails.Add(mailSelected);
        DelMails.Sort((m1, m2) => m2.DateTime.CompareTo(m1.DateTime));

        if (SendMessageOnDelete)
            MessageDestination.gameObject.SendMessage("OnDeletedMail", mailSelected);

        ChangeFilter(currentFilter);
    }

    public void LinkButton()
    {
        GlobalScreen.LinkClicked(mailSelected.Link);
    }
    #endregion
}
