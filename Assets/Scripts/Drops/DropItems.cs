using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DropItems : MonoBehaviour, ICollectable
{
    protected bool collected;

    private void OnEnable()
    {
        collected = false;
    }
    public void Collect(Player player)
    {
        if (collected) return;
        collected = true;
        StartCoroutine(MoveTowardsPlayer(player));
    }

    IEnumerator MoveTowardsPlayer(Player player)
    {
        float timer = 0;
        Vector2 initialPosition = transform.position;

        while (timer < 1)
        {
            Vector2 targetPosition = player != null ? player.transform.position : transform.position;
            transform.position = Vector2.Lerp(initialPosition, targetPosition, timer);
            timer += Time.deltaTime;
            yield return null;
        }

        Collected();
    }

    protected abstract void Collected();
}
