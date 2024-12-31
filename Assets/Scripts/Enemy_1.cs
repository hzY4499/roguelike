using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    
    private float moveDelay = 1f;
    private float delayTimer;
    private float moveTime = 0.2f;
    private float moveTimer;
    private bool isMoving;
    new void Start()
    {
        base.Start();
        delayTimer = moveDelay;
        moveTimer = moveTime;
        isMoving = false;
    }
    void Update()
    {
        if (isMoving)
        {
            moveTimer -= Time.deltaTime;
            transform.position += moveDirection * speed * Time.deltaTime * moveDelay / moveTime;
            if (moveTimer <= 0)
            {
                moveTimer = moveTime;
                isMoving = false;
            }
        }else
        {
            delayTimer -= Time.deltaTime;
            if (delayTimer <= 0)
            {
                delayTimer = moveDelay;
                isMoving = true;
            }
        }
    }

    protected override void RandomLocation()
    {
        Vector3[] corners = new Vector3[]
        {
            new Vector3(-4.5f, -4f, 0),
            new Vector3(-4.5f, 4f, 0),
            new Vector3(4.5f, -4f, 0),
            new Vector3(4.5f, 4f, 0)
        };

        if (Random.value > 0.5f)
        {
            if (Random.value > 0.5f)
            {
                randomX = -4.5f;
                moveDirection = Vector3.right;
            }
            else
            {
                randomX = 4.5f;
                moveDirection = Vector3.left;
            }
            randomY = Random.Range(-4f, 4f);
        }
        else
        {
            if (Random.value > 0.5f)
            {
                randomY = -4f;
                moveDirection = Vector3.up;
            }
            else
            {
                randomY = 4f;
                moveDirection = Vector3.down;
            }
            randomX = Random.Range(-4.5f, 4.5f);
        }
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);
        // 设置移动方向
        int r = randomX > 0 ? 0 : 1;
        int c = randomY > 0 ? 0 : 1;
        moveDirection = (corners[r * 2 + c] - spawnPosition).normalized;

        transform.position = spawnPosition;
    }
}