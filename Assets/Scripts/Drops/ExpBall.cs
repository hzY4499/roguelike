using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBall : MonoBehaviour
{
    private PlayerLevel playerLevel;
    private bool collected;
    // Start is called before the first frame update
    void Start()
    {
        playerLevel = FindAnyObjectByType<PlayerLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Vector2 targetPosition = player.transform.position;
            transform.position = Vector2.Lerp(initialPosition, targetPosition, timer);
            timer += Time.deltaTime;
            yield return null;
        }

        Collected();
    }    

    private void Collected()
    {
        playerLevel.OnBallPicked();
        Destroy(gameObject);
    }
}
