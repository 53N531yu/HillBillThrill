using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public GameObject tree;
    public float rotateSpeed = 10f;
    public int treeActivated = 0;
    public bool treeEnabled = false;
    public float timer = 5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        tree.transform.Rotate(0, 0, treeActivated * rotateSpeed, Space.Self);
        if (!treeEnabled && timer < 10f) timer += Time.deltaTime;

        if (treeEnabled) TreeAttack();
    }
    public void TreeAttack()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            tree.SetActive(true);
            treeActivated = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (timer <= 0f)
        {
            Debug.Log("Hello???");
            treeEnabled = false;
            tree.SetActive(false);
            treeActivated = 0;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    // public IEnumerator TreeAttack()
    // {
    //     treeEnabled = true;
    //     tree.SetActive(true);
    //     treeActivated = 1;
    //     Cursor.lockState = CursorLockMode.Locked;
    //     yield return new WaitForSeconds(5f);
    //     Debug.Log("Hello???");
    //     treeEnabled = false;
    //     tree.SetActive(false);
    //     treeActivated = 0;
    //     Cursor.lockState = CursorLockMode.None;
    // }
}
