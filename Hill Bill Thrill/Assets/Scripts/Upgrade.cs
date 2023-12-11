using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public TreeScript tree;
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
            tree.treeEnabled = true;
            EnemySpawner.Instance.traildecaywrapper();
            Destroy(this.gameObject);
        }

    }
}
