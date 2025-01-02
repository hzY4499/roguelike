using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strengthen3 : PanelClose
{
    protected override void ClosePanel()
    {
        Player player = FindFirstObjectByType<Player>();
        player.moveSpeed *= 1.1f;
        player.spreadAngle *= 1.2f;
        base.ClosePanel();
    }
}
