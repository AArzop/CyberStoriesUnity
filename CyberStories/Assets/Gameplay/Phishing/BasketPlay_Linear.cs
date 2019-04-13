using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketPlay_Linear : BaseBasketPlay
{
    public Vector3 speed;
    public Vector3 offset;

    private Vector3 currentOffset;
    private Vector3 sign;

    private void Start()
    {
        currentOffset = new Vector3(offset.x / 2, offset.y / 2, offset.z / 2);
        sign = new Vector3(1, 1, 1);
    }

    protected override void Update()
    {
        Vector3 pos = transform.position;

        for (int i = 0; i < 3; ++i)
        {
            if (currentOffset[i] > offset[i])
            {
                currentOffset[i] = 0;
                sign[i] = -sign[i];
            }

            currentOffset[i] += speed[i] * Time.deltaTime;
            pos[i] += sign[i] * speed[i] * Time.deltaTime;
        }

        transform.position = pos;
    }
}
