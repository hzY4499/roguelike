using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReinforcedBarrel : MonoBehaviour
{
    public GameObject levelUpPanel; // ����������������
    public Transform upgradeButtonContainer; // ������ť����������
    public List<Button> allButtons = new List<Button>(); // �洢���а�ť���б�

    void Start()
    {
        // ���������������жϣ�����������ʱ����ʾ������岢���ѡ��ť
        if (levelUpPanel.activeSelf)
        {
            // ��ͣ��Ϸ������ʱ������Ϊ0
            Time.timeScale = 0f;

            // ���ѡ��3����ť����������
            RandomlySelectButtons();
        }
        // ������Ҫ��Start�в��Ұ�ť�����谴ť�Ѿ�����ק����
    }

    void Update()
    {

    }

    void RandomlySelectButtons()
    {
        // ȷ��������������3����ť
        if (allButtons.Count < 3)
        {
            Debug.LogError("��ť�������㣬�޷�ѡ��3����ť");
            return;
        }

        // ����һ���µ��б����洢ѡ�еİ�ť
        List<Button> selectedButtons = new List<Button>();

        // �����洢���ѡ��İ�ť��������ֹ�ظ�ѡ��
        List<int> selectedIndices = new List<int>();

        // ���ѡ��3����ť
        while (selectedButtons.Count < 3)
        {
            int randomIndex = Random.Range(0, allButtons.Count);

            // ȷ��û���ظ�ѡ����ͬ�İ�ť
            if (!selectedIndices.Contains(randomIndex))
            {
                selectedIndices.Add(randomIndex);
                selectedButtons.Add(allButtons[randomIndex]);
            }
        }

        // ���upgradeButtonContainer�е�����������
        foreach (Transform child in upgradeButtonContainer)
        {
            Destroy(child.gameObject);
        }

        // ��ѡ�еİ�ť����upgradeButtonContainer
        foreach (Button btn in selectedButtons)
        {
            Button newButton = Instantiate(btn, upgradeButtonContainer); // ʵ������ť
            newButton.gameObject.SetActive(true); // �����°�ť
        }
    }
}
