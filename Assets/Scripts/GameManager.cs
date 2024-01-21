using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public GameObject victoryScreen;
    public GameObject gameOverScreen;
    public GameObject saveUsernameScreen;
    public int AvailibleLives = 3;
    public int Lives { get; set; }
    public bool IsGameStarted { get; set; }

    public static event Action<int> OnLiveLost;

    private void Start()
    {
        //Screen.SetResolution(x,y,false); setare rezolutie googel pixe
        this.Lives = AvailibleLives;

        Ball.OnBallDeath += OnBallDeath;
        Bricks.OnBrickDestruction += OnBrickDestruction;
    }

    private void OnBrickDestruction(Bricks brick)
    {
        if (BricksManager.Instance.RemainingBricks.Count <= 0)
        {
            BallsManager.Instance.ResetBalls();
            GameManager.Instance.IsGameStarted = false;
            BricksManager.Instance.LoadNextLevel();

        }

    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    private void OnBallDeath(Ball obj)
    {
        if (BallsManager.Instance.Balls.Count <= 0)
        {

            this.Lives--;
            if (this.Lives < 1)
            {
                //gameover
                gameOverScreen.SetActive(true);
            }
            else
            {
                //restart level 
                OnLiveLost?.Invoke(this.Lives);
                BallsManager.Instance.ResetBalls();
                IsGameStarted = false;
                BricksManager.Instance.LoadLevel(BricksManager.Instance.CurrentLevel);

            }
        }

    }

    private void OnDisable()
    {
        Ball.OnBallDeath -= OnBallDeath;
    }

    internal void ShowVictoryScreen()
    {

        victoryScreen.SetActive(true);
    }
}
