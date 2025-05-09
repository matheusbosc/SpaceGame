using System;
using System.Collections;
using TMPro;
using UnityEngine;
using System.Diagnostics;

namespace _Game._Scripts.Leaderboard
{
    public class Score : MonoBehaviour
    {
        private int damageTaken = 0;
        int timeInSeconds = 0;
        private bool canCount = true;

        public string uName = "";
        
	    public TMP_InputField uNameField;
        
	    Stopwatch watch;

        private void Start()
        {
	        DontDestroyOnLoad(this.gameObject);
	        
	        
	        watch = new Stopwatch();
        }

        public void ResetValues()
        {
            damageTaken = 0;
            timeInSeconds = 0;
            canCount = true;
	        watch.Start();
        }

        public void IncreaseDamage(int a)
        {
            damageTaken += a;
        }

        public void Win()
        {
	        watch.Stop();
	        TimeSpan ts = watch.Elapsed;
	        
	        timeInSeconds = (int)ts.TotalSeconds;
	        //Invoke("SetScore", 10);
        }

	    public void SetScore()
        {
	        StartCoroutine(setScoreA());
        }
        
	    private IEnumerator setScoreA()
	    {
	    	yield return new WaitForSeconds(1.5f);
	    	GameObject.FindGameObjectWithTag("LeaderboardManager").GetComponent<_Game._Scripts.Leaderboard.Leaderboard>().SetLeaderboardEntry(timeInSeconds, uName);
	    }
        
        // TODO: add tags, test
    }
}