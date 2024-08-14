using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public TextMeshProUGUI waveCountTxt;
    public float numWaves;
    public float spawnRates;
    public float timeBetweenWaves;

    public int enemyCount;
    public int waveCount = 1;

    public GameObject enemy;

    bool waveIsDone = true;

    private void Update()
    {
        waveCountTxt.text = "Wave: " + waveCount.ToString() + "/3";

        if(waveIsDone == true)
        {
            StartCoroutine(waveSpawner());
        }
        ResetWave();
    }
    IEnumerator waveSpawner()
    {
        if (waveCount < 4)
        {
            waveIsDone = false;
            for (int i = 0; i < enemyCount; i++)
            {
                GameObject enemySpawn = Instantiate(enemy);

                yield return new WaitForSeconds(timeBetweenWaves);
            }

            spawnRates -= 0.1f;
            enemyCount += 3;

            yield return new WaitForSeconds(timeBetweenWaves);
        }

    }

    private void ResetWave()
    {
        if (enemyCount == 0)
        {
            waveIsDone = true;
            waveCount += 1;
        }
    }


}
