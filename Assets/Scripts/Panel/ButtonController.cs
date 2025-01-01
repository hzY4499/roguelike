using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject gridPanel;  // 九宫格的父物体
    public Button[] buttons;  // 右侧的所有按钮
    public Sprite[] buttonImages;  // 按钮对应的图片

    void Start()
    {
        // 为每个按钮添加点击事件
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;  // 使用局部变量以避免闭包问题
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    // 处理按钮点击事件
    void OnButtonClick(int index)
    {
        Sprite selectedSprite = buttonImages[index];  // 获取按钮对应的图片
        if (selectedSprite != null)
        {
            // 找到一个空闲的格子
            GridCell emptyCell = FindEmptyCell();
            if (emptyCell != null)
            {
                emptyCell.PlaceImage(selectedSprite);  // 将图片放入该格子
            }
        }
    }

    // 查找九宫格中第一个空闲位置
    GridCell FindEmptyCell()
    {
        foreach (Transform child in gridPanel.transform)
        {
            GridCell cell = child.GetComponent<GridCell>();
            if (cell != null && !cell.isOccupied)
            {
                return cell;  // 返回第一个空闲的单元格
            }
        }
        return null;  // 如果没有空闲格子，返回null
    }
}
