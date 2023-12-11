using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health = 10f;
    public bool enemyHit = false;
    public EnemySpawner enemyDifficulty;

    public LayerMask enemyLayer;

    public Transform enemyCheck;
    
    public Image healthBar;
    public float damage;
    // Start is called before the first frame update
    [Header("Health Variables")]
    public bool canDamage = true;
    public bool isHit = false;
    public bool hasSparked = false;
    public float maxHealth;
    public float damage_reduction = 10f;
    public float totalDamage = 10f;
    public float invul_dur = 2f;
    public Vector2 offset;

    [Header("Other File Includes")]
    // public ParticleSystem spark;
    public CameraFollow cameraShake;
    public GameObject gameOver;
    public static event Action OnPlayerDamaged;

    void Update()
    {
        if (health <= 0)
        {
            Time.timeScale = 0f;
            gameOver.SetActive(true);
        }

        if (IsAttacked())
        {
            if (health > 0) health -= 2f * Time.deltaTime;
            healthBar.fillAmount = health / 20f;
        }
    }

    public void Restart()
    {
        gameOver.SetActive(false);
        health = 20f;
        healthBar.fillAmount = 10f;
        Time.timeScale = 1f;
        enemyDifficulty.difficulty = 1;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
            GameObject.Destroy(enemy);

        GameObject[] upgrades = GameObject.FindGameObjectsWithTag("Ecstasy");
        foreach(GameObject upgrade in upgrades)
            GameObject.Destroy(upgrade);

        enemyDifficulty.enemiesLeft = enemyDifficulty.difficulty;
        enemyDifficulty.createWave(enemyDifficulty.difficulty);
        enemyDifficulty.score = 0;
    }

    public IEnumerator Damaged()
    {
        canDamage = false;
        health -= damage;
        healthBar.fillAmount = health / 20f;
    
        yield return new WaitForSeconds(0.15f);
        canDamage = true;
    }

    public bool IsAttacked()
    {
        return Physics2D.OverlapCircle(enemyCheck.position, 0.2f, enemyLayer);
    }

    // public void OnTriggerStay2D(Collider2D col)
    // {
    //     if (col.tag == "Enemy") 
    //     {
    //         if (health > 0) health -= 2f * Time.deltaTime;
    //         healthBar.fillAmount = health / 20f;
    //     }
    // }

    public void TakeDamage(float amount){
        health -= amount;
        OnPlayerDamaged?.Invoke();
    }
}
