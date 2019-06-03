﻿using System;
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

    public GameObject platform;

    public ClickToChangeScene nextSceneChanger;

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
        Vector3 position = new Vector3(ballSpawnPoisition.transform.position.x + UnityEngine.Random.Range(-0.1f, 0.1f), ballSpawnPoisition.transform.position.y, ballSpawnPoisition.transform.position.z + UnityEngine.Random.Range(-0.1f, 0.1f));
        ballList.Add(Instantiate(ballPrefab, position, Quaternion.identity));
    }

    private void GenerateGoal()
    {
        Vector3 center = goalSpawnArea.transform.position;
        Vector3 area = goalSpawnArea.transform.localScale / 2;
        Vector3 position = new Vector3(center.x + UnityEngine.Random.Range(-1f, 1f) * area.x, center.y + UnityEngine.Random.Range(-1f, 1f) * area.y, center.z + UnityEngine.Random.Range(-1f, 1f) * area.z);

        int index = UnityEngine.Random.Range(0, goalPrefab.Count - 1);

        goalList.Add(Instantiate(goalPrefab[index], position, new Quaternion(0, -90, 0, 0)));
    }

    private IEnumerator BallGeneration()
    {
        for (int i = 0; i < nbBall; i++)
        {
            GenerateBall();
            yield return new WaitForSeconds(1.5f);
        }

        yield return null;
    }

    private IEnumerator GoalGeneration()
    {
        for (int i = 0; i < nbGoal; i++)
        {
            GenerateGoal();
            yield return new WaitForSeconds(0.2f);
        }

        yield return null;
    }

    private void SetupGameState()
    {
        timer = new TimeSpan(0, 0, 30 + (int) (bonusTime * 0)); // Change 0
        currentState = State.Game;
        platform.gameObject.SetActive(true);

        StartCoroutine(BallGeneration());
        StartCoroutine(GoalGeneration());
    }

    private void SetupEndState()
    {
        currentState = State.End;

        foreach (var ball in ballList)
            Destroy(ball.gameObject);

        foreach (var goal in goalList)
            Destroy(goal.gameObject);

        platform.SetActive(false);
        nextSceneChanger.gameObject.SetActive(true);
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

        platform.gameObject.SetActive(false);
        nextSceneChanger.gameObject.SetActive(false);
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
