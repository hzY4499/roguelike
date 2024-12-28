using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float initSpeed; // ����״̬���ٶ�
    public float speed;                       // ʵ���ٶ�
    private Vector3 rotationSpeed;
    private Transform childTransform;

    void Start()
    {
        childTransform = transform.GetChild(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rotationSpeed = new Vector3(0, 0, 10 * speed);
        transform.Rotate(rotationSpeed * Time.deltaTime);
        childTransform.Rotate(-2 * Time.deltaTime * rotationSpeed);
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
