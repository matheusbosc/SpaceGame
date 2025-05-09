using System;
using TMPro;
using UnityEngine;

namespace _Game._Scripts
{
    public class MenuManager : MonoBehaviour
    {
        public Leaderboard.Leaderboard lB;
        public GameObject menu;
        private string uName;

        public TMP_InputField input;

        private void Start()
        {
            if (PlayerPrefs.GetString("Name") != null && PlayerPrefs.GetString("Name") != "")
            {
                gameObject.SetActive(false);
                menu.SetActive(true);
            }
        }

        public void SetName()
        {
            uName = input.text;
            PlayerPrefs.SetString("Name", uName);
        }
    }
}