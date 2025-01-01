using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridCell : MonoBehaviour, IPointerClickHandler
{
    public bool isOccupied = false;  // �Ƿ�ռ��
    public Image cellImage;  // �õ�Ԫ���ͼƬ
    private Color originalColor;

    void Start()
    {
        cellImage = GetComponent<Image>();  // ��ȡ��Ԫ���е�ͼƬ���
        originalColor = cellImage.color;  // ��¼ԭʼ��ɫ
    }

    // ����Ԫ�񱻵��ʱ
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isOccupied)
        {
            // ˫��ʱ��ȡ��ѡ��
            if (eventData.clickCount == 2)
            {
                isOccupied = false;
                cellImage.sprite = null;  // �Ƴ�ͼƬ
                cellImage.color = originalColor;  // �ָ�ԭʼ��ɫ
            }
        }
    }

    // ����ͼƬ���õ�Ԫ��
    public void PlaceImage(Sprite imageSprite)
    {
        isOccupied = true;
        cellImage.sprite = imageSprite;  // ����ͼƬ
        cellImage.color = Color.white;  // ʹͼƬ�ɼ�
    }
}
