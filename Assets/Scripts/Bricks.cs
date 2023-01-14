using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour
{
    public int HitPoints = 1;
    public ParticleSystem DestroyEffect;

    public static event Action<Bricks> OnBrickDestruction;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Ball ball = collision.gameObject.GetComponent<Ball>();
        //ApplyCollisionLogic(ball);
    }

    /*private void ApplyCollisionLogic(Ball ball)
    {
        --this.HitPoints;

        if (this.HitPoints <= 0)
        {
            // SpawnDestroyEffect();
            Destroy(this.gameObject);
        }
    }*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
