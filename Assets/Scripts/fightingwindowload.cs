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
        SceneManager.LoadScene("fightingwindow"); // 这里的SceneB是目标场景的名称，要与构建设置中的场景名称一致
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
