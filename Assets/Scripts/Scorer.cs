using System;
using UnityEngine;

 public class Scorer : MonoBehaviour
{

    [SerializeField] private int score;
    private Action<int> _scoreAction;
    public int GetScore()
    {
        return score;
    }

    public void AddScore(int scoreAdd)
    {
        score += scoreAdd;
        if (_scoreAction != null)
        {
            _scoreAction.Invoke(score);
        }
    }

    public void AddScoreUpdateListener(Action<int> action)
    {
        _scoreAction += action;
    }
}