using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;  // ����ƶ��ٶ�
    [SerializeField] private bool isShooting;  // ������״̬
    [SerializeField] private bool autoAtacking;// �Ƿ��Զ����
    private bool autoShoot;
    private Transform enemy;

    private float ShootTimer;
    [SerializeField] private float ShootDelay = 0.1f; // ��������
    public int criticalRate = 10; // �����ʣ��ٷֱȣ�
    public float criticalDamage = 1.6f; // �����˺����ٷֱȣ�
    public GameObject Bullet;     // �ӵ�����
    public float BulletSpeed;     // �ӵ��ٶ�
    public int Bulletdamage;

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
        
        // ���Զ��������£��������״̬
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
                if (enemyChecked.GetComponent<Renderer>().enabled) // ������������������Ч
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

    // ����ƶ�
    private void PlayerMove()
    {
        float MoveX = Input.GetAxis("Horizontal");
        float MoveY = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(MoveX, MoveY);
        movement *= moveSpeed;
        rb.velocity = movement;
    }

    // ������
    private void PlayerShoot()
    {
        // �������״̬������ת�ٶ�
        float initspeed = playerRotation.GetInitSpeed();
        if (isShooting) playerRotation.speed = 3 * initspeed;
        else playerRotation.speed = initspeed;

        // �����ӵ�
        ShootTimer = ShootTimer + Time.fixedDeltaTime;
        if (isShooting && ShootTimer > ShootDelay)
        {
            Vector3 m_mousePosition = Input.mousePosition;
            m_mousePosition = Camera.main.ScreenToWorldPoint(m_mousePosition);
            m_mousePosition.z = 0;
            Vector3 targetPosition = autoShoot ? enemy.position : m_mousePosition;

            // �����������Ƕ�
            float baseFireAngle = Vector2.Angle(targetPosition - this.transform.position, Vector2.up);
            if (targetPosition.x > this.transform.position.x) baseFireAngle = -baseFireAngle;

            ShootTimer = 0;

            // ɢ��Ƕȼ��
            float spreadAngle = 10f; // ÿ���ӵ�֮��ĽǶȼ��

            // ���������ӵ�
            for (int i = -1; i <= 1; i++)
            {
                float m_fireAngle = baseFireAngle + i * spreadAngle;

                GameObject m_bullet = Instantiate(Bullet, transform.position, Quaternion.identity) as GameObject;
                m_bullet.GetComponent<Bullet>().SetDamage(Bulletdamage);
                m_bullet.transform.parent = transform;

                // �����ӵ�����
                Vector3 shootDirection = Quaternion.Euler(0, 0, m_fireAngle) * Vector2.up;
                m_bullet.GetComponent<Rigidbody2D>().velocity = shootDirection * BulletSpeed;

                m_bullet.transform.eulerAngles = new Vector3(0, 0, m_fireAngle);
            }
        }
    }

    // ��ײ���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ����Ƿ�����ǽ��
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            // ��ȡ��ײ���ߣ�ǽ�ڵı��淽��
            Vector2 bounceDirection = collision.contacts[0].normal; // ��ײ����

            // ����Ŀ��λ��
            Vector2 targetPosition = (Vector2)transform.position + bounceDirection * 0.5f; // ��Ŀ��λ��

            // ����Э�̣�ʵ��ƽ���ƶ�
            StartCoroutine(SmoothMove(targetPosition, 0.2f)); // 0.2�����ƶ����
        }
    }

    // ƽ���ƶ�Э�� ��ײ��ƽ���ƶ�
    private IEnumerator SmoothMove(Vector2 targetPosition, float duration)
    {
        Vector2 startPosition = transform.position; // ��ʼλ��
        float elapsedTime = 0f;                     // �Ѿ�����ʱ��

        while (elapsedTime < duration)
        {
            // ʹ�� Lerp ��ֵ����ƽ������
            rb.MovePosition(Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration));

            elapsedTime += Time.deltaTime; // ����ʱ��
            yield return null;             // �ȴ���һ֡
        }

        // ȷ���ƶ�������λ��
        rb.MovePosition(targetPosition);
    }
}
