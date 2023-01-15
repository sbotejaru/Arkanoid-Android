using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


public class BricksManager : MonoBehaviour
{
    #region Singleton

    private static BricksManager _instance;

    public static BricksManager Instance => _instance;

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

    public static event Action OnLevelLoaded;

    public Sprite[] Sprites;
    public List<int[,]> LevelsData { get; set; }

    private GameObject bricksContainer;

    private float shiftAmount = 0.365f;

    public List<Bricks> RemainingBricks { get; set; }

    private float initialBrickSpawnPositionX = -1.96f;
    private float initialBrickSpawnPositionY = -0.825f;

    public int CurrentLevel;

    public int InitialBricksCount { get; set; }

    public Color[] BrickColors;

    public Bricks brickPrefab;

    private int maxRows = 17;
    private int maxCols = 12;

    private void Start()
    {
        this.bricksContainer = new GameObject("BricksContainer");
        this.RemainingBricks = new List<Bricks>();
        this.LevelsData = this.LoadLevelsData();
        this.GenerateBricks();
    }

    private void GenerateBricks()
    {
        this.RemainingBricks = new List<Bricks>();
        int[,] currentLevelData = this.LevelsData[this.CurrentLevel];
        float currentSpawnX = initialBrickSpawnPositionX;
        float currentSpawnY = initialBrickSpawnPositionY;
        float zShift = 0;

        for (int row = 0; row < this.maxRows; row++)
        {
            for (int col = 0; col < this.maxCols; col++)
            {
                int brickType = currentLevelData[row, col];

                if (brickType > 0)
                {
                    Bricks newBrick = Instantiate(brickPrefab, new Vector3(currentSpawnX, currentSpawnY, 0.0f - zShift), Quaternion.identity) as Bricks;
                    newBrick.Init(bricksContainer.transform, this.Sprites[brickType - 1], this.BrickColors[brickType], brickType);

                    this.RemainingBricks.Add(newBrick);
                    zShift += 0.0001f;
                }

                currentSpawnX += shiftAmount;
                if (col + 1 == this.maxCols)
                {
                    currentSpawnX = initialBrickSpawnPositionX;
                }

            }

            currentSpawnY += shiftAmount;
        }

        this.InitialBricksCount = this.RemainingBricks.Count;
        OnLevelLoaded?.Invoke();
    }


    private List<int[,]> LoadLevelsData()
    {
        TextAsset text = Resources.Load("levels") as TextAsset;

        string[] rows = text.text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        List<int[,]> levelsData = new List<int[,]>();
        int[,] currentLevel = new int[maxRows, maxCols];
        int currentRow = 0;

        for (int row = 0; row < rows.Length; row++)
        {
            string line = rows[row];

            if (line.IndexOf("--") == -1)
            {
                string[] bricks = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int col = 0; col < bricks.Length; col++)
                {
                    currentLevel[currentRow, col] = int.Parse(bricks[col]);
                }

                currentRow++;
            }
            else
            {
                // end of current level
                // add the matrix to the last and continue the loop
                currentRow = 0;
                levelsData.Add(currentLevel);
                currentLevel = new int[maxRows, maxCols];
            }
        }

        return levelsData;
    }



    public void LoadLevel(int level)
    {
        this.CurrentLevel = level;
        this.ClearRemaniningBricks();
        this.GenerateBricks();

    }

    private void ClearRemaniningBricks()
    {
        foreach (Bricks brick in this.RemainingBricks.ToList())
        {
            Destroy(brick.gameObject);
        }
    }

    public void LoadNextLevel()
    {
        this.CurrentLevel++;

        if (this.CurrentLevel >= this.LevelsData.Count)
        {
            GameManager.Instance.ShowVictoryScreen();
        }
        else
        {
            this.LoadLevel(this.CurrentLevel);
        }

    }



}
