using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Game._Scripts.Enemy
{
    public class WaveManager : MonoBehaviour {
        public List<Level> levels;
        private int currentWave = 0;
        private int currentLevel = 0;
        private int aliveEnemies = 0;

        public Vector3 basePlayerPos = new Vector3(0, 1.36999989f, -4.05999947f);
        
        public float lerpSpeed = 5f;
        
        public PlayableDirector director;
        public double loopEndTime = 3.0; // Time in seconds to loop back to
        public double loopStartTime = 0.0;

	    private bool shouldLoop = true, loopSecond = true, newLevel = false, _lerpingToBasePos = false;

        public PlayerController player;

        public int bulletDamage = 2;
        public int levelsSinceLastChange = 0;

        public GameManager gM;
        //public Animator playerAnim;
	    
	    void Start() => StartWave();

        public void StartWave() {

            
            Wave wave = levels[currentLevel].waves[currentWave];
            
	        foreach (var group in wave.enemyGroups) {
		        StartCoroutine(Spawn(2, group, group.countPerType - 1));
            }

            
        }

        public void NewLevel()
        {
            loopSecond = false;
            newLevel = true;
        }

        private void Update()
        {
            if (shouldLoop && director.time >= loopEndTime)
            {
                director.time = loopStartTime;
                director.Evaluate(); // Force update to new time
            }
	        //print("a");
	        if (director.time >= 10.1 && loopSecond)
            {
		        director.time = 10;
		        print("time set to 10");
            }

            if (newLevel && director.time >= 720 / 60)
            {
                newLevel = false;
                shouldLoop = true;
                director.time = 0;
                StartWave();
                player.canInteract = true;
                player.animator.enabled = false;
            }

            if (_lerpingToBasePos)
            {
                player.transform.position = Vector3.Lerp(player.transform.position, basePlayerPos, lerpSpeed * Time.deltaTime);

                if (Vector3.Distance(player.transform.position, basePlayerPos) < 0.1f)
                {
                    shouldLoop = false;
                    _lerpingToBasePos = false;
                    player.animator.enabled = true;
                }
            }
        }

        public void EnemyDied() {
            aliveEnemies--;
            gM.AddCoins(5);
            if (aliveEnemies <= 0) {
                currentWave++;
                if (currentWave < levels[currentLevel].waves.Count)
                {
                    StartCoroutine(NewWave(3));
                    gM.AddCoins(8);
                }
                else
                {
                    Debug.Log("Level Complete!");
                    director.Play();
                    currentLevel++;
                    currentWave = 0;
                    loopSecond = true;
                    player.canInteract = false;
                    gM.AddCoins(10);

                    levelsSinceLastChange += 1;
                    if (levelsSinceLastChange == 1)
                    {
                        bulletDamage += 1;
                    }
                    
                    _lerpingToBasePos = true;
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
			enemy.GetComponent<EnemyMovement>().wM = this;
			aliveEnemies++;

            yield return new WaitForSeconds(t);

            if (amount != 0){
                StartCoroutine(Spawn(2, group, amount - 1));
            }
        }
    }

}