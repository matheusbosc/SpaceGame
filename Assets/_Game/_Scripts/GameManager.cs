using _Game._Scripts.Enemy;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	
	public Health playerHealth;
	public PlayerController playerController;
	
	// UI
	public TextMeshProUGUI coinCounter;
	
	public TextMeshProUGUI healthText, maxHealthText;
	
	// Stats
	public int coins;

	public Color normalHealth, medHealth, badHealth;

	public bool isPaused = false;

	public WaveManager wM;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
	    maxHealthText.text = playerHealth.maxHealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
	    coinCounter.text = coins.ToString();
	    healthText.text = playerHealth.currentHealth.ToString();

	    if (playerHealth.currentHealth <= playerHealth.maxHealth * .5f && playerHealth.currentHealth > playerHealth.maxHealth * .2f)
	    {
		    healthText.color = medHealth;
	    } else if (playerHealth.currentHealth <= playerHealth.maxHealth * .2f)
	    {
		    healthText.color = badHealth;
	    }
	    else
	    {
		    healthText.color = normalHealth;
	    }
			
    }
    
	public void RefreshInfo()
	{
		maxHealthText.text = playerHealth.maxHealth.ToString();
	}

	public void AddCoins(int amount)
	{
		coins += amount;
	}

	public void LoseCoins(int amount)
	{
		coins -= amount;
	}
	
	public void RestartGame()
	{
		SceneManager.LoadScene("SampleScene");
	}
	
	public void MainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
	
	public void Win()
	{
		GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<_Game._Scripts.Leaderboard.Score>().SetScore();
	}

	void Pause()
	{
		if (!isPaused)
		{
			isPaused = true;
			wM.director.Pause();
			
		}
	}
}
