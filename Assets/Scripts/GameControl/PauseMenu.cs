using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel; // ��ͣҳ�����
    [SerializeField] private Button resumeButton;  // ������Ϸ��ť
    [SerializeField] private Button restartButton; // ���¿�ʼ��ť
    [SerializeField] private Button mainMenuButton; // ������ҳ��ť

    private bool isPaused = false;
    private GameManager gameManager;

    private void Start()
    {
        // �󶨰�ť�¼�
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);

        gameManager = FindFirstObjectByType<GameManager>();

        // ��ʼ����ͣҳ��Ϊ����
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        // ��� ESC ������
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

    // ��ͣ��Ϸ
    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0; // ��ͣ��Ϸʱ��
        pausePanel.SetActive(true); // ��ʾ��ͣҳ��
    }

    // ������Ϸ
    private void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1; // �ָ���Ϸʱ��
        pausePanel.SetActive(false); // ������ͣҳ��
    }

    // ���¿�ʼ��Ϸ
    private void RestartGame()
    {
        Time.timeScale = 1; // �ָ���Ϸʱ��
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ���¼��ص�ǰ����
    }

    // ������ҳ
    private void ReturnToMainMenu()
    {
        Time.timeScale = 1; // �ָ���Ϸʱ��
        SceneManager.LoadScene(0); // �������˵��������������˵������� buildIndex Ϊ 0��
    }
}