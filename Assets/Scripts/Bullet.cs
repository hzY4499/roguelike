using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float aliveTime;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aliveTime < 0) Destroy(gameObject);
        aliveTime -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞对象是否是墙体
        if (collision.gameObject.CompareTag("Wall"))
        {
            // 销毁子弹
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy") && collision.GetComponent<Renderer>().enabled) // 确保敌人生成特效不会触发
        {
            Destroy(gameObject);
            int curDamage = GetDamage(out bool isCriticalHit);
            collision.gameObject.GetComponent<EnemyCollider>().TakeDamage(curDamage, isCriticalHit);
        }
    }

    private int GetDamage(out bool isCriticalHit)
    {
        isCriticalHit = false;

        if (Random.Range(0,101) <= player.criticalRate)
        {
            isCriticalHit = true;
            return (int)(damage * player.criticalDamage);
        }

        return damage;
    }
}
