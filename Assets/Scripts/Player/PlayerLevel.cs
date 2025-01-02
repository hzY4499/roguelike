using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // 引入 TextMeshPro
using UnityEngine.UI;  // 引入 Slider 所需的命名空间

public class PlayerLevel : MonoBehaviour
{
    public int currentLevel = 0;  // 当前等级
    private int index = 0;
    private int currentXP = 0;     // 当前经验值
    public TMP_Text levelText;    // 使用 TMP_Text 而不是 Text
    //public TMP_Text xpText;       // 使用 TMP_Text 而不是 Text
    public Slider xpSlider;       // 添加一个Slider来显示经验值的进度
    private GameManager gameManager;
    private ReinforcedBarrel reinforcedBarrel;

    // 每个等级所需的经验值
    private int[] xpRequiredForLevel = new int[] { 0, 10, 20, 25, 30, 45, 50, 55, 60, 65, 70 };

    // 每击杀一个敌人，玩家获得的经验
    private int xpPerBall = 1;
    // 需要引用的暂停面板
    public GameObject levelUpPanel;//升级面板
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        reinforcedBarrel = FindFirstObjectByType<ReinforcedBarrel>();
        // 隐藏升级面板
        levelUpPanel.SetActive(false);
        UpdateUI();  // 初始更新UI
    }

    // Update is called once per frame
    void Update()
    {
        // 可以在Update中进行一些其他的操作或逻辑
    }

    // 玩家击败敌人后调用此方法
    public void OnBallPicked()
    {
        // 增加经验
        currentXP += xpPerBall;

        // 检查是否升级
        CheckLevelUp();

        // 更新UI显示
        UpdateUI();
    }

    // 检查是否达到升级条件
    private void CheckLevelUp()
    {
        index = currentLevel < xpRequiredForLevel.Length ? currentLevel : xpRequiredForLevel.Length - 1;

        if (currentXP >= xpRequiredForLevel[index])
        {
            reinforcedBarrel.RandomlySelectButtons();
            
            // 升级
            currentLevel++;
            gameManager.currentScore += 100;
            // 显示升级面板
            levelUpPanel.SetActive(true);
            Time.timeScale = 0f;
            // 重置经验为0
            currentXP = 0;
        }
    }

    // 更新UI显示
    private void UpdateUI()
    {
        // 显示当前等级和经验
        levelText.text = "lvl  " + currentLevel;
        //xpText.text = "XP: " + currentXP + "/" + xpRequiredForLevel[currentLevel];

        // 更新 Slider 进度
        // 计算经验进度，范围从 0 到 1
        float xpProgress = (float)currentXP / xpRequiredForLevel[index];
        xpSlider.value = xpProgress;  // 更新 Slider 的值
    }
    // 显示等级提升的面板
    private void ShowLevelUpPanel()
    {
        // 显示升级面板
        levelUpPanel.SetActive(true);
    }

    // 关闭升级面板（可以在面板的按钮点击事件中调用）
    public void CloseLevelUpPanel()
    {
        // 隐藏升级面板
        levelUpPanel.SetActive(false);
    }
}
