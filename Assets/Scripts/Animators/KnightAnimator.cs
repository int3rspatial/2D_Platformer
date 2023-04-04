using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _attackAnimationKey;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.SetBool(_attackAnimationKey, true);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            _animator.SetBool(_attackAnimationKey, false);
        }
    }
}
