using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _strikeAnimationKey;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.SetBool(_strikeAnimationKey, true);
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            _animator.SetBool(_strikeAnimationKey, false);
        }
    }
}
