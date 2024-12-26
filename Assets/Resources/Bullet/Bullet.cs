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
        // �����ײ�����Ƿ���ǽ��
        if (collision.gameObject.CompareTag("Wall"))
        {
            // �����ӵ�
            Destroy(gameObject);
        }
    }
}
