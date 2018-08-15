﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    float moveSpeed = 6;
    float gravity = -20;
    Vector2 velocity;


    Controller2D controller;

    void Start()
    {

        controller = GetComponent<Controller2D>();
    }

    private void FixedUpdate()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        velocity.x = input.x * moveSpeed;
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}