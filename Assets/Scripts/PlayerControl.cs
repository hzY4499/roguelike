using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed;  // 玩家移动速度
    [SerializeField] private bool isShooting;  // 玩家射击状态
    [SerializeField] private bool autoAtacking;// 是否自动射击

    private float m_nextFire;
    public float FireRate = 0.1f; // 开火速率
    public GameObject Bullet;     // 子弹对象
    public float BulletSpeed;     // 子弹速度

    public PlayerRotation playerRotation;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        PlayerMove();

        // 非自动射击情况下，更新射击状态
        if (!autoAtacking)
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0)) isShooting = true;
            else isShooting = false;

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
        m_nextFire = m_nextFire + Time.fixedDeltaTime;
        if (isShooting && m_nextFire > FireRate)
        {
            Vector3 m_mousePosition = Input.mousePosition;
            m_mousePosition = Camera.main.ScreenToWorldPoint(m_mousePosition);
            m_mousePosition.z = 0;

            float m_fireAngle = Vector2.Angle(m_mousePosition - this.transform.position, Vector2.up);

            if (m_mousePosition.x > this.transform.position.x) m_fireAngle = -m_fireAngle;

            m_nextFire = 0;

            GameObject m_bullet = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject;

            m_bullet.GetComponent<Rigidbody2D>().velocity = ((m_mousePosition - transform.position).normalized * BulletSpeed);

            m_bullet.transform.eulerAngles = new Vector3(0, 0, m_fireAngle);
        }
    }
}
