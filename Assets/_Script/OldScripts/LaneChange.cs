using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneChange : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;
    public float turnSpeed = 10f; // Speed at which the player turns
    public float resetTurnSpeed = 5f; // Speed at which the rotation resets to 0

    public void FixedUpdate()
    {
        // Calculate the movement direction
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

        // Handle turning based on horizontal input
        if (variableJoystick.Horizontal != 0)
        {
            float turn = variableJoystick.Horizontal * turnSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
        else
        {
            // Gradually reset rotation to 0 when no horizontal input is detected
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);
            rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, resetTurnSpeed * Time.fixedDeltaTime));
        }
    }
}
