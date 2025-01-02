using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strengthen1 : PanelClose
{
    protected override void ClosePanel()
    {
        Player player = FindFirstObjectByType<Player>();
        player.BulletDamage = (int) (player.BulletDamage * 1.2f);
        player.BulletAliveTime *= 1.1f;
        base.ClosePanel();
    }
}
