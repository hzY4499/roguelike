using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0 : MonoBehaviour
{
    // �ƶ��ٶ�
    public float speed = 2.0f;

    // �����ʼ��Զǽ�ڷ���
    private Vector3 moveDirection;

    // Start ����Ϸ��ʼʱ����
    void Start()
    {
        // �� (-4.5, 4.5) ��Χ��������ɵ��˵�λ��
        float randomX = Random.Range(-4.5f, 4.5f);
        float randomY = Random.Range(-4.5f, 4.5f);
        transform.position = new Vector3(randomX, randomY, 0);

        // �����ʼ���벢ѡ����Զǽ�ڵķ���
        CalculateMoveDirection();
    }

    // Update ÿһ֡����
    void Update()
    {
        // ��������Զ��ǽ�ڷ����ƶ�
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    // �����ʼʱ����������ǽ�ľ��벢ѡ����Զ��ǽ��
    void CalculateMoveDirection()
    {
        // ���������ÿ��ǽ�ĳ�ʼ����
        float distanceToLeftWall = Mathf.Abs(transform.position.x + 4.5f);   // ������ǽ
        float distanceToRightWall = Mathf.Abs(transform.position.x - 4.5f);  // ������ǽ
        float distanceToTopWall = Mathf.Abs(transform.position.y - 4.5f);    // ������ǽ
        float distanceToBottomWall = Mathf.Abs(transform.position.y + 4.5f); // ������ǽ

        // ѡ����Զ��ǽ�����㳯��
        if (distanceToLeftWall >= distanceToRightWall && distanceToLeftWall >= distanceToTopWall && distanceToLeftWall >= distanceToBottomWall)
        {
            moveDirection = Vector3.left; // ����ǽ�ƶ�
        }
        else if (distanceToRightWall >= distanceToLeftWall && distanceToRightWall >= distanceToTopWall && distanceToRightWall >= distanceToBottomWall)
        {
            moveDirection = Vector3.right; // ����ǽ�ƶ�
        }
        else if (distanceToTopWall >= distanceToLeftWall && distanceToTopWall >= distanceToRightWall && distanceToTopWall >= distanceToBottomWall)
        {
            moveDirection = Vector3.up; // ����ǽ�ƶ�
        }
        else
        {
            moveDirection = Vector3.down; // ����ǽ�ƶ�
        }
    }
    // �����ǽ�ڵ���ײ����ʧ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ײ�Ķ�����ǽ�ڣ�������ʧ
        if (collision.gameObject.CompareTag("Wall"))
        {
            // ���ٵ��˶���
            Destroy(gameObject);
        }
        // �����ײ�Ķ������ӵ���������ʧ
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // ���ٵ��˶���
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �����ײ�����Ƿ���ǽ��
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // �����ӵ�
            Destroy(gameObject);
        }
    }
}