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

    // Evaluate level score
    // Score is a combinaison of each steps and basketBall minigame
    public override float GetLevelEvaluation()
    {
        float evaluation = 0f;

        foreach (var pair in stepsScore)
            evaluation += pair.Value;

        evaluation += basketGame.EvaluateGame();

        return evaluation;
    }

   // Received message from mailApplication, button "Archive mail" is clicked
    public void OnArchivedMail(Mail mail)
    {
        currentStep.gameObject.SendMessage("OnDeletedMail", mail);
        basketGame.NewBasketGame();
    }

    // Received message from mailApplication, button "Delete mail" is clicked
    public void OnDeletedMail(Mail mail)
    {
        currentStep.gameObject.SendMessage("OnDeletedMail", mail);
        basketGame.NewBasketGame();
    }

    // Received message form a website (webApplication), user have consulted a web site
    public void OnWebSiteEnter(BaseWebSite site)
    {
        currentStep.gameObject.SendMessage("OnWebSiteEnter", site);
    }

    public void OnPhishing(string url)
    {
        int i = 1;
    }

    protected override void EndLevel()
    {
    }
}
