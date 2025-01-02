using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class exitwindow : MonoBehaviour
{
    // 用于存储按钮引用
    public Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        // 获取按钮组件
        if (exitButton == null)
        {
            exitButton = GetComponent<Button>();
        }

        // 注册按钮点击事件
        exitButton.onClick.AddListener(ExitGame);
    }

    // 点击按钮时退出游戏
    void ExitGame()
    {
        // 退出游戏（在编辑器中会停止播放，在独立运行的游戏中会关闭游戏）
        Debug.Log("Exiting game...");
        Application.Quit();

        // 如果在编辑器中测试，可以使用下面的代码来模拟退出
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }
}
