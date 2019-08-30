public class PhishingQuestManager : BaseQuestManager
{
    private new void Awake()
    {
        base.Awake();
    }

   // Received message from mailApplication, button "Archive mail" is clicked
    public void OnArchivedMail(Mail mail)
    {
        CurrentStep.gameObject.SendMessage("OnArchivedMail", mail);
    }

    // Received message from mailApplication, button "Delete mail" is clicked
    public void OnDeletedMail(Mail mail)
    {
        CurrentStep.gameObject.SendMessage("OnDeletedMail", mail);
    }

    // Received message form a website (webApplication), user have consulted a web site
    public void OnWebSiteEnter(BaseWebSite site)
    {
        CurrentStep.gameObject.SendMessage("OnWebSiteEnter", site);
    }

    public void OnPhishing(string url)
    {
        CurrentStep.gameObject.SendMessage("OnPhishing", url);

    }

    protected override void EndLevel()
    {
    }
}
