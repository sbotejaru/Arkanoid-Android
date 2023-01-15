using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;


public class Bricks : MonoBehaviour
{
    public int HitPoints = 1;
    public ParticleSystem DestroyEffect;
    private SpriteRenderer sr;

    public static event Action<Bricks> OnBrickDestruction;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        ApplyCollisionLogic(ball);
    }

    private void ApplyCollisionLogic(Ball ball)
    {
        --this.HitPoints;

        if (this.HitPoints <= 0)
        {
            BricksManager.Instance.RemainingBricks.Remove(this);
            OnBrickDestruction?.Invoke(this);
            OnBrickDestroy();
            SpawnDestroyEffect();
            Destroy(this.gameObject);
        }
        else
        {
            this.sr.sprite = BricksManager.Instance.Sprites[this.HitPoints -1];
        }
    }

    private void OnBrickDestroy()
    {
        float buffChance = UnityEngine.Random.Range(0, 100f);
        float debuffChance = UnityEngine.Random.Range(0, 100f);
        bool alreadySpawned = false;

        if (buffChance <= CollectibleManager.Instance.BuffChance)
        {
            alreadySpawned = true;
            Collectible newBuff = this.SpawnCollectible(true);
        }

        if (debuffChance <= CollectibleManager.Instance.DebuffChance && !alreadySpawned)
        {
            Collectible newDebuff = this.SpawnCollectible(false);
        }
    }

    private Collectible SpawnCollectible(bool isBuff)
    {
        List<Collectible> collection;

        if (isBuff)
        {
            collection = CollectibleManager.Instance.AvailableBuffs;
        }
        else
        {
            collection = CollectibleManager.Instance.AvailableDebuffs;
        }

        int index = UnityEngine.Random.Range(0, collection.Count);
        Collectible prefab = collection[index];
        Collectible newCollectible = Instantiate(prefab, this.transform.position, Quaternion.identity) as Collectible;

        return newCollectible;
    }

    private void SpawnDestroyEffect()
    {
        Vector3 brickPos = gameObject.transform.position;
        Vector3 spawnPosition = new Vector3(brickPos.x, brickPos.y, brickPos.z - 0.2f);
        GameObject effect = Instantiate(DestroyEffect.gameObject, spawnPosition, Quaternion.identity);

        MainModule mm = effect.GetComponent<ParticleSystem>().main;
        mm.startColor = this.sr.color;
        Destroy(effect, DestroyEffect.main.startLifetime.constant);
    }

    // Start is called before the first frame update
    private void Awake()
    {
        this.sr = this.GetComponent<SpriteRenderer>();

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Transform containerTransform, Sprite sprite, Color color, int hitpoints)
    {
        this.transform.SetParent(containerTransform);
        this.sr.sprite = sprite;
        this.sr.color = color;
        this.HitPoints = hitpoints;
    }
}
