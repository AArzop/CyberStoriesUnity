using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhishingGameplayManager : MonoBehaviour
{
    [Range(1, 25)]
    public int nbGoal;

    [Range(1, 10)]
    public int nbBall;

    public GameObject ballPrefab;
    public GameObject goalPrefab;

    public uint bonusTime = 10;

    private List<GameObject> ballList;
    private List<GameObject> goalList;

    private TimeSpan timer;
    private enum State
    {
        Prepare,
        Game,
        End
    }
    private State currentState;

    public GameObject goalSpawnArea;
    public Vector3 ballSpawnPoisition;

    private void GenerateBall()
    {
        ballList.Add(GameObject.Instantiate(ballPrefab, ballSpawnPoisition, Quaternion.identity));
    }

    private void GenerateGoal()
    {
        Vector3 center = goalSpawnArea.transform.position;
        Vector3 area = goalSpawnArea.transform.localScale / 2;

        Vector3 position = new Vector3(center.x + UnityEngine.Random.Range(-1f, 1f) * area.x, center.y + UnityEngine.Random.Range(-1f, 1f) * area.y, center.z + UnityEngine.Random.Range(-1f, 1f) * area.z);

        goalList.Add(GameObject.Instantiate(goalPrefab, position, Quaternion.identity));
    }

    private void SetupGameState()
    {
        timer = new TimeSpan(0, 0, 10  + (int) (bonusTime * 0)); // Change 0
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
        Console.WriteLine("Next State");
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
        timer = new TimeSpan(0, 0, 15);
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
}
