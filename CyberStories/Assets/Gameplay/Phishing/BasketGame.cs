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

    // Location and rotation of prefab when it will be generated
    public GameObject ballSpawn;
    public GameObject goalSpawn;

    // object in scene which has been generated from prefab
    private BasketGameBall ball;
    private BaseBasketPlay currentGame;

    public bool isPlaying { get; set; }

    private void Start()
    {
        basketplayScore = new List<float>();
        foreach (var item in GamePrefab)
            basketplayScore.Add(0f);

        isPlaying = false;
    }

    // Initialize object from prefab. Destroy previous objects if they aren t destroyed
    public void NewBasketGame()
    {
        if (isPlaying)
            ResetBasketGame(0f);

        currentIndex = UnityEngine.Random.Range(0, GamePrefab.Count);
        currentGame = Instantiate(GamePrefab[currentIndex], goalSpawn.transform.position, goalSpawn.transform.rotation);
        ball = Instantiate(BallPrefab, ballSpawn.transform.position, ballSpawn.transform.rotation);
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
        // if player manages to score, score will be at least 10
        // Score: 45 => distance + 45 => time + 10 => success
        float score = 10f;

        const float minDistance = 0f;
        const float maxDistance = 10f;

        if (distance >= maxDistance)
            distance = maxDistance;
        else if (distance < minDistance) // should never happened
            distance = minDistance;

        score += ((distance - minDistance) / (maxDistance - minDistance)) * 45;

        TimeSpan minTime = new TimeSpan(0, 0, 8);
        TimeSpan maxTime = new TimeSpan(0, 1, 4);

        if (time < minTime)
            score += 45f;
        else if (time > maxTime)
            score += 1f;
        else
        {
            time.Add(new TimeSpan(0, 0, 0, 0, 500));
            int nbSecond = (int) time.TotalSeconds;
            int minSecond = (int) minTime.TotalSeconds;
            int maxSecond = (int) maxTime.TotalSeconds;

            float coef = (nbSecond / minSecond) / (maxSecond / minSecond);

            score += 45f * (1f - coef);
        }

        return score;
    }

    // Reset + compute score
    public void EndBasketGame()
    {
        TimeSpan launchingTime = DateTime.Now - ball.launchTime;
        float shootDistance = Vector3.Distance(ball.launchPosition, currentGame.GetGoalPosition());

        basketplayScore[currentIndex] = Math.Max(ComputeScore(shootDistance, launchingTime), basketplayScore[currentIndex]);

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
