using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorResetter : MonoBehaviour {

    public Animator animator;
    public RuntimeAnimatorController controller;

    private void OnEnable()
    {
        if (animator == null)
        {
            animator = gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = controller;
        }
    }
    private void OnDisable()
    {
        Destroy(animator);
    }
}
