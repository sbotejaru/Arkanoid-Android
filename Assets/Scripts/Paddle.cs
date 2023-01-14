using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Camera mainCamera;
    private float paddleInitialY;
    Touch touch;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        paddleInitialY = this.transform.position.y;
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

            float touchPositionX = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, 0, 0)).x;

            this.transform.position = new Vector3(touchPositionX, paddleInitialY, 0);
        }
        
    }
}
