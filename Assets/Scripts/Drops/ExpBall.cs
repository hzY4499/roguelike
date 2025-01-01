using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExpBall : DropItems
{
    private PlayerLevel playerLevel;
    public static Action<ExpBall> onCollected;
    // Start is called before the first frame update
    void Start()
    {
        playerLevel = FindAnyObjectByType<PlayerLevel>();
    } 

    protected override void Collected()
    {
        playerLevel.OnBallPicked();
        onCollected?.Invoke(this);
    }
}
