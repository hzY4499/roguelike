using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed;  // ����ƶ��ٶ�
    [SerializeField] private bool isShooting;  // ������״̬
    [SerializeField] private bool autoAtacking;// �Ƿ��Զ����
    [SerializeField] private List<GameObject> walls; // ��ǽ��Ԥ��������༭��


    private float m_nextFire;
    public float FireRate = 0.1f; // ��������
    public GameObject Bullet;     // �ӵ�����
    public float BulletSpeed;     // �ӵ��ٶ�

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

        // ���Զ��������£��������״̬
        if (!autoAtacking)
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0)) isShooting = true;
            else isShooting = false;

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
    // ��ײ���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ����Ƿ�����ǽ��
        if (collision.gameObject.CompareTag("Wall"))
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
