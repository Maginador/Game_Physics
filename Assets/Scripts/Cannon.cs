using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    [SerializeField] private GameObject cannonPivot;
    [SerializeField] private PhysicsEngine ballPrefab;
    private bool _isShooting;
    private float _power;

    [SerializeField] private float basePower;
    private float _finalRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isShooting)
            UpdateCannonRotation();
        if (Input.GetButtonDown("Fire1"))
        {
            if (!_isShooting)
            {
                StartShoot();
            }
            else
            {
                Shoot();
            }
        }
    }

    private async void StartShoot()
    {
        _isShooting = true;
        while (true)
        {
            if (!_isShooting)
            {
                return;
            }
            _power = 1 - Mathf.Lerp(0.0f, 0.9f, Mathf.Abs(Mathf.Sin(Time.time)));
            await Task.Yield();
            
        }
    }

    private void UpdateCannonRotation()
    {
        var mousePosition = Input.mousePosition;
        var rotation = (Screen.height - mousePosition.y);
        
        //Linear convertion formula 
        //(((Oldvalue - oldMin) * newRange)/oldRange) + newMin
        var oldRange = (Screen.height - 0);
        var newRange = (45 - (-45));
        var convertedRotation = (((rotation - 0) * newRange) / oldRange) + (-45);

        //Convert Range
        _finalRotation = Mathf.Clamp(-convertedRotation, -45, 45);
        var localEulerAngles = cannonPivot.transform.localEulerAngles;
        localEulerAngles = new Vector3(localEulerAngles.x,
            localEulerAngles.y, _finalRotation);
        cannonPivot.transform.localEulerAngles = localEulerAngles;
    }

    private void Shoot()
    {
        _isShooting = false;
        var ball = Instantiate(ballPrefab);
        var force = (cannonPivot.transform.localPosition - cannonPivot.transform.up);
        force = new Vector3(-force.x, -force.y -1, force.z);

        ball.AddFOrce(force * (_power * basePower));
        _power = 0;

    }

    public void UpdateUI()
    {
        
    }

    public float GetPower()
    {
        return _power;
    }
}
