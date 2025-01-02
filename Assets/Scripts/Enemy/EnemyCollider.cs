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

    // �����ǽ�ڵ���ײ����ʧ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ײ�Ķ�����ǽ�ڣ�������ʧ
        if (collision.gameObject.CompareTag("Wall"))
        {
            // ���ٵ��˶���
            PassAway();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            health--;
            if (health <= 0) PassAway();
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
