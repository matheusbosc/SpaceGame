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
        private bool canCount = false;

        public string uName = "";
        
	    public TMP_InputField uNameField;

	    private TextMeshProUGUI timeText;
        
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
            //timeText = GameObject.FindGameObjectWithTag("TimeText").GetComponent<TextMeshProUGUI>();
            if (watch.IsRunning)
            {
	            watch.Restart();
            }
            else
            {
	            watch.Start();
            }
        }

        private void Update()
        {
	        if (canCount)
	        {
		        if (!timeText)
		        {
			        timeText = GameObject.FindGameObjectWithTag("TimeText").GetComponent<TextMeshProUGUI>();
		        }
		        timeText.text = TimeString((int)watch.Elapsed.TotalSeconds);
	        }
        }

        public void IncreaseDamage(int a)
        {
            damageTaken += a;
        }

        public void Win()
        {
	        watch.Stop();
	        canCount = false;
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
	    	yield return new WaitForSeconds(0.5f);
	    	GameObject.FindGameObjectWithTag("LeaderboardManager").GetComponent<_Game._Scripts.Leaderboard.Leaderboard>().SetLeaderboardEntry(timeInSeconds, damageTaken);
	    }
	    
	    private string TimeString(int t)
	    {
		    bool isSettingTime = true;
		    int seconds = 0;
		    int minutes = 0;
            
		    while (isSettingTime)
		    {
			    if (t >= 60)
			    {
				    minutes++;
				    t -= 60;
			    }
			    else
			    {
				    seconds = t;
				    isSettingTime = false;
			    }
		    }
            
		    return minutes.ToString("00") + "m " + seconds.ToString("00") + "s";
	    }
        
        // TODO: add tags, test
    }
}