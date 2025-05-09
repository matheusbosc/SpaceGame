using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace _Game._Scripts.Leaderboard
{
    public class Score : MonoBehaviour
    {
        private int damageTaken = 0;
        int timeInSeconds = 0;
        private bool canCount = true;

        public string uName = "";
        
        public TMP_InputField uNameField;

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public void ResetValues()
        {
            damageTaken = 0;
            timeInSeconds = 0;
            canCount = true;
            StartCoroutine(Count());
        }

        public void IncreaseDamage(int a)
        {
            damageTaken += a;
        }

        public void Win()
        {
            canCount = false;
            Invoke("SetScore", 10);
        }

        private void SetScore()
        {
            GameObject.FindGameObjectWithTag("LeaderboardManager").GetComponent<_Game._Scripts.Leaderboard.Leaderboard>().SetLeaderboardEntry(timeInSeconds);
        }

        private IEnumerator Count()
        {
            yield return new WaitForSeconds(1);
            if (canCount) timeInSeconds++;
        }
    }
}