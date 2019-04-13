using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBasketPlay : MonoBehaviour
{
    private BasketGame game;
    public BoxCollider goal;

    public float multiplicator = 1f;

    protected void Awake()
    {
        game = FindObjectOfType<BasketGame>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            var a = GameObject.Find(other.name);
            BasketGameBall ball = a.GetComponent<BasketGameBall>();
            if (ball != null && !ball.isGrabbed)
                game.endBasketGame();
        }
    }

    public Vector3 GetGoalPosition()
    {
        return goal.transform.position;
    }

    protected abstract void Update();
}
