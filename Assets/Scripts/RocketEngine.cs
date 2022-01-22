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
    
    private PhysicsEngine _engine;

    private float _currentThrust; 
    private void Start()
    {
        _engine = GetComponent<PhysicsEngine>();
        _engine.mass += fuelMass;
    }

    private void FixedUpdate()
    {
        if (fuelMass > FuelThisUpdate())
        {
            fuelMass -= FuelThisUpdate();
            _engine.mass -= FuelThisUpdate();
            ExertForce();
        }
        else
        {
            Debug.LogWarning("Not enough fuel!!");
        }
    }

    private float FuelThisUpdate()
    {
        const float effectiveExhaustVelocity = 4462f;
        var exhaustMassFlow = _currentThrust / effectiveExhaustVelocity;
        return exhaustMassFlow * Time.deltaTime;
    }

    private void ExertForce()
    {
        _currentThrust = thrustPercent * maxThrust * 1000f;
        var thrustVector = thrustUnitVector.normalized * _currentThrust; // N
        _engine.AddFOrce(thrustVector);
        
    }
}
