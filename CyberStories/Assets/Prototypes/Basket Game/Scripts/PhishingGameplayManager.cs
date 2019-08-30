using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class PhishingGameplayManager : MonoBehaviour
{
    [FormerlySerializedAs("nbGoal")] [Range(1, 15)] public int NbGoal;

    [FormerlySerializedAs("nbBall")] [Range(1, 5)] public int NbBall;

    [FormerlySerializedAs("ballPrefab")] public BasketGameBall BallPrefab;
    [FormerlySerializedAs("goalPrefab")] public List<BaseBasketPlay> GoalPrefab;

    [FormerlySerializedAs("bonusTime")] public uint BonusTime = 10;

    [FormerlySerializedAs("prepareTimerText")] public Text PrepareTimerText;
    
    [FormerlySerializedAs("prepareTipsText")] public Text PrepareTipsText;

    [FormerlySerializedAs("prepareCanvas")] public Canvas PrepareCanvas;

    [FormerlySerializedAs("gameTimer")] public List<TextMeshPro> GameTimer;

    [FormerlySerializedAs("platform")] public GameObject Platform;

    [FormerlySerializedAs("nextSceneChanger")] public ClickToChangeScene NextSceneChanger;

    private List<BasketGameBall> ballList;
    private List<BaseBasketPlay> goalList;

    private TimeSpan timer;

    private enum State
    {
        Prepare,
        Game,
        End
    }

    private State currentState;

    [FormerlySerializedAs("goalSpawnArea")] public GameObject GoalSpawnArea;
    
    [FormerlySerializedAs("ballSpawnPosition")] public GameObject BallSpawnPosition;

    private void GenerateBall()
    {
        Vector3 position = new Vector3(
            BallSpawnPosition.transform.position.x + UnityEngine.Random.Range(-0.1f, 0.1f),
            y: BallSpawnPosition.transform.position.y,
            BallSpawnPosition.transform.position.z + UnityEngine.Random.Range(-0.1f, 0.1f));
        ballList.Add(Instantiate(BallPrefab, position, Quaternion.identity));
    }

    private void GenerateGoal()
    {
        Vector3 center = GoalSpawnArea.transform.position;
        Vector3 area = GoalSpawnArea.transform.localScale / 2;
        Vector3 position = new Vector3(center.x + UnityEngine.Random.Range(-1f, 1f) * area.x,
            center.y + UnityEngine.Random.Range(-1f, 1f) * area.y,
            center.z + UnityEngine.Random.Range(-1f, 1f) * area.z);

        int index = UnityEngine.Random.Range(0, GoalPrefab.Count - 1);

        goalList.Add(Instantiate(GoalPrefab[index], position, new Quaternion(0, -90, 0, 0)));
    }

    private IEnumerator BallGeneration()
    {
        for (int i = 0; i < NbBall; i++)
        {
            GenerateBall();
            yield return new WaitForSeconds(1.5f);
        }

        yield return null;
    }

    private IEnumerator GoalGeneration()
    {
        for (int i = 0; i < NbGoal; i++)
        {
            GenerateGoal();
            yield return new WaitForSeconds(0.2f);
        }

        yield return null;
    }

    private void SetupGameTimer()
    {
        string str = (timer.Seconds > 9 ? timer.Seconds.ToString() : "0" + timer.Seconds.ToString()) + ":" +
                     (timer.Milliseconds > 9 ? timer.Milliseconds.ToString() : "0" + timer.Milliseconds.ToString());
        foreach (var item in GameTimer)
            item.text = str;
    }

    private void SetupGameState()
    {
        PrepareCanvas.gameObject.SetActive(false);

        foreach (var item in GameTimer)
            item.gameObject.SetActive(true);

        timer = new TimeSpan(0, 0, 30);
        currentState = State.Game;
        Platform.gameObject.SetActive(true);

        StartCoroutine(BallGeneration());
        StartCoroutine(GoalGeneration());
    }

    private void SetupEndState()
    {
        currentState = State.End;
        foreach (var item in GameTimer)
            item.gameObject.SetActive(false);

        foreach (var ball in ballList)
            Destroy(ball.gameObject);

        foreach (var goal in goalList)
            Destroy(goal.gameObject);

        Platform.SetActive(false);
        NextSceneChanger.gameObject.SetActive(true);
    }

    public void NextState()
    {
        switch (currentState)
        {
            case State.Prepare:
                SetupGameState();
                break;

            case State.Game:
                SetupEndState();
                break;

            case State.End:
                break;
        }
    }

    private void Start()
    {
        currentState = State.Prepare;
        ballList = new List<BasketGameBall>();
        goalList = new List<BaseBasketPlay>();
        timer = new TimeSpan(0, 0, 5);

        Platform.gameObject.SetActive(false);
        NextSceneChanger.gameObject.SetActive(false);
    }

    private void UpdatePreparation()
    {
        timer -= TimeSpan.FromSeconds(Time.deltaTime);
        PrepareTimerText.text = timer.Seconds.ToString();

        if (timer < TimeSpan.Zero)
        {
            NextState();
        }
    }

    private void UpdateGame()
    {
        timer -= TimeSpan.FromSeconds(Time.deltaTime);
        SetupGameTimer();
        if (timer < TimeSpan.Zero)
        {
            NextState();
        }
    }

    private void UpdateEnd()
    {
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Prepare:
                UpdatePreparation();
                break;

            case State.Game:
                UpdateGame();
                break;

            case State.End:
                UpdateEnd();
                break;
        }
    }

    public void Mark(BaseBasketPlay goal, BasketGameBall ball)
    {
        if (goal != null && goalList.Contains(goal))
        {
            goalList.Remove(goal);
            Destroy(goal.gameObject, 0.5f);
            GenerateGoal();
        }

        if (ball != null && ballList.Contains(ball))
        {
            ballList.Remove(ball);
            Destroy(ball.gameObject, 0.5f);
            GenerateBall();
        }
    }
}