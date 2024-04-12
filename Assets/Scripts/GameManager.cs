using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] EndGameScreen endGameScreen;

    public void ClearEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    public void StartGame()
    {
        endGameScreen.Hide();
        enemySpawner.StartSpawner();

        PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        playerController.ResetPlayer();
    }

    public void EndGame()
    {
        ClearEnemies();
        enemySpawner.StopSpawner();
        endGameScreen.Show();
    }
}
