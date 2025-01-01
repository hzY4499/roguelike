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
        // �����ײ�����Ƿ���ǽ��
        if (collision.gameObject.CompareTag("Wall"))
        {
            // �����ӵ�
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy") && collision.GetComponent<Renderer>().enabled) // ȷ������������Ч���ᴥ��
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<EnemyCollider>().TakeDamage(damage);
        }
    }
}
