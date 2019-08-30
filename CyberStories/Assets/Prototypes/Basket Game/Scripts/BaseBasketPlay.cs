using UnityEngine;

public abstract class BaseBasketPlay : MonoBehaviour
{
    private PhishingGameplayManager game;
    public BoxCollider Goal;

    protected void Awake()
    {
        game = FindObjectOfType<PhishingGameplayManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            GameObject obj = GameObject.Find(other.name);
            BasketGameBall ball = obj.GetComponent<BasketGameBall>();
            if (ball != null && !ball.IsGrabbed)
                game.Mark(this, ball);
        }
    }

    public Vector3 GetGoalPosition()
    {
        return Goal.transform.position;
    }

    protected abstract void Update();
}