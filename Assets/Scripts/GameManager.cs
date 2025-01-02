using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private TMP_Text scoreText;
    public bool isOver;
    public int currentScore;
    void Start()
    {
        isOver = false;
        gameOverUI.gameObject.SetActive(false);
    }

    void Update()
    {
        scoreText.text = "当前分数：" + currentScore;
    }
    public void GameOver()
    {
        isOver = true;
        // 启动销毁敌人的协程
        StartCoroutine(DestroyEnemiesWithDelay());
        gameOverUI.Show(currentScore);
    }

    private IEnumerator DestroyEnemiesWithDelay()
    {
        // 查找场景中所有 EnemyCollider 对象
        EnemyCollider[] enemies = FindObjectsByType<EnemyCollider>(FindObjectsSortMode.None);

        // 遍历所有敌人
        foreach (EnemyCollider enemy in enemies)
        {
            if (enemy != null) // 空值检查
            {
                enemy.SetDestroyReason(EnemyCollider.DestroyReason.Other);
                enemy.PassAway(); // 调用敌人的销毁方法
                yield return new WaitForSeconds(0.1f); // 等待一段时间
            }
        }
    }
}
