using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float aliveTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        //aliveTime = 1.5f;
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
    }
}
