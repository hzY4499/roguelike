using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isShooting;
    [SerializeField] private bool autoAtacking;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    public PlayerRotation playerRotation;

    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float MoveX = Input.GetAxis("Horizontal");
        float MoveY = Input.GetAxis("Vertical");

        if (!autoAtacking)
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0)) isShooting = true;
            else isShooting = false;

        float initspeed = playerRotation.GetInitSpeed();
        if (isShooting) playerRotation.speed = 3 * initspeed;
        else playerRotation.speed = initspeed;   

        Vector3 vMove = new Vector3(MoveX, MoveY);
        Vector3 newPosition = transform.position + (moveSpeed * Time.deltaTime * vMove);
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    }
}
