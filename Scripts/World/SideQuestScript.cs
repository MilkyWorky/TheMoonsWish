using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SideQuestScript : MonoBehaviour
{
    public TextMeshProUGUI questTxt;
    public playerHP playerHP;
    public WORLDDATA worldData;
    public GameObject questCanvas;

    [Header("InvisibleWall")]
    public GameObject wall;

    public float totalEnemy;
    public float currentEnemyNum;






    // Start is called before the first frame update
    void Start()
    {
        worldData = GameObject.Find("WorldController").GetComponent<WORLDDATA>();
        playerHP = GameObject.Find("PlaYer").GetComponent<playerHP>();

    }

    // Update is called once per frame
    void Update()
    {
        if (worldData.setEnemy)
        {
            currentEnemyNum = totalEnemy;


        }
        ActivateKillZombies();
        EnemyKillQuest();

        if (!worldData.isSideQuestComplete)
        {
            worldData.setEnemy = false;
        }
        DisableWall();
    }

    private void EnemyKillQuest()
    {
        if (worldData.isZombie)
        {
            if (currentEnemyNum > 0)
            {
                worldData.isGetQuest = false;
                questTxt.text = "Kill " + currentEnemyNum + "/" + totalEnemy + " Zombies";
            }
            else if (currentEnemyNum == 0)
            {
                currentEnemyNum = -1;
                worldData.lvlUPBonus++;
                //Debug.Log("XXX");
                worldData.getRandom = true;
            }
            else
            {
                questTxt.text = "Quest Completed!";
                worldData.isSideQuestComplete = true;
            }
        }
        else if (worldData.isMutant)
        {
            if (currentEnemyNum > 0)
            {
                worldData.isGetQuest = false;
                questTxt.text = "Kill " + currentEnemyNum + "/" + totalEnemy + " Mutant";
            }
            else if (currentEnemyNum == 0)
            {
                currentEnemyNum = -1;
                worldData.lvlUPBonus++;
                //Debug.Log("XXX");
                worldData.getRandom = true;
            }
            else
            {
                questTxt.text = "Quest Completed!";
                worldData.isSideQuestComplete = true;
            }
        }


    }

    private void ActivateKillZombies()
    {
        if (worldData.isGetQuest)
        {
            worldData.setEnemy = true;
            worldData.isSideQuestComplete = false;
            questCanvas.SetActive(true);
        }
    }

    private void DisableWall()
    {
        if (worldData.gateKeeperNum == 0)
        {
            wall.SetActive(false);
        }
    }
}
