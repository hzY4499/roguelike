using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [SerializeField] private float totalTime = 300f; // 5分钟 = 300秒
    public TMP_Text timerText; // 用于显示倒计时的UI文本
    private float currentTime;
    private bool isTimerRunning = true;

    void Start()
    {
        currentTime = totalTime;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                isTimerRunning = false;
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
        Debug.Log("倒计时结束，游戏结算！");
        // 在这里调用游戏结算逻辑
        GameOver();
    }

    private void GameOver()
    {
        // 例如：显示结算界面、停止游戏等
        // 你可以在这里添加你的游戏结算逻辑
        Debug.Log("游戏结束，进行结算！");
    }
}