using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupUI : MonoBehaviour
{
    public Animator anim_controller;
    public AnimationClip anim;
    // Start is called before the first frame update
    void Start()
    {
        anim_controller = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            anim_controller.Play(anim.name);
        }
    }
}
