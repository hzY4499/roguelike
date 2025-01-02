using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomDrops : DropItems
{
    public static Action<BoomDrops> onCollected;
    public Renderer boomRenderer;
    public GameObject boomCollider;

    void Start()
    {
        
    }
    protected override void Collected()
    {
        onCollected?.Invoke(this);
    }
}
