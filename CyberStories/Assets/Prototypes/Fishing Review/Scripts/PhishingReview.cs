using System.Collections;
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

    public ClickToChangeScene toMinigame;

    private int FinalNote;

    private void UnlockMinigameScene()
    {
        toMinigame.Unlock();
        gameplayTipsText.text = GlobalManager.GetLocalization("Review_UnlockGameplay");
    }

    private void GenerateSuccessCanvas()
    {
        FinalNote = 100;
        successCanvas.gameObject.SetActive(true);
        failCanvas.gameObject.SetActive(false);

        successText.text = GlobalManager.GetLocalization("PhishingReview_Success");

        noteInput.text = "100 / 100";
        UnlockMinigameScene();
    }

    private List<int> GetEvaluationAndCost(PhishingDetails detail)
    {
        List<int> ret = new List<int>();

        int note = 100 - detail.wrongMail * 5 - detail.phishingWebSite * 40;
        note = note < 0 ? 0 : note;

        ret.Add(note);
        ret.Add(detail.phishingWebSite * 650 + detail.wrongMail * 20);

        return ret;
    }

    private void GenerateFailedCanvas(PhishingDetails detail)
    {
        successCanvas.gameObject.SetActive(false);
        failCanvas.gameObject.SetActive(true);

        const int note = 0;
        const int cost = 1;

        List<int> tab = GetEvaluationAndCost(detail);
        FinalNote = tab[note];

        noteTitle.text = GlobalManager.GetLocalization("Review_Note");
        noteInput.text = tab[note].ToString() + " / 100";
        tipsTitle.text = GlobalManager.GetLocalization("Review_Tips");
        tipsText.text = GlobalManager.GetLocalization("PhishingReview_Tips");
        costText.text = GlobalManager.GetLocalization("PhishingReview_Cost");
        costText.text += " " + tab[cost].ToString() + "€";

        if (tab[note] > 75)
            UnlockMinigameScene();
        else
            gameplayTipsText.text = GlobalManager.GetLocalization("Review_LockGameplay");
    }

    void Awake()
    {
        PhishingDetails detail = GlobalManager.details as PhishingDetails;
        if (detail == null)
            return;

        noteTitle.text = GlobalManager.GetLocalization("Review_Note");

        if (detail.wrongMail == 0 && detail.phishingWebSite == 0)
            GenerateSuccessCanvas();
        else
            GenerateFailedCanvas(detail);

        GlobalManager.RegisterScore(GlobalManager.Level.Phishing, FinalNote);
    }
}
