using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;
using UnityEngine.SceneManagement;

namespace _Game._Scripts.Leaderboard
{
    public class Leaderboard : MonoBehaviour
    {
        [SerializeField] private List<TextMeshProUGUI> names;
        [SerializeField] private List<TextMeshProUGUI> times;
        
        [SerializeField] private List<TextMeshProUGUI> damageNames;
        [SerializeField] private List<TextMeshProUGUI> damageAmount;
        
        public string uName = "";
        public TMP_InputField nameField;

	    private string publicKey = "4bafe1da2bf204550f7bfeacb4d7755bdb8981761ef6b65e88f9ab326fe317f6";
	    private string damagePublicKey = "c55391251979109e5349ad84605ddcb41e2cd55506470e704cc92de6b17672f6";
        
	    // Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	    private void Start()
	    {
	    	
	    }

        public void GetLeaderboard()
        {
            LeaderboardCreator.GetLeaderboard(publicKey, ((msg) =>
            {
                for (int i = 0; i < names.Count; i++)
                {
                    names[i].text = msg[i].Username;
                    times[i].text = TimeString(msg[i].Score);
                }
            }));
            
            LeaderboardCreator.GetLeaderboard(damagePublicKey, ((msg) =>
            {
                for (int i = 0; i < damageNames.Count; i++)
                {
                    damageNames[i].text = msg[i].Username;
                    damageAmount[i].text = msg[i].Score.ToString();
                }
            }));
        }

	    public void SetLeaderboardEntry(int t, int d)
        {
	        LeaderboardCreator.UploadNewEntry(publicKey, PlayerPrefs.GetString("Name"), t, ((msg) =>
            {
                
            }));
            
            LeaderboardCreator.UploadNewEntry(damagePublicKey, PlayerPrefs.GetString("Name"), d, ((msg) =>
            {
                GetLeaderboard();
            }));
        }
        
        public void SetUname()
        {
            PlayerPrefs.SetString("Name", "");
            SceneManager.LoadScene("MainMenu");
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
    }
}