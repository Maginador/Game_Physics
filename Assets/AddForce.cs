using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    [SerializeField] private Vector3 force;

    private PhysicsEngine engine;
    // Start is called before the first frame update
    void Start()
    {
        engine = GetComponent<PhysicsEngine>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        engine.AddFOrce(force);
    }
}
