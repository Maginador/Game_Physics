using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class UniversalGravitation : MonoBehaviour
{
    private PhysicsEngine[] _physicsEnginesArray;
    private const float BigG = 6.673e-11f; // [m^3 s^-2 kg^-1]


    private void Start ()
    {
        _physicsEnginesArray = FindObjectsOfType<PhysicsEngine>();
    }

    private void FixedUpdate()
    {
        CalculateGravity();
    }

    private void CalculateGravity()
    {
        foreach (var engine1 in _physicsEnginesArray)
        {
            foreach (var engine2 in _physicsEnginesArray)
            {
                if (engine1 != engine2) 
                {
                    var offset = engine1.transform.position - engine2.transform.position;
                    var rSquared = Mathf.Pow(offset.magnitude, 2f);
                    if (rSquared == 0) continue;
                    var gravityMagnitude = (BigG * engine1.mass * engine2.mass) / rSquared;
                    var gravityFeltVector = gravityMagnitude * offset.normalized;
                    engine1.AddFOrce(-gravityFeltVector);
                }
            }
        }
    }
}