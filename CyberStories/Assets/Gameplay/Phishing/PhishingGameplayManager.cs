using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhishingGameplayManager : MonoBehaviour
{
    [Range(1, 15)]
    public int nbGoal;

    [Range(1, 5)]
    public int nbBall;

    public BasketGameBall ballPrefab;
    public List<BaseBasketPlay> goalPrefab;

    public uint bonusTime = 10;

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

    public GameObject goalSpawnArea;
    public GameObject ballSpawnPoisition;

    private void GenerateBall()
    {
        ballList.Add(Instantiate(ballPrefab, ballSpawnPoisition.transform.position, Quaternion.identity));
    }

    private void GenerateGoal()
    {
        Vector3 center = goalSpawnArea.transform.position;
        Vector3 area = goalSpawnArea.transform.localScale / 2;
        Vector3 position = new Vector3(center.x + UnityEngine.Random.Range(-1f, 1f) * area.x, center.y + UnityEngine.Random.Range(-1f, 1f) * area.y, center.z + UnityEngine.Random.Range(-1f, 1f) * area.z);

        int index = UnityEngine.Random.Range(0, goalList.Count - 1);

        goalList.Add(Instantiate(goalPrefab[index], position, new Quaternion(0, -90, 0, 0)));
    }

    private void SetupGameState()
    {
        timer = new TimeSpan(0, 0, 30 + (int) (bonusTime * 0)); // Change 0
        currentState = State.Game;

        for (int i = 0; i < nbBall; i++)
            GenerateBall();

        for (int i = 0; i < nbGoal; i++)
            GenerateGoal();
    }

    private void SetupEndState()
    {
        currentState = State.End;

        foreach (var ball in ballList)
            Destroy(ball.gameObject);

        foreach (var goal in goalList)
            Destroy(goal.gameObject);
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

            default:
                break;
        }
    }

    void Start()
    {
        currentState = State.Prepare;
        ballList = new List<BasketGameBall>();
        goalList = new List<BaseBasketPlay>();
        timer = new TimeSpan(0, 0, 5);
    }

    private void UpdatePreparation()
    {
        timer -= TimeSpan.FromSeconds(Time.deltaTime);
        if (timer < TimeSpan.Zero)
        {
            NextState();
            return;
        }
    }

    private void UpdateGame()
    {
        timer -= TimeSpan.FromSeconds(Time.deltaTime);
        if (timer < TimeSpan.Zero)
        {
            NextState();
            return;
        }
    }

    private void UpdateEnd()
    {
    }

    void Update()
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

            default:
                break;
        }
    }

    public void Mark(BaseBasketPlay goal, BasketGameBall ball)
    {

    }
}
