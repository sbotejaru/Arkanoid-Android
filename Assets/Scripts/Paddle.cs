using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    #region Singleton

    private static Paddle _instance;

    public static Paddle Instance => _instance;

    public bool PaddleIsTransforming { get; set; }

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
    private BoxCollider2D boxCol;
    private Touch touch;

    public float extendShrinkDuration = 10;
    public float paddleWidth = 2;
    public float paddleHeight = 0.20f;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        paddleInitialY = this.transform.position.y;
        sr = GetComponent<SpriteRenderer>();
        boxCol = GetComponent<BoxCollider2D>();
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
        if (coll.gameObject.tag == "Ball")
        {
            Rigidbody2D ballRb = coll.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = coll.contacts[0].point;
            Vector3 paddleCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

            ballRb.velocity = Vector2.zero;

            float difference = paddleCenter.x - hitPoint.x;

            if (hitPoint.x < paddleCenter.x)
            {
                ballRb.AddForce(new Vector2(-(Mathf.Abs(difference * 200)), BallsManager.Instance.initialBallSpeed));
            }
            else
            {
                ballRb.AddForce(new Vector2((Mathf.Abs(difference * 200)), BallsManager.Instance.initialBallSpeed));
            }

        }

    }

    public void StartWidthAnimation(float newWidth)
    {
        StartCoroutine(AnimatePaddleWidth(newWidth));
    }

    private IEnumerator AnimatePaddleWidth(float newWidth)
    {
        this.PaddleIsTransforming = true;
        this.StartCoroutine(ResetPaddleWidthAfterTime(this.extendShrinkDuration));

        if (newWidth > this.sr.size.x)
        {
            float currentWidth = this.sr.size.x;
            while (currentWidth < newWidth)
            {
                currentWidth += Time.deltaTime * 2;
                this.sr.size = new Vector2(currentWidth, paddleHeight);
                boxCol.size = new Vector2(currentWidth, paddleHeight);

                yield return null;
            }
        }
        else
        {
            float currentWidth = this.sr.size.x;
            while (currentWidth > newWidth)
            {
                currentWidth -= Time.deltaTime * 2;
                this.sr.size = new Vector2(currentWidth, paddleHeight);
                boxCol.size = new Vector2(currentWidth, paddleHeight);

                yield return null;
            }
        }

        this.PaddleIsTransforming = false;
    }

    private IEnumerator ResetPaddleWidthAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        this.StartWidthAnimation(this.paddleWidth);
    }
}
