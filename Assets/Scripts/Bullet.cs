using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float aliveTime;
    // Start is called before the first frame update
    void Start()
    {

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
            collision.gameObject.GetComponent<EnemyCollider>().TakeDamage(damage);
        }
    }
}
