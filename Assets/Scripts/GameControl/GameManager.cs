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
        scoreText.text = "��ǰ������" + currentScore;
    }
    public void GameOver()
    {
        isOver = true;
        // �������ٵ��˵�Э��
        StartCoroutine(DestroyEnemiesWithDelay());
        gameOverUI.Show(currentScore);
    }

    private IEnumerator DestroyEnemiesWithDelay()
    {
        // ���ҳ��������� EnemyCollider ����
        EnemyCollider[] enemies = FindObjectsByType<EnemyCollider>(FindObjectsSortMode.None);

        // �������е���
        foreach (EnemyCollider enemy in enemies)
        {
            if (enemy != null) // ��ֵ���
            {
                enemy.SetDestroyReason(EnemyCollider.DestroyReason.Other);
                enemy.PassAway(); // ���õ��˵����ٷ���
                yield return new WaitForSeconds(0.1f); // �ȴ�һ��ʱ��
            }
        }
    }
}
