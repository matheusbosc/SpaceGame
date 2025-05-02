using UnityEngine;

public class Health : MonoBehaviour
{
	public int maxHealth = 100;
	private int currentHealth;
	public bool isPlayer = false;
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	private void Start()
	{
		currentHealth = maxHealth;
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
		Destroy(gameObject);
	}
}
