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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �����ײ�����Ƿ����ӵ�
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // �����ӵ�
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
