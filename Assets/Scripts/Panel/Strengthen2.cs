using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strengthen2 : PanelClose
{
    protected override void ClosePanel()
    {
        PlayerHealth playerHealth = FindFirstObjectByType<PlayerHealth>();
        playerHealth.recoveryTimes *= 1.1f;
        base.ClosePanel();
    }
}
