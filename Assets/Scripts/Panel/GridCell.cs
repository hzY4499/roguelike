using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridCell : MonoBehaviour, IPointerClickHandler
{
    public bool isOccupied = false;  // 是否被占用
    public Image cellImage;  // 该单元格的图片
    private Color originalColor;

    void Start()
    {
        cellImage = GetComponent<Image>();  // 获取单元格中的图片组件
        originalColor = cellImage.color;  // 记录原始颜色
    }

    // 当单元格被点击时
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isOccupied)
        {
            // 双击时，取消选择
            if (eventData.clickCount == 2)
            {
                isOccupied = false;
                cellImage.sprite = null;  // 移除图片
                cellImage.color = originalColor;  // 恢复原始颜色
            }
        }
    }

    // 放置图片到该单元格
    public void PlaceImage(Sprite imageSprite)
    {
        isOccupied = true;
        cellImage.sprite = imageSprite;  // 设置图片
        cellImage.color = Color.white;  // 使图片可见
    }
}
