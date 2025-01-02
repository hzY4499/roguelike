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

    // 销毁原因枚举
    public enum DestroyReason
    {
        PlayerAttack, // 被玩家攻击
        Other         // 其他原因（包括撞墙）
    }

    private DestroyReason destroyReason = DestroyReason.Other; // 默认销毁原因

    // 设置销毁原因
    public void SetDestroyReason(DestroyReason reason)
    {
        destroyReason = reason;
    }

    // 获取销毁原因
    public DestroyReason GetDestroyReason()
    {
        return destroyReason;
    }

    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage, bool isCriticalHit)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;

        animator.Play("GetDamage");

        onDamageTaken?.Invoke(damage, transform.position, isCriticalHit);

        if (health <= 0)
        {
            SetDestroyReason(DestroyReason.PlayerAttack); // 被玩家攻击
            PassAway();
        }
    }

    // 检测与墙壁的碰撞并消失
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果碰撞的对象是墙壁，敌人消失
        if (collision.gameObject.CompareTag("Wall"))
        {
            SetDestroyReason(DestroyReason.Other); // 撞墙属于其他原因
            PassAway();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            health--;
            if (health <= 0)
            {
                SetDestroyReason(DestroyReason.PlayerAttack); // 被玩家攻击
                PassAway();
            }
        }
    }

    public void PassAway()
    {
        onPassedAway?.Invoke(transform.position);

        passAwayParticles.transform.parent = null;
        passAwayParticles.Play();
        Destroy(gameObject.transform.parent.gameObject);
    }
}