using _Game._Scripts.Enemy;
using UnityEngine;

public class Health : MonoBehaviour
{
	public int maxHealth = 100;
	[HideInInspector] public int currentHealth;
	public bool isPlayer = false;
	public WaveManager wM;
	public GameManager gM;
	public ParticleSystem ps;
	public AudioSource dieAudio, takeDamageAudio;
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	private void Start()
	{
		currentHealth = maxHealth;
		wM = FindFirstObjectByType<WaveManager>().GetComponent<WaveManager>();
		gM = FindFirstObjectByType<GameManager>().GetComponent<GameManager>();
		//ps.Stop();
	}
	
	public void LoseHealth(int h)
	{
		takeDamageAudio.Play();
		currentHealth -= h;
		if (currentHealth <= 0)
		{
			Die();
			if (isPlayer)
			{
				gM.LoseCoins(4);
			}
		}
		
		//print(gameObject.name + ": " + currentHealth);
	}
	
	public void GainHealth(int h)
	{
		currentHealth += h;
		if (currentHealth > maxHealth)
		{
			currentHealth = maxHealth;
		}
		
		//print(gameObject.name + ": " + currentHealth);
	}
	
	public void Die()
	{
		//Code For Dying
		
		dieAudio.Play();
		if (!isPlayer)
		{
			wM.EnemyDied();
			ps.Play();
			ps.transform.SetParent(transform.parent);
			ps.gameObject.GetComponent<DestroyAfterTime>().DAT(3);
			Destroy(gameObject);
			return;
		}
		
		wM.PlayerDied();
		
	}
}
