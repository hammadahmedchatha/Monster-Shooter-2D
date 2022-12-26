using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    [SerializeField]
    private Text enemyKillCountTxt;

    private int enemyKillCount;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void EnemyKilled()
    {
        enemyKillCount++;
        // Debug.Log(enemyKillCount);
        enemyKillCountTxt.text = "Monster Killed : " + enemyKillCount;
    }

    public void RestartGame()
    {
        Invoke("Restart", 3f);
    }

    void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level3");
    }

    void NextLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level4");
    }
}
