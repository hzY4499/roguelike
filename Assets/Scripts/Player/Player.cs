using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 3f;  // 玩家移动速度
    private bool isShooting;  // 玩家射击状态
    [SerializeField] private bool autoAtacking;// 是否自动射击
    private bool autoShoot;
    private Transform enemy;

    private float ShootTimer;
    public float ShootDelay = 0.2f; // 开火速率
    public int criticalRate = 10; // 暴击率（百分比）
    public float criticalDamage = 1.6f; // 暴击伤害（百分比）
    public GameObject Bullet;     // 子弹对象
    public float BulletSpeed = 10f;     // 子弹速度
    public float BulletDamage = 3f;
    public float BulletAliveTime = 0.5f;
    public float BulletSize = 1f;
    public int BulletCount = 3;
    public float spreadAngle = 10f;

    private PlayerRotation playerRotation;
    private Rigidbody2D rb;

    void Start()
    {
        autoShoot = autoAtacking;
        rb = GetComponent<Rigidbody2D>();
        playerRotation = GetComponentInChildren<PlayerRotation>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        PlayerMove();

        if (Input.GetKeyDown(KeyCode.F))
        {
            autoAtacking = !autoAtacking;
            autoShoot = autoAtacking;
        }
        
        // 非自动射击情况下，更新射击状态
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            isShooting = true;
            if (autoAtacking) autoShoot = false;
         }
         else
         {
            isShooting = false;
            if (autoAtacking) autoShoot = true;
         }
        if (autoShoot)
        {
            GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
            int closestEnemyIndex = -1;
            float minDistance = 5000;

            for (int i = 0; i < enemyObjects.Length; i++)
            {
                GameObject enemyChecked = enemyObjects[i];
                if (enemyChecked.GetComponent<Renderer>().enabled) // 避免锁定敌人生成特效
                {
                    float distanceToEnemy = Vector2.Distance(transform.position, enemyChecked.transform.position);
                    if (distanceToEnemy < minDistance)
                    {
                        closestEnemyIndex = i;
                        minDistance = distanceToEnemy;
                    }
                }
            }

            if (closestEnemyIndex != -1)
            {
                isShooting = true;
                enemy = enemyObjects[closestEnemyIndex].transform;
            }
            else
            {
                isShooting = false;
                enemy = null;
            }
        }

        PlayerShoot();
    }

    // 玩家移动
    private void PlayerMove()
    {
        float MoveX = Input.GetAxis("Horizontal");
        float MoveY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(MoveX, MoveY);
        movement *= moveSpeed;
        rb.velocity = movement;
    }

    // 玩家射击
    private void PlayerShoot()
    {
        // 根据射击状态调整自转速度
        float initspeed = playerRotation.GetInitSpeed();
        if (isShooting) playerRotation.speed = 3 * initspeed;
        else playerRotation.speed = initspeed;

        // 生成子弹
        ShootTimer = ShootTimer + Time.fixedDeltaTime;
        if (isShooting && ShootTimer > ShootDelay)
        {
            Vector3 m_mousePosition = Input.mousePosition;
            m_mousePosition = Camera.main.ScreenToWorldPoint(m_mousePosition);
            m_mousePosition.z = 0;
            Vector3 targetPosition = autoShoot ? enemy.position : m_mousePosition;

            // 计算基础射击角度
            float baseFireAngle = Vector2.Angle(targetPosition - this.transform.position, Vector2.up);
            if (targetPosition.x > this.transform.position.x) baseFireAngle = -baseFireAngle;

            ShootTimer = 0;

            for (int i = 0; i < BulletCount; i++)
            {
                // 计算当前子弹的发射角度
                float m_fireAngle = baseFireAngle + (i - (BulletCount - 1) / 2f) * spreadAngle;

                // 生成子弹
                GameObject m_bullet = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject;
                m_bullet.GetComponent<Bullet>().SetDamage(BulletDamage);
                m_bullet.GetComponent<Bullet>().SetAliveTime(BulletAliveTime);
                m_bullet.transform.localScale *= BulletSize;
                m_bullet.transform.parent = transform;

                // 计算子弹方向
                Vector3 shootDirection = Quaternion.Euler(0, 0, m_fireAngle) * Vector2.up;
                m_bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * BulletSpeed;

                // 设置子弹旋转角度
                m_bullet.transform.eulerAngles = new Vector3(0, 0, m_fireAngle);
            }
        }
    }

    // 碰撞检测
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查是否碰到墙壁
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            // 获取碰撞法线（墙壁的表面方向）
            Vector2 bounceDirection = collision.contacts[0].normal; // 碰撞法线

            // 计算目标位置
            Vector2 targetPosition = (Vector2)transform.position + bounceDirection * 0.5f; // 新目标位置

            // 启动协程，实现平滑移动
            StartCoroutine(SmoothMove(targetPosition, 0.2f)); // 0.2秒内移动完成
        }
    }

    // 平滑移动协程 碰撞后平滑移动
    private IEnumerator SmoothMove(Vector2 targetPosition, float duration)
    {
        Vector2 startPosition = transform.position; // 起始位置
        float elapsedTime = 0f;                     // 已经过的时间

        while (elapsedTime < duration)
        {
            // 使用 Lerp 插值进行平滑过渡
            rb.MovePosition(Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration));

            elapsedTime += Time.deltaTime; // 更新时间
            yield return null;             // 等待下一帧
        }

        // 确保移动到最终位置
        rb.MovePosition(targetPosition);
    }
}
