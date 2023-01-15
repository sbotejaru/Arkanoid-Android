using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    
    public static event Action<Ball> OnBallDeath;    

    public void Die()
    {
    
    OnBallDeath?.Invoke(this);
    Destroy(gameObject, 1);
    
    }
}
