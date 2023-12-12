using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyHealth = 10f;
    public GameObject player;
    public GameObject explosion;
    TrailRenderer trail;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
        trail = GetComponent<TrailRenderer>();
    }
    void Update()
    {
        if (enemyHealth <= 0)
        {
            EnemySpawner.Instance.deathsound();
            EnemySpawner.Instance.score += 500; //add score
            EnemySpawner.Instance.reduce();
            Instantiate(explosion, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);


            Destroy(gameObject);        //destroy enemy
        }
        
        chase();

        if (EnemySpawner.Instance.enemytrailactive == true)
        {
            trail.emitting = true;
        }
        else
        {
            trail.emitting = false;
            trail.Clear();
        }
        
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Flames") if (enemyHealth > 0)
        {
            enemyHealth -= 5f * Time.deltaTime;

            EnemySpawner.Instance.oofsound();

            StopCoroutine(flash());
            StartCoroutine(flash());
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Tree") if (enemyHealth > 0)
        {
                EnemySpawner.Instance.oofsound();
                enemyHealth = 0;
        }
    }
    //damage flash
    IEnumerator flash()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(1f);
        spriteRenderer.color = Color.red;

    }

    //chases player
    private void chase()
    {
        //transform.LookAt(player.transform.position);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, EnemySpawner.Instance.enemyspeed * Time.deltaTime);
    }
}
