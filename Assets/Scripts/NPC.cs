using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue;
    private bool can_Interact = false;
    public Animator win_animcontroller;
    public AnimationClip  win_animation;

    void Update()
    {
        if (can_Interact && Input.GetKeyDown(KeyCode.X))
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<Dialogue_Manager>().StartDialogue(dialogue);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        can_Interact = true;
        win_animcontroller.Play(win_animation.name);
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        can_Interact = false;
    }
}
