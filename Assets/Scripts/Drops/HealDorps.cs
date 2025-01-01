using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealDrops : DropItems
{
    private PlayerHealth playerHealth;
    public static Action<HealDrops> onCollected;

    void Start()
    {
        playerHealth = FindAnyObjectByType<PlayerHealth>();
    }
    protected override void Collected()
    {
        playerHealth.Recovery(5f);
        onCollected?.Invoke(this);
    }
}
