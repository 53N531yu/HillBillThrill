using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 10f;
    
    void Update()
    {
        if (health <= 0) Destroy(gameObject);
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Flames") if (health > 0) health -= 5f * Time.deltaTime;
    }
}
