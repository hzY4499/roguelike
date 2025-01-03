using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private TMP_Text scoreText;

    [Header("Music Settings")]
    [SerializeField] private AudioClip backgroundMusic; // 游戏进行时的背景音乐
    [SerializeField] private AudioClip gameOverMusic;   // 游戏结束时的音乐

    private AudioSource audioSource; // 用于播放音乐的 AudioSource

    public bool isOver;
    public int currentScore;

    void Start()
    {
        isOver = false;
        gameOverUI.gameObject.SetActive(false);

        // 获取或添加 AudioSource 组件
        audioSource = GetComponent<AudioSource>();

        // 播放游戏进行时的背景音乐
        PlayBackgroundMusic();
    }

    void Update()
    {
        scoreText.text = "当前分数：" + currentScore;
    }

    public void GameOver(bool win)
    {
        isOver = true;

        // 停止当前音乐并播放游戏结束时的音乐
        StopMusic();
        PlayGameOverMusic();

        // 启动销毁敌人的协程
        StartCoroutine(DestroyEnemiesWithDelay());
        gameOverUI.Show(currentScore, win);
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

    // 播放游戏进行时的背景音乐
    private void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && audioSource != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true; // 循环播放
            audioSource.Play();
        }
    }

    public void PauseMusic()
    {
        if (audioSource != null)
        {
            audioSource.Pause();
        }
    }

    // 播放游戏结束时的音乐
    private void PlayGameOverMusic()
    {
        if (gameOverMusic != null && audioSource != null)
        {
            audioSource.clip = gameOverMusic;
            audioSource.loop = true; // 不循环播放
            audioSource.Play();
        }
    }

    // 停止当前音乐
    private void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}