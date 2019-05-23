using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketPlay_Size : BaseBasketPlay
{
    public float minSize;
    public float maxSize;
    public float speed;

    public bool firstIncrease = true;

    private float sign;

    private void Start()
    {
        sign = firstIncrease ? 1f : -1f;
    }

    protected override void Update()
    {
        float size = transform.localScale.x; // x, y, z are equals

        if (size > maxSize || size < minSize)
            sign = -sign;

        size += sign * speed * Time.deltaTime;

        transform.localScale = new Vector3(size, size, size);
    }
}
