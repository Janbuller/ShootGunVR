using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInfo
{
    public GameObject EnemyPrefab;
    public bool GoodEnemy;
}

public class MonsterSpawner : MonoBehaviour
{
    public GameObject Player;
    private PlayerController PlayerCtrl;

    public EnemyInfo[] Enemies;
    private EnemyInfo NextEnemy;
    [Header("The chance for a reroll of next enemy if it's a good enemy.\nChance = 1/x")]
    public int GoodRerollChance = 10;

    public double TimeBetweenEnemies = 2;
    private double SpawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        PlayerCtrl = Player.GetComponent<PlayerController>();
        SpawnTimer = TimeBetweenEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnTimer <= 0)
        {
            GetNextEnemy();
            SpawnEnemy();
            SpawnTimer = TimeBetweenEnemies;
        }

        SpawnTimer -= Time.deltaTime;
    }

    void GetNextEnemy()
    {
        bool reroll = true;

        System.Random rand = new System.Random();

        int index = rand.Next(0, Enemies.Length);
        if(!Enemies[index].GoodEnemy)
            reroll = false;

        while (reroll)
        {
            index = rand.Next(0, Enemies.Length);
            reroll = rand.Next(0, GoodRerollChance) == 0 ? false : true;
        }

        NextEnemy = Enemies[index];
    }

    void SpawnEnemy()
    {
        System.Random rand = new System.Random();
        float DistanceToPlayer = NextEnemy.GoodEnemy ? 20 : 50;

        float Angle = (float)(rand.NextDouble() * Mathf.PI * 2);

        float xPos = Mathf.Cos(Angle) * DistanceToPlayer;
        float yPos = Mathf.Sin(Angle) * DistanceToPlayer;

        Vector3 Pos = new Vector3(xPos, 5.85f, yPos);

        GameObject Spawned = Instantiate(NextEnemy.EnemyPrefab, Pos, Quaternion.identity);
        if(!NextEnemy.GoodEnemy)
            Spawned.GetComponent<EnemyController>().PlayerCtrl = PlayerCtrl;
        else
            Spawned.GetComponent<GoodEnemyController>().PlayerCtrl = PlayerCtrl;
    }
}
