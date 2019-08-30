using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

public class PhishingMainQuest : BaseQuest
{
    [FormerlySerializedAs("mailApp")] public MailApplication MailApp;
    private Dictionary<Mail, bool> mailTreated;

    [FormerlySerializedAs("webSites")] public List<BaseWebSite> WebSites;
    private Dictionary<BaseWebSite, bool> siteConsulted;
    private List<string> fishingWebsiteConsulted;

    private PhishingDetails details;

    private new void Awake()
    {
        base.Awake();

        mailTreated = new Dictionary<Mail, bool>();
        siteConsulted = new Dictionary<BaseWebSite, bool>();
        siteConsulted = new Dictionary<BaseWebSite, bool>();
        fishingWebsiteConsulted = new List<string>();

        details = GlobalManager.Details as PhishingDetails;
        foreach (var site in WebSites)
            siteConsulted[site] = false;
    }

    public override void SetupQuest()
    {
    }

    public override void CheckQuest()
    {
        if (MailApp.NewMails.Count != 0)
            return;

        if (siteConsulted.Any(pair => !pair.Value))
        {
            return;
        }

        EndQuest();
        QuestManager.FulfillStep();
    }

    public override void EndQuest()
    {
    }

    public override void EvaluateQuest()
    {
        if (details == null)
            return;

        foreach (KeyValuePair<Mail, bool> pair in mailTreated)
        {
            if (pair.Value)
                details.CorrectMail++;
            else
                details.WrongMail++;
        }
    }

    public void OnArchivedMail(Mail mail)
    {
        mailTreated[mail] = !mail.IsFishingMail;
        CheckQuest();
    }

    public void OnDeletedMail(Mail mail)
    {
        mailTreated[mail] = mail.IsFishingMail;
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
        if (details == null || fishingWebsiteConsulted.Contains(url))
            return;

        details.FishingWebSite++;
        fishingWebsiteConsulted.Add(url);
    }

    public override string GetQuestInformation()
    {
        return InfoQuest;
    }
}