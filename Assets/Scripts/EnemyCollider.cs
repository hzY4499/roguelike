using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    [SerializeField] private ParticleSystem passAwayParticles;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞对象是否是子弹
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // 销毁子弹
            // Destory(collision.gameObject);

            PassAway();
        }
    }

    private void PassAway()
    {
        passAwayParticles.transform.parent = null;
        passAwayParticles.Play();
        Destroy(gameObject.transform.parent.gameObject);
    }
}
