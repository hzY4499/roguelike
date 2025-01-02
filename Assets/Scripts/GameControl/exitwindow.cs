using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class exitwindow : MonoBehaviour
{
    // ���ڴ洢��ť����
    public Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        // ��ȡ��ť���
        if (exitButton == null)
        {
            exitButton = GetComponent<Button>();
        }

        // ע�ᰴť����¼�
        exitButton.onClick.AddListener(ExitGame);
    }

    // �����ťʱ�˳���Ϸ
    void ExitGame()
    {
        // �˳���Ϸ���ڱ༭���л�ֹͣ���ţ��ڶ������е���Ϸ�л�ر���Ϸ��
        Debug.Log("Exiting game...");
        Application.Quit();

        // ����ڱ༭���в��ԣ�����ʹ������Ĵ�����ģ���˳�
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }
}
