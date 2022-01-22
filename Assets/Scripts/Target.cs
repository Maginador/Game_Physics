using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    private Scorer _scorer;
    private TargetSpawner _spawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            _scorer.AddScore(1);
            _spawner.RemoveTarget(this);
            Destroy(gameObject);
        }
    }

    public void PassReferences(Scorer targetScorer, TargetSpawner spawner)
    {
        _scorer = targetScorer;
        _spawner = spawner;
    }
}
