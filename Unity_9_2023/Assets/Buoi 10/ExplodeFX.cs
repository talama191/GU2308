using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeFX : MonoBehaviour
{
    [SerializeField] private AnimationClip clip;
    private Animation animation;

    void Start()
    {
        animation = GetComponent<Animation>();
        animation.Play("explode", PlayMode.StopAll);
    }
}
