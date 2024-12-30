using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float initSpeed; // 正常状态的速度
    public float speed;                       // 实际速度
    private Vector3 rotationSpeed;
    private Transform childTransform1;
    private Transform childTransform2;

    void Start()
    {
        childTransform1 = transform.GetChild(0);
        childTransform2 = transform.GetChild(1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rotationSpeed = new Vector3(0, 0, 10 * speed);
        childTransform1.Rotate(rotationSpeed * Time.deltaTime);
        childTransform2.Rotate(-2 * Time.deltaTime * rotationSpeed);
    }

    public void SetInitSpeed(float speed)
    {
        this.initSpeed = speed;
    }

    public float GetInitSpeed()
    {
        return initSpeed;
    }    
}
