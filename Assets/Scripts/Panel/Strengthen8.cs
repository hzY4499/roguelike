using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strengthen8 : PanelClose
{
    protected override void ClosePanel()
    {
        Player player = FindFirstObjectByType<Player>();
        player.criticalDamage *= 1.3f;
        player.spreadAngle *= 0.9f;
        base.ClosePanel();
    }
}
