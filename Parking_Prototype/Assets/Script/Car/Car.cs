using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float acceleration = 12f;
    public float deceleration = 5f;
    public float maxSpeed = 7f;
    public float rotationSpeed = 100f;
    public float friction = 0.98f;

    private Rigidbody2D rb;
    public float currentSpeed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    int Sign(float x) => x > 0 ? 1 : x < 0 ? -1 : 0;

    void Update()
    {
        // Get user input for forward and backward movement
        float moveInput = 0;

        if(Input.GetKey(KeyCode.UpArrow))
        {
            moveInput = 1;
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            moveInput = -1;
        }

        // Update the car's speed based on input
        if (moveInput == 1 && currentSpeed < maxSpeed)
        {
            // Accelerate forward
            currentSpeed += acceleration * Time.deltaTime;
        }
        else if (moveInput == -1 && currentSpeed > -maxSpeed/2)
        {
            // Decelerate or move backward
            currentSpeed -= deceleration * Time.deltaTime;
        }
        else if(Mathf.Abs(currentSpeed) > 0.1)
        {
            // Natural deceleration (friction)
            currentSpeed += -Sign(currentSpeed) * deceleration * Time.deltaTime;
        }

        // Clamp speed to max limits
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed/2, maxSpeed);
        if(currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }

        // Rotate the car based on horizontal input (left/right arrow)
        float rotationInput = Input.GetAxis("Horizontal");
        float rotationAmount = -rotationInput * rotationSpeed * Time.deltaTime;
        if (moveInput != 0) transform.Rotate(0, 0, rotationAmount);

        // Set the car's velocity in the direction it¡¯s facing
        rb.velocity = transform.up * currentSpeed;
    }
}
