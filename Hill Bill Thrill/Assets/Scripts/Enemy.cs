using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 10f;
    public GameObject player;
    public GameObject explosion;
    public float speed;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        player = GameObject.Find("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            //EnemySpawner.Instance.enemiesLeft--;
            EnemySpawner.Instance.reduce();

            Instantiate(explosion, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        }
        
        chase();
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Flames") if (health > 0)
            {
                health -= 5f * Time.deltaTime;
                StopCoroutine(flash());
                StartCoroutine(flash());
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
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
