using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // ���� TextMeshPro
using UnityEngine.UI;  // ���� Slider ����������ռ�

public class PlayerLevel : MonoBehaviour
{
    public int currentLevel = 0;  // ��ǰ�ȼ�
    private int index = 0;
    private int currentXP = 0;     // ��ǰ����ֵ
    public TMP_Text levelText;    // ʹ�� TMP_Text ������ Text
    //public TMP_Text xpText;       // ʹ�� TMP_Text ������ Text
    public Slider xpSlider;       // ���һ��Slider����ʾ����ֵ�Ľ���
    private GameManager gameManager;
    private ReinforcedBarrel reinforcedBarrel;
    public bool isPausedByUpgrade;

    // ÿ���ȼ�����ľ���ֵ
    private int[] xpRequiredForLevel = new int[] { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 60, 70, 80, 90, 100, 120, 140, 160, 180, 200, 230, 260, 290, 330, 360, 390, 430, 470, 510, 550};

    // ÿ��ɱһ�����ˣ���һ�õľ���
    private int xpPerBall = 1;
    // ��Ҫ���õ���ͣ���
    public GameObject levelUpPanel;//�������
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        reinforcedBarrel = FindFirstObjectByType<ReinforcedBarrel>();
        // �����������
        levelUpPanel.SetActive(false);
        UpdateUI();  // ��ʼ����UI
    }

    // Update is called once per frame
    void Update()
    {
        // ������Update�н���һЩ�����Ĳ������߼�
    }

    // ��һ��ܵ��˺���ô˷���
    public void OnBallPicked()
    {
        // ���Ӿ���
        currentXP += xpPerBall;

        // ����Ƿ�����
        CheckLevelUp();

        // ����UI��ʾ
        UpdateUI();
    }

    // ����Ƿ�ﵽ��������
    private void CheckLevelUp()
    {
        if (currentLevel < xpRequiredForLevel.Length && currentXP >= xpRequiredForLevel[currentLevel])
        {
            reinforcedBarrel.RandomlySelectButtons();
            
            // ����
            currentLevel++;
            gameManager.currentScore += 100;
            // ��ʾ�������
            levelUpPanel.SetActive(true);
            isPausedByUpgrade = true;
            Time.timeScale = 0f;
            // ���þ���Ϊ0
            currentXP = 0;
        }
    }

    private void UpdateUI()
    {
        // ��ʾ��ǰ�ȼ�
        levelText.text = "lvl  " + currentLevel;

        // ���� Slider �����ֵ
        xpSlider.maxValue = xpRequiredForLevel[currentLevel];

        // ���㾭����ȣ���Χ�� 0 �� 1
        if (xpRequiredForLevel[currentLevel] == 0)
        {
            xpSlider.value = 0;
        }
        else
        {
            float xpProgress = (float)currentXP;// / xpRequiredForLevel[index];
            xpSlider.value = xpProgress;  // ���� Slider ��ֵ
        }
    }

    // ��ʾ�ȼ����������
    private void ShowLevelUpPanel()
    {
        // ��ʾ�������
        levelUpPanel.SetActive(true);
    }

    // �ر�������壨���������İ�ť����¼��е��ã�
    public void CloseLevelUpPanel()
    {
        // �����������
        levelUpPanel.SetActive(false);
    }
}
