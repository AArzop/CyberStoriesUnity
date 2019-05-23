using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBasketPlay : MonoBehaviour
{
    private PhishingGameplayManager game;
    public BoxCollider goal;

    protected void Awake()
    {
        game = FindObjectOfType<PhishingGameplayManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            var obj = GameObject.Find(other.name);
            BasketGameBall ball = obj.GetComponent<BasketGameBall>();
            if (ball != null && !ball.isGrabbed)
                game.Mark(this, ball);
        }
    }

    public Vector3 GetGoalPosition()
    {
        return goal.transform.position;
    }

    protected abstract void Update();
}
