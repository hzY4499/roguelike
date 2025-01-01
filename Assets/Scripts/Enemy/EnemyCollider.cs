using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyCollider : MonoBehaviour
{
    [SerializeField] private ParticleSystem passAwayParticles;

    [SerializeField] private int maxHealth;
    private int health;

    [SerializeField] private Animator animator;

    public static Action<int, Vector2, bool> onDamageTaken;
    public static Action<Vector2> onPassedAway;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage, bool isCriticalHit)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;

        animator.Play("GetDamage");

        onDamageTaken?.Invoke(damage, transform.position, isCriticalHit);

        if (health <= 0) PassAway();
    }

    // 检测与墙壁的碰撞并消失
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果碰撞的对象是墙壁，敌人消失
        if (collision.gameObject.CompareTag("Wall"))
        {
            // 销毁敌人对象
            PassAway();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            health--;
            if (health <= 0) PassAway();
        }
    }

    private void PassAway()
    {
        onPassedAway?.Invoke(transform.position);
        
        passAwayParticles.transform.parent = null;
        passAwayParticles.Play();
        Destroy(gameObject.transform.parent.gameObject);
    }
}
