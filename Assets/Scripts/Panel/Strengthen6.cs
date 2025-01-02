using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strengthen6 : PanelClose
{
    protected override void ClosePanel()
    {
        Player player = FindFirstObjectByType<Player>();
        player.BulletCount++;
        base.ClosePanel();
    }
}
