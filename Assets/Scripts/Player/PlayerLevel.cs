using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // ���� TextMeshPro
using UnityEngine.UI;  // ���� Slider ����������ռ�

public class PlayerLevel : MonoBehaviour
{
    public int currentLevel = 0;  // ��ǰ�ȼ�
    private int currentXP = 0;     // ��ǰ����ֵ
    public TMP_Text levelText;    // ʹ�� TMP_Text ������ Text
    //public TMP_Text xpText;       // ʹ�� TMP_Text ������ Text
    public Slider xpSlider;       // ���һ��Slider����ʾ����ֵ�Ľ���

    // ÿ���ȼ�����ľ���ֵ
    private int[] xpRequiredForLevel = new int[] { 0, 10, 20, 25, 30, 45, 50, 55, 60, 65, 70 };

    // ÿ��ɱһ�����ˣ���һ�õľ���
    private int xpPerBall = 1;
    // ��Ҫ���õ���ͣ���
    public GameObject levelUpPanel;//�������
    // Start is called before the first frame update
    void Start()
    {
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
        // ȷ�����ᳬ�����ȼ�
        if (currentLevel < xpRequiredForLevel.Length - 1 && currentXP >= xpRequiredForLevel[currentLevel])
        {
            // ����
            currentLevel++;
            // ��ʾ�������
            levelUpPanel.SetActive(true);
            Time.timeScale = 0f;
            // ���þ���Ϊ0
            currentXP = 0;
        }
    }

    // ����UI��ʾ
    private void UpdateUI()
    {
        // ��ʾ��ǰ�ȼ��;���
        levelText.text = "lvl  " + currentLevel;
        //xpText.text = "XP: " + currentXP + "/" + xpRequiredForLevel[currentLevel];

        // ���� Slider ����
        // ���㾭����ȣ���Χ�� 0 �� 1
        float xpProgress = (float)currentXP / xpRequiredForLevel[currentLevel];
        xpSlider.value = xpProgress;  // ���� Slider ��ֵ
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
