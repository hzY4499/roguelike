using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0 : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 moveDirection;
    [SerializeField] private ParticleSystem spawnIndicator;
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private ParticleSystem passAwayParticles;
    float randomX;
    float randomY;

    void Start()
    {       
        RandomLocation();
        enemyRenderer.enabled = false;

        LeanTween.rotate(spawnIndicator.gameObject, new Vector3(0, 0, 10f), 1f)
            .setOnComplete(SpawnSequenceCompleted);
    }

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void SpawnSequenceCompleted()
    {
        enemyRenderer.enabled = true;
        spawnIndicator.Stop();
        spawnIndicator.transform.parent = null;
        speed = 1f;
    }

    private void RandomLocation()
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