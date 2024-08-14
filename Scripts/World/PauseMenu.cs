using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public WORLDDATA worldData;
    public GameObject pauseMenu;
    [SerializeField]
    private bool isPausing;
    private bool cheatSwitch;
    private bool cheatSwitchEnemy;

    public EnemyHP[] enemyHP;
    public float[] enemyHPTemp;

    //private void Awake()
    //{
    //    pauseMenu.SetActive(false);
    //}

    // Start is called before the first frame update
    void Start()
    {
        isPausing = false;
        worldData = GameObject.Find("WorldController").GetComponent<WORLDDATA>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(WaitTime());

    }

    IEnumerator WaitTime()
    {
        if (!isPausing)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                pauseMenu.SetActive(true);
                Cursor.visible = true;
                yield return new WaitForSeconds(0);
                isPausing = true;
                Time.timeScale = 0;
            }

        }

        if (isPausing)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                pauseMenu.SetActive(false);
                Cursor.visible = false;
                yield return new WaitForSeconds(0);
                isPausing = false;
                Time.timeScale = 1;
            }
        }
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void HPCheat()
    {
        if (!cheatSwitch)
        {
            worldData.playerRefreshHP = true;
            worldData.playerHealthBonus = 9999;
            cheatSwitch = true;
        }
        else if (cheatSwitch)
        {
            cheatSwitch = false;
            worldData.playerRefreshHP = true;
            worldData.playerHealthBonus = -9999;

        }

    }
    public void EnemyHPCheat()
    {
        if (!cheatSwitchEnemy)
        {
            enemyHPTemp[0] = 100;
            enemyHP[0].currentHP = 2;
            enemyHPTemp[1] = 50;
            enemyHP[1].currentHP = 2;
            enemyHPTemp[2] = 50;
            enemyHP[2].currentHP = 2;
            cheatSwitchEnemy = true;
        }
        else if (cheatSwitchEnemy)
        {
            cheatSwitchEnemy = false;
            enemyHP[0].currentHP = enemyHPTemp[0];
            enemyHP[1].currentHP = enemyHPTemp[1];
            enemyHP[2].currentHP = enemyHPTemp[2];
            

        }

    }



}
