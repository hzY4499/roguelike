using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class fightingwindowload : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadScene);
    }
    void LoadScene()
    {
        SceneManager.LoadScene("fightingwindow"); // �����SceneB��Ŀ�곡�������ƣ�Ҫ�빹�������еĳ�������һ��
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
