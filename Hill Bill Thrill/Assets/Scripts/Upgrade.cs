using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
            {

            Debug.Log("noob");
            //StartCoroutine(EnemySpawner.Instance.traildecay());
            EnemySpawner.Instance.traildecaywrapper();
            Destroy(this.gameObject);
        }

    }
}
