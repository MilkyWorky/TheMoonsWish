using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WORLDDATA : MonoBehaviour
{

    //SIDE QUEST MAN
    [Header("Side Quest Man")]
    public bool isInteracting;
    public bool isGetQuest;
    public bool setEnemy;
    public bool isSideQuestComplete;
    public bool isZombie;
    public bool isMutant;
    public bool getRandom;
    public float enemyType;


    //PRIEST
    [Header("Priest")]
    public bool isInteractingPriest;
    public float playerLVL;
    public float getPlayerCurrentHP;
    public float playerDamageBonus;
    public float playerHealthBonus;
    public float lvlUPBonus;
    public bool playerRefreshHP;

    //Enemy
    [Header("EnemyDamageBonus")]
    public float enemyDamageBonus;
    public float enemyHPBonus;

    //Boss
    [Header("Boss")]
    public bool bossattack;

    [Header("GateKeep")]
    public float gateKeeperNum;



}
