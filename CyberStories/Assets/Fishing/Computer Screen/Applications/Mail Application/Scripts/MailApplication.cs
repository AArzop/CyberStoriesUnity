using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailApplication : BaseApplication
{
    // List of mails, one by filter
    public List<Mail> newMails;
    public List<Mail> archMails;
    public List<Mail> delMails;

    public bool sendMessageOnArchiv = false;
    public bool sendMessageOnDelete = false;

    public BaseQuestManager messageDestination;

    // Nb item in UI
    private const int NbMailDisplayed = 6;
    public List<MailListItem> mailItemUI;

    // Current state of the application
    private Filter currentFilter;
    private int currentIndexInList = 0;
    private List<Mail> currentMailList;

    // Current mail which is displayed
    private Mail mailSelected;

    // Display warning logo
    public bool displayWarningLogo = false;

    #region UI
        public GameObject bodyCanvas;
        public Text mailHeaderObjectText;
        public Text mailHeaderDateText;
        public Text mailHeaderSourceText;
        public Text mailBodyText;
        public Button mailLinkButton;
        public Text mailLinkText;
        public Image warningLogo;
    #endregion

    private enum Filter
    {
        newMail,
        ArchivedMail,
        DeletedMail
    };

    public override void ResetApplication()
    {}

    // Called on scene load, set all text
    private void Awake()
    {
        // Replace text by loca
        const int nbFilter = 3;
        for (int i = 1; i <= nbFilter; ++i)
        {
            GameObject filterCanvas = GameObject.Find("Filter" + i.ToString() + "Button");
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
        currentFilter = Filter.newMail;
        currentMailList = newMails;
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
            mailItemUI[i].ChangeMail(mail);
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

    // Select a mail which is going to be displayed on the right side
    public void SelectMail(Mail mail)
    {
        mailSelected = mail;

        if (mail != null)
        {
            bodyCanvas.SetActive(true);
            mailHeaderSourceText.text = mail.Source;
            mailHeaderObjectText.text = mail.Object;
            mailHeaderDateText.text = mail.DateTime.ToShortDateString();
            mailBodyText.text = mail.Body;

            if (mail.isThereLink)
            {
                mailLinkButton.gameObject.SetActive(true);
                mailLinkText.text = mail.Link;
            }
            else
            {
                mailLinkButton.gameObject.SetActive(false);
                mailLinkText.text = "";
            }

            if (displayWarningLogo && mail.isFishingMail)
                warningLogo.gameObject.SetActive(true);
            else
                warningLogo.gameObject.SetActive(false);
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
            warningLogo.gameObject.SetActive(false);
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
        if (!mailSelected || currentFilter == Filter.ArchivedMail) return;
        
        currentMailList.Remove(mailSelected);
        archMails.Add(mailSelected);
        archMails.Sort((m1, m2) => m2.DateTime.CompareTo(m1.DateTime));

        if (sendMessageOnArchiv)
            messageDestination.gameObject.SendMessage("OnArchivedMail", mailSelected);

        ChangeFilter(currentFilter);
    }

    public void SelectedMailButtonDelete()
    {
        if (!mailSelected || currentFilter == Filter.DeletedMail) return;
        
        currentMailList.Remove(mailSelected);
        delMails.Add(mailSelected);
        delMails.Sort((m1, m2) => m2.DateTime.CompareTo(m1.DateTime));

        if (sendMessageOnDelete)
            messageDestination.gameObject.SendMessage("OnDeletedMail", mailSelected);

        ChangeFilter(currentFilter);
    }

    public void LinkButton()
    {
        globalScreen.LinkClicked(mailSelected.Link);
    }
    #endregion
}
