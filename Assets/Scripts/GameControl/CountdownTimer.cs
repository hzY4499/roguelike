using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float totalTime = 300f; // 5���� = 300��
    [SerializeField] private GameManager gameManager;
    public TMP_Text timerText; // ������ʾ����ʱ��UI�ı�
    private float currentTime;

    void Start()
    {
        currentTime = totalTime;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (!gameManager.isOver)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                OnTimerEnd();
            }
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void OnTimerEnd()
    {
        // �����������Ϸ�����߼�
        gameManager.GameOver();
    }
}