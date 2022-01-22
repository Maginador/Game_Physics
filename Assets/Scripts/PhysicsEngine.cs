using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhysicsEngine : MonoBehaviour {
    public float mass = 1f;         //[kg]
    public Vector3 velocityVector;  //[m sˆ1]
    public Vector3 netForceVector;  //N [kg m s^-2]
    private List<Vector3> _forceVectorList = new List<Vector3>();

    void Start ()
    {
        SetupTrails();
    }
	
    void FixedUpdate ()
    {
        RenderTrails();
        SumForces ();
        UpdateVelocity ();
    }

    public void AddFOrce(Vector3 force)
    {
        _forceVectorList.Add(force);
    }

    void SumForces () {
        netForceVector = Vector3.zero;
		
        foreach (Vector3 forceVector in _forceVectorList) {
            netForceVector = netForceVector + forceVector;
        }

        _forceVectorList = new List<Vector3>();
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
    private LineRenderer _lineRenderer;
    private int _numberOfForces;

    void SetupTrails () {
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.SetColors(Color.yellow, Color.yellow);
        _lineRenderer.SetWidth(0.2F, 0.2F);
        _lineRenderer.useWorldSpace = false;
    }
	
    void RenderTrails () {
        if (showTrails) {
            _lineRenderer.enabled = true;
            _numberOfForces = _forceVectorList.Count;
            _lineRenderer.SetVertexCount(_numberOfForces * 2);
            int i = 0;
            foreach (Vector3 forceVector in _forceVectorList) {
                _lineRenderer.SetPosition(i, Vector3.zero);
                _lineRenderer.SetPosition(i+1, -forceVector);
                i = i + 2;
            }
        } else {
            _lineRenderer.enabled = false;
        }
    }
}