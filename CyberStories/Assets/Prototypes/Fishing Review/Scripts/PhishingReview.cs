using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PhishingReview : MonoBehaviour
{
    [FormerlySerializedAs("successCanvas")] public Canvas SuccessCanvas;
    [FormerlySerializedAs("failCanvas")] public Canvas FailCanvas;

    [FormerlySerializedAs("successText")] public Text SuccessText;
    [FormerlySerializedAs("noteTitle")] public Text NoteTitle;
    [FormerlySerializedAs("noteInput")] public Text NoteInput;
    [FormerlySerializedAs("costText")] public Text CostText;
    [FormerlySerializedAs("tipsTitle")] public Text TipsTitle;
    [FormerlySerializedAs("tipsText")] public Text TipsText;
    [FormerlySerializedAs("gameplayTipsText")] public Text GameplayTipsText;

    [FormerlySerializedAs("toMiniGame")] public ClickToChangeScene ToMiniGame;

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

        const int note = 0;
        const int cost = 1;

        List<int> tab = GetEvaluationAndCost(detail);

        NoteTitle.text = GlobalManager.GetLocalization("Review_Note");
        NoteInput.text = tab[note].ToString() + " / 100";
        TipsTitle.text = GlobalManager.GetLocalization("Review_Tips");
        TipsText.text = GlobalManager.GetLocalization("PhishingReview_Tips");
        CostText.text = GlobalManager.GetLocalization("PhishingReview_Cost");
        CostText.text += " " + tab[cost].ToString() + "€";

        if (tab[note] > 75)
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
