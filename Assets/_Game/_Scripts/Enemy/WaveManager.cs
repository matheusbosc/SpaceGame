using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using Random = UnityEngine.Random;

namespace _Game._Scripts.Enemy
{
    public class WaveManager : MonoBehaviour {
        public List<Level> levels;
        private int currentWave = 0;
        private int currentLevel = 0;
        private int aliveEnemies = 0;
        
        public float splineSpeed = 0.06f;
        
        public PlayableDirector director;
        public double loopEndTime = 3.0; // Time in seconds to loop back to
        public double loopStartTime = 0.0;

	    public bool shouldLoop = true, loopSecond = true;

        public PlayerController player;
	    
	    void Start() => StartWave();

        public void StartWave() {

            
            Wave wave = levels[currentLevel].waves[currentWave];
            
	        foreach (var group in wave.enemyGroups) {
		        StartCoroutine(Spawn(2, group, group.countPerType - 1));
            }

            
        }

        private void Update()
        {
            if (shouldLoop && director.time >= loopEndTime)
            {
                director.time = loopStartTime;
                director.Evaluate(); // Force update to new time
            }
	        //print("a");
	        if (loopSecond && director.time >= 8.7)
            {
		        //director.SetTime(277);
		        director.time = 8.6;
		        print("time set to 7");
		        //director.Evaluate();
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
                else
                {
                    Debug.Log("Level Complete!");
                    shouldLoop = false;
                    director.Play();
                    currentLevel++;
                    currentWave = 0;
                    player.animator.enabled = true;
                }
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