using _Game._Scripts.Enemy;
using UnityEngine;

public class Health : MonoBehaviour
{
	public int maxHealth = 100;
	[HideInInspector] public int currentHealth;
	public bool isPlayer = false;
	public WaveManager wM;
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	private void Start()
	{
		currentHealth = maxHealth;
		wM = FindFirstObjectByType<WaveManager>().GetComponent<WaveManager>();
	}
	
	public void LoseHealth(int h)
	{
		currentHealth -= h;
		if (currentHealth <= 0)
		{
			Die();
		}
		
		print(gameObject.name + ": " + currentHealth);
	}
	
	public void GainHealth(int h)
	{
		currentHealth += h;
		if (currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}
		
		print(gameObject.name + ": " + currentHealth);
	}
	
	public void Die()
	{
		//Code For Dying
		
		if (!isPlayer) wM.EnemyDied();
		
		Destroy(gameObject);
	}
}
