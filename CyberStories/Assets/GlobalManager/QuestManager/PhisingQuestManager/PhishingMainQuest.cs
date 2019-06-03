using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhishingMainQuest : BaseQuest
{
    public MailApplication mailApp;
    private Dictionary<Mail, bool> mailTreated;

    public List<BaseWebSite> webSites;
    private Dictionary<BaseWebSite, bool> siteConsulted;
    private List<string> phishingwebSiteConsulted;


    private PhishingDetails details;
    

    private new void Awake()
    {
        base.Awake();

        mailTreated = new Dictionary<Mail, bool>();
        siteConsulted = new Dictionary<BaseWebSite, bool>();
        siteConsulted = new Dictionary<BaseWebSite, bool>();
        phishingwebSiteConsulted = new List<string>();

        details = GlobalManager.details as PhishingDetails;
        foreach (var site in webSites)
            siteConsulted[site] = false;
    }

    public override void SetupQuest()
    {
    }

    public override void CheckQuest()
    {
        if (mailApp.newMails.Count != 0)
            return;

        foreach (var pair in siteConsulted)
        {
            if (!pair.Value)
                return;
        }

        EndQuest();
        questManager.FullfillStep();
    }

    public override void EndQuest()
    {
    }

    public override void EvaluateQuest()
    {
        if (details == null)
            return;

        foreach (var pair in mailTreated)
        {
            if (pair.Value)
                details.correctMail++;
            else
                details.wrongMail++;
        }
    }

    public void OnArchivedMail(Mail mail)
    {
        mailTreated[mail] = !mail.isPhishingMail;
        CheckQuest();
    }

    public void OnDeletedMail(Mail mail)
    {
        mailTreated[mail] = mail.isPhishingMail;
        CheckQuest();
    }

    public void OnWebSiteEnter(BaseWebSite site)
    {
        if (siteConsulted.ContainsKey(site))
        {
            siteConsulted[site] = true;
            CheckQuest();
        }
    }

    public void OnPhishing(string url)
    {
        if (details == null || phishingwebSiteConsulted.Contains(url))
            return;

        details.phishingWebSite++;
        phishingwebSiteConsulted.Add(url);
    }

    public override string GetQuestInformation()
    {
        return infoQuest;
    }
}
