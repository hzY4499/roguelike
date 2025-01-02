using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReinforcedBarrel : MonoBehaviour
{
    public GameObject levelUpPanel; // 级别提升面板的引用
    public Transform upgradeButtonContainer; // 升级按钮容器的引用
    public List<Button> allButtons = new List<Button>(); // 存储所有按钮的列表

    private List<Button> selectedButtons = new List<Button>(); // 存储选中的按钮

    void Start()
    {
    }

    void Update()
    {
    }

    public void RandomlySelectButtons()
    {
        // 确保容器内至少有3个按钮
        if (allButtons.Count < 3)
        {
            Debug.LogError("按钮数量不足，无法选择3个按钮");
            return;
        }

        // 清空之前选中的按钮
        selectedButtons.Clear();

        // 用来存储随机选择的按钮索引，防止重复选择
        List<int> selectedIndices = new List<int>();

        // 随机选择3个按钮
        while (selectedButtons.Count < 3)
        {
            int randomIndex = Random.Range(0, allButtons.Count);

            // 确保没有重复选择相同的按钮
            if (!selectedIndices.Contains(randomIndex))
            {
                selectedIndices.Add(randomIndex);
                selectedButtons.Add(allButtons[randomIndex]);
            }
        }

        // 清空upgradeButtonContainer中的所有子物体
        foreach (Transform child in upgradeButtonContainer)
        {
            Destroy(child.gameObject);
        }

        // 将选中的按钮放入upgradeButtonContainer
        foreach (Button btn in selectedButtons)
        {
            Button newButton = Instantiate(btn, upgradeButtonContainer); // 实例化按钮
            newButton.gameObject.SetActive(true); // 启用新按钮
        }
    }
}
