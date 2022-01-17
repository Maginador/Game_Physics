using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidDrag : MonoBehaviour
{
    [Range(1, 2)] 
    public float velocityExponent;      //None

    public float dragConstant;          //??

    private PhysicsEngine engine; 
    void Start()
    {
        engine = GetComponent<PhysicsEngine>();
    }

    private void FixedUpdate()
    {
        var velocityVector = engine.velocityVector;
        var speed = velocityVector.magnitude;
        var dragSize = CalculateDrag(speed);
        var dragVector = dragSize * -velocityVector.normalized;
        engine.AddFOrce(dragVector);
    }

    float CalculateDrag(float velocity)
    {
        return dragConstant * Mathf.Pow(velocity, velocityExponent);
    }
}
