using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strengthen7 : PanelClose
{
    protected override void ClosePanel()
    {
        PlayerHealth playerHealth = FindFirstObjectByType<PlayerHealth>();
        playerHealth.maxHealth *= 1.1f;
        Player player = FindFirstObjectByType<Player>();
        player.BulletDamage *= 1.2f;
        base.ClosePanel();
    }
}
