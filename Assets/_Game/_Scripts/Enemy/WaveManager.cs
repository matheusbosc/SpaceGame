using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game._Scripts.Enemy
{
    public class WaveManager : MonoBehaviour {
        public List<Level> levels;
        private int currentWave = 0;
        private int currentLevel = 0;
        private int aliveEnemies = 0;

	    private bool canSpawn = true, allHasSpawned = false;
	    
	    void Start() => StartWave();

        public void StartWave() {

            
            Wave wave = levels[currentLevel].waves[currentWave];
            
	        foreach (var group in wave.enemyGroups) {
            	
		        StartCoroutine(Spawn(2, group, group.countPerType - 1));
            	
		        /*
	            int i = 0;
	            
	            print ("Starting Loop");
                
	            while (!allHasSpawned)
	            {
	            	if (i < group.countPerType && canSpawn)
	            	{
	            		canSpawn = false;
		            	StartCoroutine(Spawn(2, group));
		            	
		            	GameObject enemy = Instantiate(group.enemyPrefabs[Random.Range(0, group.enemyPrefabs.Length)], group.path.waypoints[0], Quaternion.identity);
		            	enemy.GetComponent<EnemyMovement>().path = group.path;
		            	aliveEnemies++;
		            	canSpawn = true;
		            	
		            i++;
		            }
		        }*/
            }

            
        }

        public void EnemyDied() {
            aliveEnemies--;
            if (aliveEnemies <= 0) {
                currentWave++;
                if (currentWave < levels[currentLevel].waves.Count)
                {
                    StartCoroutine(NewWave(3));
                }
                else Debug.Log("Level Complete!");
            }
        }

        IEnumerator NewWave(float t)
        {
            yield return new WaitForSeconds(t);
            StartWave();
        }

        IEnumerator Spawn(float t, WaveData group, int amount)
        {
            
	        GameObject enemy = Instantiate(group.enemyPrefabs[Random.Range(0, group.enemyPrefabs.Length)], group.path.waypoints[0], Quaternion.identity);
	        enemy.transform.eulerAngles = new Vector3(0,180,0);
			enemy.GetComponent<EnemyMovement>().path = group.path;
			aliveEnemies++;

            yield return new WaitForSeconds(t);

            if (amount != 0){
                StartCoroutine(Spawn(2, group, amount - 1));
            }
        }
    }

}