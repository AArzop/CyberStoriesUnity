using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhishingQuestManager : BaseQuestManager
{
    private BasketGame basketGame;

    private new void Awake()
    {
        base.Awake();
        basketGame = GetComponent<BasketGame>();
    }

    public override float GetLevelEvaluation()
    {
        float evaluation = 0f;

        foreach (var pair in stepsScore)
            evaluation += pair.Value;

        return evaluation;
    }

    public void OnArchivedMail(Mail mail)
    {
        currentStep.gameObject.SendMessage("OnDeletedMail", mail);
        basketGame.newBasketGame();
    }

    public void OnDeletedMail(Mail mail)
    {
        currentStep.gameObject.SendMessage("OnDeletedMail", mail);
        basketGame.endBasketGame();
    }

    public void OnWebSiteEnter(BaseWebSite site)
    {
        currentStep.gameObject.SendMessage("OnWebSiteEnter", site);
    }
}
