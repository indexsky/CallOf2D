﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 0f;
    private float movex = 0f;
    private float movey = 0f;

    private LockstepIOComponent lockstep;

    // Use this for initialization
    void Start()
    {
        lockstep = GameObject.Find("NetworkScripts").GetComponent<LockstepIOComponent>();
        gameObject.StoreID();
    }

    // Update is called once per frame
    void Update()
    {

        // Ensure lockstep is ready before issuing commands
        if (lockstep.LockstepReady)
        {
            JSONObject j = new JSONObject();

            // Signal Player movement

            // on up arrow 
            if (Input.GetKey(KeyCode.W))
            {
                j.AddField("y", 1);
                j.AddField("gameobject", gameObject.GetInstanceID());
            }
            // on down arrow
            else if (Input.GetKey(KeyCode.S))
            {
                j.AddField("y", -1);
                j.AddField("gameobject", gameObject.GetInstanceID());
            }
            // on left arrow
            if (Input.GetKey(KeyCode.A))
            {
                j.AddField("x", -1);
                j.AddField("gameobject", gameObject.GetInstanceID());
            }
            // on right arrow
            else if (Input.GetKey(KeyCode.D))
            {
                j.AddField("x", 1);
                j.AddField("gameobject", gameObject.GetInstanceID());
            }
            // issue the command above
            lockstep.IssueCommand(j);

        }

        movex = Input.GetAxis("Horizontal");
        movey = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(movex, movey, 0.0f);
        gameObject.GetComponent<Rigidbody2D>().velocity = movement * moveSpeed;

    }
}