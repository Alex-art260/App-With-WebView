using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    public static ScoreCount instance = null;
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int score = 10;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        _scoreText.text = "" + score;
    }
    private void OnEnable()
    {
        MovementObjects.OnMultiplyScore += MultiplyScore;
    }

    private void OnDisable()
    {
        MovementObjects.OnMultiplyScore -= MultiplyScore;
    }

    public void MultiplyScore()
    {
       score = score * 2;
        _scoreText.text = "" + score;
    }
}
