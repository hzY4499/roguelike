using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    [SerializeField] private ParticleSystem passAwayParticles;

    [SerializeField] private int maxHealth;
    private int health;

    public PlayerLevel playerLevel;  // 引用 PlayerLevel 脚本
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

    // 检测与墙壁的碰撞并消失
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果碰撞的对象是墙壁，敌人消失
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player"))
        {
            // 销毁敌人对象
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
