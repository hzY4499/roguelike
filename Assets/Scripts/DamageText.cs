using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro damageText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Animate(int damage)
    {
        damageText.text = damage.ToString();
        animator.Play("Animate");
    }
}
