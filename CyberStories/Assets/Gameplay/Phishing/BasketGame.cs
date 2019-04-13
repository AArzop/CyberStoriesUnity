using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketGame : MonoBehaviour
{
    public BasketGameBall BallPrefab;
    public List<BaseBasketPlay> GamePrefab;

    public Vector3 ballSpawnPosition;
    public Vector3 goalSpawnPosition;

    private BasketGameBall ball;
    private BaseBasketPlay currentGame;

    public bool isPlaying { get; set; }

    public float Score { get; set; }

    private void Start()
    {
        Score = 0f;
        isPlaying = false;
    }

    public void newBasketGame()
    {
        if (isPlaying)
            ResetBasketGame(0f);

        currentGame = Instantiate(GamePrefab[UnityEngine.Random.Range(0, GamePrefab.Count)], goalSpawnPosition, Quaternion.identity);
        ball = Instantiate(BallPrefab, ballSpawnPosition, Quaternion.identity);
        isPlaying = true;
    }

    private void ResetBasketGame(float timeBeforeDestroy)
    {
        Destroy(ball.gameObject, timeBeforeDestroy);
        Destroy(currentGame.gameObject, timeBeforeDestroy);

        ball = null;
        currentGame = null;
        isPlaying = false;
    }

    private float ComputeScore(float distance, TimeSpan time)
    {
        return 1f;
    }

    public void endBasketGame()
    {
        TimeSpan launchingTime = DateTime.Now - ball.launchTime;
        float shootDistance = Vector3.Distance(ball.launchPosition, currentGame.GetGoalPosition());

        Score += ComputeScore(shootDistance, launchingTime);

        ResetBasketGame(1.5f);
    }
}
