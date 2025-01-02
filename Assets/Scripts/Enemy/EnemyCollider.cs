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

    // ����ԭ��ö��
    public enum DestroyReason
    {
        PlayerAttack, // ����ҹ���
        Other         // ����ԭ�򣨰���ײǽ��
    }

    private DestroyReason destroyReason = DestroyReason.Other; // Ĭ������ԭ��

    // ��������ԭ��
    public void SetDestroyReason(DestroyReason reason)
    {
        destroyReason = reason;
    }

    // ��ȡ����ԭ��
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
            SetDestroyReason(DestroyReason.PlayerAttack); // ����ҹ���
            PassAway();
        }
    }

    // �����ǽ�ڵ���ײ����ʧ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ײ�Ķ�����ǽ�ڣ�������ʧ
        if (collision.gameObject.CompareTag("Wall"))
        {
            SetDestroyReason(DestroyReason.Other); // ײǽ��������ԭ��
            PassAway();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            health--;
            if (health <= 0)
            {
                SetDestroyReason(DestroyReason.PlayerAttack); // ����ҹ���
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