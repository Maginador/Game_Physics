using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhysicsEngine : MonoBehaviour {
    public float mass = 1f;
    public Vector3 velocityVector;  // average velocity this FixedUpdate()
    public Vector3 netForceVector;
    public List<Vector3> forceVectorList = new List<Vector3>();

    // Use this for initialization
    void Start ()
    {
        SetupTrails();
    }
	
    void FixedUpdate ()
    {
        RenderTrails();
        SumForces ();
        UpdateVelocity ();
        // Update position
    }

    public void AddFOrce(Vector3 force)
    {
        forceVectorList.Add(force);
    }
    void SumForces () {
        netForceVector = Vector3.zero;
		
        foreach (Vector3 forceVector in forceVectorList) {
            netForceVector = netForceVector + forceVector;
        }

        forceVectorList = new List<Vector3>();
    }
	
    void UpdateVelocity () {
        Vector3 accelerationVector = netForceVector / mass;
        velocityVector += accelerationVector * Time.deltaTime;
        transform.position += velocityVector * Time.deltaTime;

    }
    
    
    /// <summary>
    /// Draw Trails code
    /// </summary>
    public bool showTrails = true;
    private LineRenderer lineRenderer;
    private int numberOfForces;
	
    // Use this for initialization
    void SetupTrails () {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.SetColors(Color.yellow, Color.yellow);
        lineRenderer.SetWidth(0.2F, 0.2F);
        lineRenderer.useWorldSpace = false;
    }
	
    // Update is called once per frame
    void RenderTrails () {
        if (showTrails) {
            lineRenderer.enabled = true;
            numberOfForces = forceVectorList.Count;
            lineRenderer.SetVertexCount(numberOfForces * 2);
            int i = 0;
            foreach (Vector3 forceVector in forceVectorList) {
                lineRenderer.SetPosition(i, Vector3.zero);
                lineRenderer.SetPosition(i+1, -forceVector);
                i = i + 2;
            }
        } else {
            lineRenderer.enabled = false;
        }
    }
}