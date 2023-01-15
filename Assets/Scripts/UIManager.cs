using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text Target;
    public Text ScoreText;
    public Text LivesText;
    public int Score { get; set; }

    private void Awake()
    {
        Bricks.OnBrickDestruction += OnBrickDestruction;
        BricksManager.OnLevelLoaded += OnLevelLoaded;
        GameManager.OnLiveLost += OnLiveLost;
    }
    private void Start()
    {
        OnLiveLost(GameManager.Instance.AvailibleLives);
    }
    private void OnLiveLost(int remainingLives)
    {
        LivesText.text = $"VIETI\n{remainingLives}/3";
    }
    private void OnLevelLoaded()
    {
        UpdateRemainingBricksText();
        UpdateScoreText(0);
    }
    private void UpdateScoreText(int increment)
    {
        this.Score += increment;
        string scoreString = this.Score.ToString().PadLeft(5, '0');
        ScoreText.text = $"SCOR\n{scoreString}";
    }
    private void OnBrickDestruction(Bricks obj)
    {
        UpdateRemainingBricksText();
        UpdateScoreText(10);
    }
    private void UpdateRemainingBricksText()
    {
        Target.text = $"TINTA\n{BricksManager.Instance.RemainingBricks.Count}/{BricksManager.Instance.InitialBricksCount}";
    }
    private void OnDisable()
    {
        Bricks.OnBrickDestruction -= OnBrickDestruction;
        BricksManager.OnLevelLoaded -= OnLevelLoaded;
    }
}
