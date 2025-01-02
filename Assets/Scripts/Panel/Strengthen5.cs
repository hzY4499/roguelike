using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strengthen5 : PanelClose
{
    protected override void ClosePanel()
    {
        Player player = FindFirstObjectByType<Player>();
        player.BulletDamage *= 1.2f;
        player.BulletSize *= 1.1f;
        base.ClosePanel();
    }
}
