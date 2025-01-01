using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject gridPanel;  // �Ź���ĸ�����
    public Button[] buttons;  // �Ҳ�����а�ť
    public Sprite[] buttonImages;  // ��ť��Ӧ��ͼƬ

    void Start()
    {
        // Ϊÿ����ť��ӵ���¼�
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;  // ʹ�þֲ������Ա���հ�����
            buttons[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    // ����ť����¼�
    void OnButtonClick(int index)
    {
        Sprite selectedSprite = buttonImages[index];  // ��ȡ��ť��Ӧ��ͼƬ
        if (selectedSprite != null)
        {
            // �ҵ�һ�����еĸ���
            GridCell emptyCell = FindEmptyCell();
            if (emptyCell != null)
            {
                emptyCell.PlaceImage(selectedSprite);  // ��ͼƬ����ø���
            }
        }
    }

    // ���ҾŹ����е�һ������λ��
    GridCell FindEmptyCell()
    {
        foreach (Transform child in gridPanel.transform)
        {
            GridCell cell = child.GetComponent<GridCell>();
            if (cell != null && !cell.isOccupied)
            {
                return cell;  // ���ص�һ�����еĵ�Ԫ��
            }
        }
        return null;  // ���û�п��и��ӣ�����null
    }
}
