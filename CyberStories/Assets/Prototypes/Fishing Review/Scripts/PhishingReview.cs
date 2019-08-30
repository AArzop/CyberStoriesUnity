using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhishingReview : MonoBehaviour
{
    public Canvas successCanvas;
    public Canvas failCanvas;

    public Text successText;
    public Text noteTitle;
    public Text noteInput;
    public Text costText;
    public Text tipsTitle;
    public Text tipsText;
    public Text gameplayTipsText;

    public ClickToChangeScene toMiniGame;

    private void UnlockMiniGameScene()
    {
        toMiniGame.Unlock();
        gameplayTipsText.text = GlobalManager.GetLocalization("Review_UnlockGameplay");
    }

    private void GenerateSuccessCanvas()
    {
        successCanvas.gameObject.SetActive(true);
        failCanvas.gameObject.SetActive(false);

        successText.text = GlobalManager.GetLocalization("PhishingReview_Success");

        noteInput.text = "100 / 100";
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
        successCanvas.gameObject.SetActive(false);
        failCanvas.gameObject.SetActive(true);

        const int note = 0;
        const int cost = 1;

        List<int> tab = GetEvaluationAndCost(detail);

        noteTitle.text = GlobalManager.GetLocalization("Review_Note");
        noteInput.text = tab[note].ToString() + " / 100";
        tipsTitle.text = GlobalManager.GetLocalization("Review_Tips");
        tipsText.text = GlobalManager.GetLocalization("PhishingReview_Tips");
        costText.text = GlobalManager.GetLocalization("PhishingReview_Cost");
        costText.text += " " + tab[cost].ToString() + "€";

        if (tab[note] > 75)
            UnlockMiniGameScene();
        else
            gameplayTipsText.text = GlobalManager.GetLocalization("Review_LockGameplay");
    }

    private void Awake()
    {
        if (!(GlobalManager.Details is PhishingDetails detail))
            return;

        noteTitle.text = GlobalManager.GetLocalization("Review_Note");

        if (detail.WrongMail == 0 && detail.FishingWebSite == 0)
            GenerateSuccessCanvas();
        else
            GenerateFailedCanvas(detail);
    }
}
