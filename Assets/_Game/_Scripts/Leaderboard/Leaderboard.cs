using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

namespace _Game._Scripts.Leaderboard
{
    public class Leaderboard : MonoBehaviour
    {
        [SerializeField] private List<TextMeshProUGUI> names;
        [SerializeField] private List<TextMeshProUGUI> times;
        
        public string uName = "";
        public TMP_InputField nameField;

        private string publicKey = "4bafe1da2bf204550f7bfeacb4d7755bdb8981761ef6b65e88f9ab326fe317f6";

        public void GetLeaderboard()
        {
            LeaderboardCreator.GetLeaderboard(publicKey, ((msg) =>
            {
                for (int i = 0; i < names.Count; i++)
                {
                    names[i].text = msg[i].Username;
                    times[i].text = msg[i].Score.ToString();
                }
            }));
        }

        public void SetLeaderboardEntry(int t)
        {
            LeaderboardCreator.UploadNewEntry(publicKey, uName, t, ((msg) =>
            {
                GetLeaderboard();
            }));
        }
        
        public void SetUname()
        {
            GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<_Game._Scripts.Leaderboard.Score>().uName =
                nameField.text;
        }
    }
}