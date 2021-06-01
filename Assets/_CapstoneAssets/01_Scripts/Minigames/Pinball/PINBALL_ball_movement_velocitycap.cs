﻿using UnityEngine;

public class PINBALL_ball_movement_velocitycap : MonoBehaviour
{

    // *** Thanks to Ehren of https://answers.unity.com/questions/9985/limiting-rigidbody-velocity.html thread

    // This MonoBehaviour uses hard clamping to limit the velocity of a rigidbody.


    // The maximum allowed velocity. The velocity will be clamped to keep 
    // it from exceeding this value.
    public float maxVelocity;


    // The cached rigidbody reference.
    Rigidbody2D rb;

    // A cached copy of the squared max velocity. Used in FixedUpdate.
    float sqrMaxVelocity;


    // Awake is a built-in unity function that is called called only once during the lifetime of the script instance.
    // It is called after all objects are initialized.
    // For more info, see:
    // http://unity3d.com/support/documentation/ScriptReference/MonoBehaviour.Awake.html
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetMaxVelocity(maxVelocity);
    }


    // Sets the max velocity and calculates the squared max velocity for use in FixedUpdate.
    // Outside callers who wish to modify the max velocity should use this function. Otherwise,
    // the cached squared velocity will not be recalculated.
    protected void SetMaxVelocity(float maxVelocity)
    {
        this.maxVelocity = maxVelocity;
        sqrMaxVelocity = maxVelocity * maxVelocity;
    }


    // FixedUpdate is a built-in unity function that is called every fixed framerate frame.
    // We use FixedUpdate instead of Update here because the docs recommend doing so when
    // dealing with rigidbodies.
    // For more info, see:
    // http://unity3d.com/support/documentation/ScriptReference/MonoBehaviour.FixedUpdate.html 
    void FixedUpdate()
    {
        var v = rb.velocity;
        // Clamp the velocity, if necessary
        // Use sqrMagnitude instead of magnitude for performance reasons.
        if (v.sqrMagnitude > sqrMaxVelocity)
        { // Equivalent to: rigidbody.velocity.magnitude > maxVelocity, but faster.
          // Vector3.normalized returns this vector with a magnitude 
          // of 1. This ensures that we're not messing with the 
          // direction of the vector, only its magnitude.
            rb.velocity = v.normalized * maxVelocity;
        }
    }
}
