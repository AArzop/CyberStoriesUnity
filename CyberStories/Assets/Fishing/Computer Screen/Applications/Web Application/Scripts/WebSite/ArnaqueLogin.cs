public class ArnaqueLogin : BaseWebSite
{
    public override void ResetWebSite()
    {
    }

    public void OnClickedButton()
    {
        messageDestination.SendMessage("OnPhishing", Url);
        RedirectToNextWebSite();
    }
}
