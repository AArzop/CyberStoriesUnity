using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketGame : MonoBehaviour
{
    // Prefab which will be initialized 
    public BasketGameBall BallPrefab;
    public List<BaseBasketPlay> GamePrefab;

    public List<float> basketplayScore;
    private int currentIndex;

    // Location of spawn for each prefab
    public Vector3 ballSpawnPosition;
    public Vector3 goalSpawnPosition;

    // object in scene which has been generated from prefab
    private BasketGameBall ball;
    private BaseBasketPlay currentGame;

    public bool isPlaying { get; set; }

    private void Start()
    {
        basketplayScore = new List<float>(GamePrefab.Count);
        isPlaying = false;
    }

    // Initialize object from prefab. Destroy previous objects if they aren t destroyed
    public void NewBasketGame()
    {
        if (isPlaying)
            ResetBasketGame(0f);

        currentIndex = UnityEngine.Random.Range(0, GamePrefab.Count);
        currentGame = Instantiate(GamePrefab[currentIndex], goalSpawnPosition, Quaternion.identity);
        ball = Instantiate(BallPrefab, ballSpawnPosition, Quaternion.identity);
        isPlaying = true;
    }

    // Destroy objects and set variable
    private void ResetBasketGame(float timeBeforeDestroy)
    {
        Destroy(ball.gameObject, timeBeforeDestroy);
        Destroy(currentGame.gameObject, timeBeforeDestroy);

        ball = null;
        currentGame = null;
        currentIndex = -1;
        isPlaying = false;
    }

    // Compute score thanks to time between grab and shoot and distance between goal and player
    private float ComputeScore(float distance, TimeSpan time)
    {
        return 1f;
    }

    // Reset + compute score
    public void EndBasketGame()
    {
        TimeSpan launchingTime = DateTime.Now - ball.launchTime;
        float shootDistance = Vector3.Distance(ball.launchPosition, currentGame.GetGoalPosition());

        basketplayScore[currentIndex] = Math.Max(currentGame.multiplicator * ComputeScore(shootDistance, launchingTime), basketplayScore[currentIndex]);

        ResetBasketGame(1.5f);
    }

    public float EvaluateGame()
    {
        float avg = 0f;
        int nb = 0;
        foreach (var score in basketplayScore)
        {
            if (score != 0f)
            {
                avg += score;
                ++nb;
            }
        }

        if (nb == 0)
            return 0f;

        return avg / nb;
    }
}
