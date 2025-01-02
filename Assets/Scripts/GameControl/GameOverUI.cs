using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    //[Header("UI Elements")]
    [SerializeField] private TMP_Text currentScoreText; // ��ǰ�����ı�
    [SerializeField] private TMP_Text highScoreText;    // ��ʷ��߷��ı�
    [SerializeField] private Button restartButton;      // ���¿�ʼ��ť
    [SerializeField] private Button mainMenuButton;     // �������˵���ť

    private int currentScore; // ��ǰ����
    private int highScore;    // ��ʷ��߷�

    private void Awake()
    {
        // �󶨰�ť�¼�
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    public void Show(int score)
    {
        currentScore = score;
        UpdateHighScore(); // ������ʷ��߷�
        UpdateUI();        // ���� UI ��ʾ
        gameObject.SetActive(true); // ��ʾ�������
    }

    private void UpdateHighScore()
    {
        // �� PlayerPrefs �м�����ʷ��߷�
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        // �����ǰ�������ߣ�������ʷ��߷�
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    private void UpdateUI()
    {
        currentScoreText.text = "��ǰ����: " + currentScore;
        highScoreText.text = "��ʷ��߷�: " + highScore;
    }

    private void RestartGame()
    {
        // ���¼��ص�ǰ����
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ReturnToMainMenu()
    {
        // �������˵��������������˵������� buildIndex Ϊ 0��
        SceneManager.LoadScene(0);
    }
}