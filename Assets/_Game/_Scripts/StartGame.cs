using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game._Scripts
{
    public class StartGame : MonoBehaviour
    {
        public void GameStart()
        {
            SceneManager.LoadScene("SampleScene");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}