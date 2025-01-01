using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed;

    protected Vector3 moveDirection;
    [SerializeField] protected ParticleSystem spawnIndicator;
    [SerializeField] protected SpriteRenderer enemyRenderer;
    [SerializeField] protected ParticleSystem passAwayParticles;
    protected float randomX;
    protected float randomY;

    // Start is called before the first frame update
    protected void Start()
    {
        RandomLocation();
        enemyRenderer.enabled = false;

        LeanTween.rotate(spawnIndicator.gameObject, new Vector3(0, 0, 10f), 1f)
            .setOnComplete(SpawnSequenceCompleted);
    }

    protected void SpawnSequenceCompleted()
    {
        enemyRenderer.enabled = true;
        Destroy(spawnIndicator);
        speed = 1f;
    }

    protected abstract void RandomLocation();
}
