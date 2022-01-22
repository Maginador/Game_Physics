using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScore : MonoBehaviour
{
   [SerializeField] private Scorer scorer;

    [SerializeField] private Text scoreText;
    private void Start()
    {
        scorer.AddScoreUpdateListener(UpdateUI);
    }

    private void UpdateUI(int score)
    {
        scoreText.text = score.ToString();
    }

 
}
