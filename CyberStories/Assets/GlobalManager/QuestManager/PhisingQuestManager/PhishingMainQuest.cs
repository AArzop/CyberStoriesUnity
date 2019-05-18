using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhishingMainQuest : BaseQuest
{
    public MailApplication mailApp;
    private Dictionary<Mail, bool> mailTreated;

    public List<BaseWebSite> webSites;
    private Dictionary<BaseWebSite, bool> siteConsulted;

    private const float successScore = 10f;
    private const float failScore = -10f;

    private new void Awake()
    {
        base.Awake();

        mailTreated = new Dictionary<Mail, bool>();
        siteConsulted = new Dictionary<BaseWebSite, bool>();
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

    public override float EvaluateQuest()
    {
        float evaluation = 0f;

        foreach (var pair in mailTreated)
        {
            if (pair.Value)
                evaluation += successScore;
            else
                evaluation += failScore;
        }

        return evaluation;
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

    public override string GetQuestInformation()
    {
        return infoQuest;
    }
}
