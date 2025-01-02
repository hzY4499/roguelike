using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strengthen4 : PanelClose
{
    protected override void ClosePanel()
    {
        PlayerHealth playerHealth = FindFirstObjectByType<PlayerHealth>();
        playerHealth.maxHealth *= 1.2f;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.localScale *= 1.1f;
        base.ClosePanel();
    }
}
