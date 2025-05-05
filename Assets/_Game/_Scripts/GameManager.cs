using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
	
	public Health playerHealth;
	public PlayerController playerController;
	
	// UI
	public TextMeshProUGUI coinCounter;
	
	public TextMeshProUGUI healthText, maxHealthText;
	
	// Stats
	public int coins;
	
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
    }
    
	void RefreshInfo()
	{
		maxHealthText.text = playerHealth.maxHealth.ToString();
	}
}
