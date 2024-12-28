using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0 : MonoBehaviour
{
    // 移动速度
    public float speed = 2.0f;

    // 保存初始最远墙壁方向
    private Vector3 moveDirection;

    // Start 在游戏开始时调用
    void Start()
    {
        // 在 (-4.5, 4.5) 范围内随机生成敌人的位置
        float randomX = Random.Range(-4.5f, 4.5f);
        float randomY = Random.Range(-4.5f, 4.5f);
        transform.position = new Vector3(randomX, randomY, 0);

        // 计算初始距离并选择最远墙壁的方向
        CalculateMoveDirection();
    }

    // Update 每一帧调用
    void Update()
    {
        // 持续朝最远的墙壁方向移动
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    // 计算初始时敌人与四面墙的距离并选择最远的墙壁
    void CalculateMoveDirection()
    {
        // 计算敌人与每面墙的初始距离
        float distanceToLeftWall = Mathf.Abs(transform.position.x + 4.5f);   // 距离左墙
        float distanceToRightWall = Mathf.Abs(transform.position.x - 4.5f);  // 距离右墙
        float distanceToTopWall = Mathf.Abs(transform.position.y - 4.5f);    // 距离上墙
        float distanceToBottomWall = Mathf.Abs(transform.position.y + 4.5f); // 距离下墙

        // 选择最远的墙并计算朝向
        if (distanceToLeftWall >= distanceToRightWall && distanceToLeftWall >= distanceToTopWall && distanceToLeftWall >= distanceToBottomWall)
        {
            moveDirection = Vector3.left; // 朝左墙移动
        }
        else if (distanceToRightWall >= distanceToLeftWall && distanceToRightWall >= distanceToTopWall && distanceToRightWall >= distanceToBottomWall)
        {
            moveDirection = Vector3.right; // 朝右墙移动
        }
        else if (distanceToTopWall >= distanceToLeftWall && distanceToTopWall >= distanceToRightWall && distanceToTopWall >= distanceToBottomWall)
        {
            moveDirection = Vector3.up; // 朝上墙移动
        }
        else
        {
            moveDirection = Vector3.down; // 朝下墙移动
        }
    }
    // 检测与墙壁的碰撞并消失
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 如果碰撞的对象是墙壁，敌人消失
        if (collision.gameObject.CompareTag("Wall"))
        {
            // 销毁敌人对象
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查碰撞对象是否是墙体
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // 销毁子弹
            Destroy(gameObject);
        }
    }
}