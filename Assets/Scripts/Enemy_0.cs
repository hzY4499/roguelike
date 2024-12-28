using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0 : MonoBehaviour
{
    // �ƶ��ٶ�
    [SerializeField] private float speed;

    // �����ʼ��Զǽ�ڷ���
    private Vector3 moveDirection;
    [SerializeField] private ParticleSystem spawnIndicator;
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private ParticleSystem passAwayParticles;

    // Start ����Ϸ��ʼʱ����
    void Start()
    {       
        RandomLocation();
        enemyRenderer.enabled = false;

        LeanTween.rotate(spawnIndicator.gameObject, new Vector3(0, 0, 10f), 1f)
            .setOnComplete(SpawnSequenceCompleted);
    }

    // Update ÿһ֡����
    void Update()
    {
        // ��������Զ��ǽ�ڷ����ƶ�
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void SpawnSequenceCompleted()
    {
        enemyRenderer.enabled = true;
        spawnIndicator.Stop();
        spawnIndicator.transform.parent = null;
        speed = 1f;
    }

    private void RandomLocation()
    {
        float randomX;
        float randomY;

        // ȷ�����������ڱ�Ե
        if (Random.value > 0.5f)
        {
            // ��������߻��ұ߱�Ե
            if (Random.value > 0.5f)
            {
                randomX = -4.5f;
                moveDirection = Vector3.right;
            }
            else
            {
                randomX = 4.5f;
                moveDirection = Vector3.left;
            }
            randomY = Random.Range(-4f, 4f); // ���±�Ե���
        }
        else
        {
            // �������ϱ߻��±߱�Ե
            if (Random.value > 0.5f)
            {
                randomY = -4f;
                moveDirection = Vector3.up;
            }
            else
            {
                randomY = 4f;
                moveDirection = Vector3.down;
            }
            randomX = Random.Range(-4.5f, 4.5f); // ���ұ�Ե���
        }

        transform.position = new Vector3(randomX, randomY, 0);
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

        transform.position = new Vector3(randomX, randomY, 0);
    }

    private void PassAway()
    {

        passAwayParticles.Play();
            passAwayParticles.transform.parent = null;
        Destroy(gameObject);
    }
}