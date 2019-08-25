using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketPlay_Size : BaseBasketPlay
{
    private float minSize;
    private float maxSize;
    private float speed;

    private float sign;

    private void Start()
    {
        minSize = Random.Range(0.1f, 0.5f);
        maxSize = Random.Range(0f, 1f) + minSize;
        speed = Random.Range(0f, 1f);
        sign = Random.Range(0f, 1f) > 0.5f ? 1f : -1f;
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
