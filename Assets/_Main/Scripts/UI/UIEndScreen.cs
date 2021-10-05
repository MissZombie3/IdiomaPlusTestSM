using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEndScreen : MonoBehaviour
{
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject loseScreen;

    private List<Health> enemiesHealths = new List<Health>();  
    private Health playerHealth;
    private bool isEnded;

    private void Awake()
    {
        victoryScreen.SetActive(false);
        loseScreen.SetActive(false);

        foreach (EnemyAI enemy in FindObjectsOfType<EnemyAI>())
        {
            enemiesHealths.Add(enemy.GetComponent<Health>());
        }
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    private void Update()
    {
        if (isEnded)
            return;

        if (playerHealth.IsDead)
        {
            loseScreen.SetActive(true);
            isEnded = true;
        }

        int deadEnemies = 0;

        foreach (Health enemy in enemiesHealths)
        {
            if (enemy.IsDead)
                deadEnemies++;
        }

        if (deadEnemies == enemiesHealths.Count)
        {
            victoryScreen.SetActive(true);
            isEnded = true;
        }
    }
}
