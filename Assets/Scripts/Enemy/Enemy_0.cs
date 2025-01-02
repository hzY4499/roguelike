using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0 : Enemy
{
    new void Start()
    {
        base.Start();
        score = 1;
    }
    
    new void Update()
    {
        base.Update();
        transform.position += moveDirection * speed * Time.deltaTime;
    }  

    protected override void RandomLocation()
    {
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

        transform.position = new Vector3(randomX, randomY, 0);
    }
}