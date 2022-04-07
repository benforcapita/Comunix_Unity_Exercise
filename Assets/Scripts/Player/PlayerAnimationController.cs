using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    //animator
    [SerializeField] private Animator animator;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    private void Update()
    {
        //get player input
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        //if horizontal is not 0 set animator bool to true
        animator.SetBool(IsWalking, (horizontal != 0) || (vertical != 0));
    }
}