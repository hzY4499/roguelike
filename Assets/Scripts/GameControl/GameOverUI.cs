using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    //[Header("UI Elements")]
    [SerializeField] private TMP_Text currentScoreText; // 当前分数文本
    [SerializeField] private TMP_Text highScoreText;    // 历史最高分文本
    [SerializeField] private TMP_Text winText;
    [SerializeField] private Button restartButton;      // 重新开始按钮
    [SerializeField] private Button mainMenuButton;     // 返回主菜单按钮

    private int currentScore; // 当前分数
    private int highScore;    // 历史最高分

    private void Awake()
    {
        // 绑定按钮事件
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    public void Show(int score, bool win)
    {
        currentScore = score;
        UpdateHighScore(); // 更新历史最高分
        UpdateUI(win);        // 更新 UI 显示
        gameObject.SetActive(true); // 显示结算界面
    }

    private void UpdateHighScore()
    {
        // 从 PlayerPrefs 中加载历史最高分
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        // 如果当前分数更高，更新历史最高分
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    private void UpdateUI(bool win)
    {
        if (win) winText.SetText("恭喜你，获得胜利！");
        else winText.SetText("");
        currentScoreText.text = "当前分数: " + currentScore;
        highScoreText.text = "历史最高分: " + highScore;
    }

    private void RestartGame()
    {
        // 重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ReturnToMainMenu()
    {
        // 加载主菜单场景（假设主菜单场景的 buildIndex 为 0）
        SceneManager.LoadScene(0);
    }
}