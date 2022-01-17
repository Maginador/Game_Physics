using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class RocketEngine : MonoBehaviour
{
    public float fuelMass;              // [kg]
    public float maxThrust;             // kN [kg m s^-2]
    [Range(0f,1f)]
    public float thrustPercent;         //[none]
    public Vector3 thrustUnitVector;    //[none] 
    
    private PhysicsEngine engine;

    private float currentThrust; 
    // Start is called before the first frame update
    void Start()
    {
        engine = GetComponent<PhysicsEngine>();
        engine.mass += fuelMass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fuelMass > FuelThisUpdate())
        {
            fuelMass -= FuelThisUpdate();
            engine.mass -= FuelThisUpdate();
            ExtertForce();
        }
        else
        {
            Debug.LogWarning("Not enough fuel!!");
        }
    }

    private float FuelThisUpdate()
    {
        const float effectiveExhaustVelocity = 4462f;
        var exhaustMassFlow = currentThrust / effectiveExhaustVelocity;
        return exhaustMassFlow * Time.deltaTime;
    }

    private void ExtertForce()
    {
        currentThrust = thrustPercent * maxThrust * 1000f;
        Vector3 thrustVector = thrustUnitVector.normalized * currentThrust; // N
        engine.AddFOrce(thrustVector);
        
    }
}
