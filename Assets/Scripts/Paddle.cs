using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    #region Singleton

    private static Paddle _instance;

    public static Paddle Instance => _instance;

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

    private Camera mainCamera;
    private float paddleInitialY;
    private float defaultWidth = 200;
    private float defaultLeftClamp = 280;
    private float defaultRightClamp = 820;
    private SpriteRenderer sr;
    Touch touch;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        paddleInitialY = this.transform.position.y;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PaddleMovement();
    }
    
    void PaddleMovement()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            float paddleShift = (defaultWidth - ((defaultWidth / 2) * this.sr.size.x));
            float leftClamp = defaultLeftClamp - paddleShift;
            float rightClamp = defaultRightClamp + paddleShift;
            float touchPositionPixels = Mathf.Clamp(touch.position.x, leftClamp, rightClamp);

            float touchPositionX = mainCamera.ScreenToWorldPoint(new Vector3(touchPositionPixels, 0, 0)).x;

            this.transform.position = new Vector3(touchPositionX, paddleInitialY, 0);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
    if (coll.gameObject.tag =="Ball")
    {
        Rigidbody2D ballRb = coll.gameObject.GetComponent<Rigidbody2D>();
        Vector3 hitPoint = coll.contacts[0].point;
        Vector3 paddleCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        
        ballRb.velocity = Vector2.zero;

        float difference = paddleCenter.x - hitPoint.x;

        if(hitPoint.x < paddleCenter.x)
        {
            ballRb.AddForce(new Vector2(-(Mathf.Abs(difference * 10520)), BallsManager.Instance.initialBallSpeed));
        }
        else
        {
            ballRb.AddForce(new Vector2((Mathf.Abs(difference * 10520)), BallsManager.Instance.initialBallSpeed));
        }

    }
    
    }
}
