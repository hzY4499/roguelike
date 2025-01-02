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
    [SerializeField] private AudioClip backgroundMusic; // ��Ϸ����ʱ�ı�������
    [SerializeField] private AudioClip gameOverMusic;   // ��Ϸ����ʱ������

    private AudioSource audioSource; // ���ڲ������ֵ� AudioSource

    public bool isOver;
    public int currentScore;

    void Start()
    {
        isOver = false;
        gameOverUI.gameObject.SetActive(false);

        // ��ȡ����� AudioSource ���
        audioSource = GetComponent<AudioSource>();

        // ������Ϸ����ʱ�ı�������
        PlayBackgroundMusic();
    }

    void Update()
    {
        scoreText.text = "��ǰ������" + currentScore;
    }

    public void GameOver()
    {
        isOver = true;

        // ֹͣ��ǰ���ֲ�������Ϸ����ʱ������
        StopMusic();
        PlayGameOverMusic();

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

    // ������Ϸ����ʱ�ı�������
    private void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && audioSource != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true; // ѭ������
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

    // ������Ϸ����ʱ������
    private void PlayGameOverMusic()
    {
        if (gameOverMusic != null && audioSource != null)
        {
            audioSource.clip = gameOverMusic;
            audioSource.loop = true; // ��ѭ������
            audioSource.Play();
        }
    }

    // ֹͣ��ǰ����
    private void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}