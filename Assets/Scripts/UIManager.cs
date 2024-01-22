using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton

    private static UIManager _instance;

    public static UIManager Instance => _instance;

    #endregion

    public Text Target;
    public Text ScoreText;
    public Text LivesText;
    public Text HighscoreText;
    public int Score { get; set; }

    private void Awake()
    {
        Bricks.OnBrickDestruction += OnBrickDestruction;
        BricksManager.OnLevelLoaded += OnLevelLoaded;
        GameManager.OnLiveLost += OnLiveLost;
        _instance = this;
    }
    private void Start()
    {
        OnLiveLost(GameManager.Instance.AvailibleLives);
        // set highscore text
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

        if (this.Score > GameManager.Instance.restAPI.Highscore)
            HighscoreText.text = $"SCOR MAX\n{scoreString}";
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
