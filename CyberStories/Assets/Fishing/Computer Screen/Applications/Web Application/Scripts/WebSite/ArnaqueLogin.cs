public class ArnaqueLogin : BaseWebSite
{
    public override void ResetWebSite()
    {
    }

    public void OnClickedButton()
    {
        MessageDestination.SendMessage("OnPhishing", Url);
        RedirectToNextWebSite();
    }
}