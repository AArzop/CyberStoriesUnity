using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhishingReview : MonoBehaviour
{
    public Canvas SuccessCanvas;
    public Canvas FailCanvas;

    public Text SuccessText;
    public Text NoteTitle;
    public Text NoteInput;
    public Text CostText;
    public Text TipsTitle;
    public Text TipsText;
    public Text GameplayTipsText;

    public ClickToChangeScene ToMiniGame;

    private void UnlockMiniGameScene()
    {
        ToMiniGame.Unlock();
        GameplayTipsText.text = GlobalManager.GetLocalization("Review_UnlockGameplay");
    }

    private void GenerateSuccessCanvas()
    {
        SuccessCanvas.gameObject.SetActive(true);
        FailCanvas.gameObject.SetActive(false);

        SuccessText.text = GlobalManager.GetLocalization("PhishingReview_Success");

        NoteInput.text = "100 / 100";
        UnlockMiniGameScene();
    }

    private List<int> GetEvaluationAndCost(PhishingDetails detail)
    {
        List<int> ret = new List<int>();

        int note = 100 - detail.WrongMail * 5 - detail.FishingWebSite * 40;
        note = note < 0 ? 0 : note;

        ret.Add(note);
        ret.Add(detail.FishingWebSite * 650 + detail.WrongMail * 20);

        return ret;
    }

    private void GenerateFailedCanvas(PhishingDetails detail)
    {
        SuccessCanvas.gameObject.SetActive(false);
        FailCanvas.gameObject.SetActive(true);

        const int Note = 0;
        const int Cost = 1;

        List<int> tab = GetEvaluationAndCost(detail);

        NoteTitle.text = GlobalManager.GetLocalization("Review_Note");
        NoteInput.text = tab[Note].ToString() + " / 100";
        TipsTitle.text = GlobalManager.GetLocalization("Review_Tips");
        TipsText.text = GlobalManager.GetLocalization("PhishingReview_Tips");
        CostText.text = GlobalManager.GetLocalization("PhishingReview_Cost");
        CostText.text += " " + tab[Cost].ToString() + "€";

        if (tab[Note] > 75)
            UnlockMiniGameScene();
        else
            GameplayTipsText.text = GlobalManager.GetLocalization("Review_LockGameplay");
    }

    private void Awake()
    {
        if (!(GlobalManager.Details is PhishingDetails detail))
            return;

        NoteTitle.text = GlobalManager.GetLocalization("Review_Note");

        if (detail.WrongMail == 0 && detail.FishingWebSite == 0)
            GenerateSuccessCanvas();
        else
            GenerateFailedCanvas(detail);
    }
}
