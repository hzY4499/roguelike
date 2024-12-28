using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0 : MonoBehaviour
{
    // 移动速度
    [SerializeField] private float speed;

    // 保存初始最远墙壁方向
    private Vector3 moveDirection;
    [SerializeField] private ParticleSystem spawnIndicator;
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private ParticleSystem passAwayParticles;

    // Start 在游戏开始时调用
    void Start()
    {       
        RandomLocation();
        enemyRenderer.enabled = false;

        LeanTween.rotate(spawnIndicator.gameObject, new Vector3(0, 0, 10f), 1f)
            .setOnComplete(SpawnSequenceCompleted);
    }

    // Update 每一帧调用
    void Update()
    {
        // 持续朝最远的墙壁方向移动
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

        // 确保敌人生成在边缘
        if (Random.value > 0.5f)
        {
            // 生成在左边或右边边缘
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
            randomY = Random.Range(-4f, 4f); // 上下边缘随机
        }
        else
        {
            // 生成在上边或下边边缘
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
            randomX = Random.Range(-4.5f, 4.5f); // 左右边缘随机
        }

        transform.position = new Vector3(randomX, randomY, 0);
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

        passAwayParticles.Play();
            passAwayParticles.transform.parent = null;
        Destroy(gameObject);
    }
}