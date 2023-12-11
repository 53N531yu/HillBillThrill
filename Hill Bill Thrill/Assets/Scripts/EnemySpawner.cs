using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //singleton
    public static EnemySpawner Instance { get; private set; }

    public GameObject player;
    public GameObject enemy;
    public int difficulty=1;
    public int enemiesLeft=1;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesLeft == 0)
        {
            difficulty++;
            enemiesLeft = difficulty;
            createWave(difficulty);
        }
    }

    void createWave(int enemiestoSpawn)
    {
        for(int i = 0; i < enemiestoSpawn; i++)
        {
            float x = Random.Range(-25, 25);
            float y = Random.Range(-25, 25);



            Instantiate(enemy, new Vector2(x,y), Quaternion.identity);
        }
    }

    public void reduce()
    {
        //Debug.Log("reduce works");
        enemiesLeft--;
    }
}
