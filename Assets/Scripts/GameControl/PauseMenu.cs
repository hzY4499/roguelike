using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel; // 暂停页面面板
    [SerializeField] private Button resumeButton;  // 继续游戏按钮
    [SerializeField] private Button restartButton; // 重新开始按钮
    [SerializeField] private Button mainMenuButton; // 返回主页按钮

    private bool isPaused = false;
    private GameManager gameManager;

    private void Start()
    {
        // 绑定按钮事件
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);

        gameManager = FindFirstObjectByType<GameManager>();

        // 初始化暂停页面为隐藏
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        // 检测 ESC 键按下
        if (!gameManager.isOver && Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // 暂停游戏
    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; // 暂停游戏时间
        gameManager.PauseMusic();
        pausePanel.SetActive(true); // 显示暂停页面
    }

    // 继续游戏
    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; // 恢复游戏时间
        pausePanel.SetActive(false); // 隐藏暂停页面
    }

    // 重新开始游戏
    private void RestartGame()
    {
        Time.timeScale = 1; // 恢复游戏时间
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 重新加载当前场景
    }

    // 返回主页
    private void ReturnToMainMenu()
    {
        Time.timeScale = 1; // 恢复游戏时间
        SceneManager.LoadScene(0); // 加载主菜单场景（假设主菜单场景的 buildIndex 为 0）
    }
}