using System.Collections.Generic;
using System.Linq;

public class PhishingMainQuest : BaseQuest
{
    public MailApplication mailApp;
    private Dictionary<Mail, bool> mailTreated;

    public List<BaseWebSite> webSites;
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

        if (siteConsulted.Any(pair => !pair.Value))
        {
            return;
        }

        EndQuest();
        questManager.FulfillStep();
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
        mailTreated[mail] = !mail.isFishingMail;
        CheckQuest();
    }

    public void OnDeletedMail(Mail mail)
    {
        mailTreated[mail] = mail.isFishingMail;
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
        return infoQuest;
    }
}