using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //singleton
    public static EnemySpawner Instance { get; private set; }

    public GameObject player;
    public GameObject enemy;
    public GameObject upgrade;
    public int difficulty=1;
    public int enemiesLeft=1;

    public float enemyspeed = 4;
    public TrailRenderer trail;

    public bool enemytrailactive = false;

    public TMP_Text scoreUI;
    public float score = 0;

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
        trail.enabled = false;
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

        scoreUI.SetText(score.ToString());
    }

    public void createWave(int enemiestoSpawn)
    {
        Debug.Log("spawning wave");
        Debug.Log("enemiestospawn is " + enemiestoSpawn);

        //spawns upgrade every 10 waves
        if (difficulty % 5 == 0)
        {
            float x = Random.Range(-25, 25);
            float y = Random.Range(-25, 25);
            Instantiate(upgrade, new Vector2(x, y), Quaternion.identity);
        }

        //spawns enemies
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


    //add upgrade effects here
    //rn it sets trails for everyone and reduces enemy speed for 10 seconds
    public IEnumerator traildecay()
    {
        //
        trail.Clear();
        trail.enabled = true;
        enemytrailactive = true;
        EnemySpawner.Instance.enemyspeed = 1f;

        yield return new WaitForSeconds(10);
        trail.enabled = false;
        enemytrailactive = false;
        EnemySpawner.Instance.enemyspeed = 5f;
    }

    public void traildecaywrapper()
    {
        StartCoroutine(traildecay());
    }
}
