using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetInteger("Run", 1);
    }

    public void Restart()
    {
        _animator.SetInteger("Run", 1);
        _animator.SetBool("Victory", false);
    }

    public void Win()
    {
        _animator.SetInteger("Run", 0);
        _animator.SetBool("Victory", true);
    }
}
