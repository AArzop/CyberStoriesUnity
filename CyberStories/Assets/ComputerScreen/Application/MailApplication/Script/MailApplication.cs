using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (currentFilter == newFilter)
            return;

        currentFilter = newFilter;
        currentIndexInList = 0;

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
        ChangeFilter(Filter.newMail);
    }

    public void FilterButtonArchive()
    {
        ChangeFilter(Filter.ArchivedMail);
    }

    public void FilterButtonDelete()
    {
        ChangeFilter(Filter.DeletedMail);
    }


    #endregion
}
