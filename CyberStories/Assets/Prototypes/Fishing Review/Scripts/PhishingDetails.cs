public class PhishingDetails : BaseDetails
{
    public int CorrectMail { get; set; }
    public int WrongMail { get; set; }
    public int FishingWebSite { get; set; }

    public PhishingDetails()
    {
        CorrectMail = 0;
        WrongMail = 0;
        FishingWebSite = 0;
    }
}
