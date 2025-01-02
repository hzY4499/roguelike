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
    protected GameManager gameManager;
    public int score;

    protected EnemyCollider enemyCollider;

    // Start is called before the first frame update
    protected void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        enemyCollider = GetComponentInChildren<EnemyCollider>();
        RandomLocation();
        enemyRenderer.enabled = false;

        LeanTween.rotate(spawnIndicator.gameObject, new Vector3(0, 0, 10f), 1f)
            .setOnComplete(SpawnSequenceCompleted);
    }

    protected void OnDestroy()
    {
        // 只有在被玩家攻击时才加分
        if (enemyCollider != null && enemyCollider.GetDestroyReason() == EnemyCollider.DestroyReason.PlayerAttack)
        {
            gameManager.currentScore += score;
        }
    }

    protected void Update()
    {
        if (gameManager.isOver) moveDirection = Vector2.zero;
    }

    protected void SpawnSequenceCompleted()
    {
        enemyRenderer.enabled = true;
        Destroy(spawnIndicator);
        speed = 1f;
    }

    protected abstract void RandomLocation();
}