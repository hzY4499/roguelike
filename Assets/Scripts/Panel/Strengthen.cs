using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strengthen : PanelClose
{
    protected override void ClosePanel()
    {
        Player player = FindFirstObjectByType<Player>();
        player.ShootDelay *= 0.9f;
        player.spreadAngle *= 1.4f;
        base.ClosePanel();
    }
}
