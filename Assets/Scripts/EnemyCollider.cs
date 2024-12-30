using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    [SerializeField] private ParticleSystem passAwayParticles;

    [SerializeField] private int maxHealth;
    private int health;

    public PlayerLevel playerLevel;  // ���� PlayerLevel �ű�
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        playerLevel = FindAnyObjectByType<PlayerLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;

        if (health <= 0) PassAway();
    }

    // �����ǽ�ڵ���ײ����ʧ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ײ�Ķ�����ǽ�ڣ�������ʧ
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player"))
        {
            // ���ٵ��˶���
            PassAway();
        }
    }

    private void PassAway()
    {
        passAwayParticles.transform.parent = null;
        passAwayParticles.Play();
        playerLevel.OnEnemyKilled();
        Destroy(gameObject.transform.parent.gameObject);
    }
}
