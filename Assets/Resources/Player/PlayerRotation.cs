using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float speed;
    private Vector3 rotationSpeed;
    private Transform childTransform;
    void Start()
    {
        rotationSpeed = new Vector3(0, 0, 10 * speed);
        childTransform = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
       childTransform.Rotate(-2 * rotationSpeed * Time.deltaTime);
    }
}
