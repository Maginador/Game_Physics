using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetSpawner : MonoBehaviour
{

    [SerializeField] private Scorer score;
    [SerializeField] private Target targetPrefab;
    private readonly List<Target> _targets = new List<Target>();
    [SerializeField] private Vector2 xMinMax = new Vector2(10, 35), yMinMax = new Vector2(5, 20);

    private float _timer;
    [SerializeField] private float delayTime = 5.0f;
    private void Start()
    {
        SpawnTargets();
    }

    private async void SpawnTargets()
    {
        while (true)
        {
            if (_timer <= Time.time)
            {
                _timer = Time.time + delayTime;
                var target = Instantiate(targetPrefab);
                var randomPosition = new Vector3(Random.Range(xMinMax.x, xMinMax.y), Random.Range(yMinMax.x, yMinMax.y), 0);
                target.PassReferences(score, this);
                target.transform.position = randomPosition;
               _targets.Add(target);
            }
            await Task.Yield();
            if (!Application.isPlaying)
                return;
        }
    }

    public int GetQuantityOfTargetsLeft()
    {
        return _targets.Count;
    }

    public void RemoveTarget(Target target)
    {
        _targets.Remove(target);
        
    }
}