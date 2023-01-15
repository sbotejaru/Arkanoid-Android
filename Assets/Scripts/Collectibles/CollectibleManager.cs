using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    #region Singleton

    private static CollectibleManager _instance;

    public static CollectibleManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }

    #endregion

    public List<Collectible> AvailableBuffs;
    public List<Collectible> AvailableDebuffs;

    [Range(0, 100)]
    public float BuffChance;

    [Range(0, 100)]
    public float DebuffChance;
}
