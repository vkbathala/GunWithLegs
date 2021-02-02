using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeemonBehavior : Enemy
{
    public float speed = -1.0f;

    private float timer = 0.0f;

    public GameObject beemon;

    private float o_beemonx;
    private float o_beemony;
    private bool facingRight = false;

    void Start()
    {
        o_beemonx = beemon.transform.position.x;
        o_beemony = beemon.transform.position.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float seconds = timer % 60;

        if (seconds >= 0.5f)
        {
            timer = 0.0f;
            Flip();
        }

        transform.Translate(speed * Time.deltaTime, 0, 0);
    
    }


    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		transform.Rotate(0f, 180f, 0f);
	}
}
