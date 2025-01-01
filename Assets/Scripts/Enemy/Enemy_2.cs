using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    private Player player;

    new void Start()
    {
        base.Start();
        player = FindFirstObjectByType<Player>();
    }

    protected void Update()
    {
        moveDirection = (player.transform.position - transform.position).normalized;
        transform.position += moveDirection * speed * Time.deltaTime;
        transform.Rotate(new Vector3(0, 0, 50f) * Time.deltaTime);
    }  

    protected override void RandomLocation()
    {
        if (Random.value > 0.5f)
        {
            randomX = Random.value > 0.5f ? -4.5f : 4.5f;
            randomY = Random.Range(-4f, 4f); 
        }
        else
        {
            randomY = Random.value > 0.5f ? -4f : 4f;
            randomX = Random.Range(-4.5f, 4.5f); 
        }

        transform.position = new Vector3(randomX, randomY, 0);
    }
}