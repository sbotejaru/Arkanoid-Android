using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BallsManager : MonoBehaviour
{
    #region Singleton

    private static BallsManager _instance;


    public static BallsManager Instance => _instance;

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

    [SerializeField]
    private Ball ballPrefab;

    private Rigidbody2D initialBallRb;

    private Ball initialBall;

    public List<Ball> Balls { get; set; }

    public float initialBallSpeed = 250;

    Touch touch;

    private void Start()
    {
        InitBall();
    }

    private void Update(){
        if (!GameManager.Instance.IsGameStarted)
        {
            Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
            Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + .30f, 0);
            initialBall.transform.position = ballPosition;

            if (Input.touchCount > 0)
            {
               
            touch = Input.GetTouch(0);   
            initialBallRb.isKinematic = false;
            initialBallRb.AddForce(new Vector2(0,initialBallSpeed));
            GameManager.Instance.IsGameStarted = true;
             
            }

        }
    }

    private void InitBall()
    {
        Vector3 paddlePosition = Paddle.Instance.gameObject.transform.position;
        Vector3 startingPosition = new Vector3(paddlePosition.x, paddlePosition.y + .30f, 0); //get from paddle
        initialBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
        initialBallRb = initialBall.GetComponent<Rigidbody2D>();

        this.Balls = new List<Ball>
        {
            initialBall
        };


    }



    public void ResetBalls()
    {
        foreach (var ball in this.Balls.ToList())
        {
            Destroy(ball.gameObject);
        }
        
        InitBall();
    
    }
}
