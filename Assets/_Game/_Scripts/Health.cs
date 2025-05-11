using _Game._Scripts.Enemy;
using _Game._Scripts.Leaderboard;
using Unity.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
	public int maxHealth = 100;
	[ReadOnly] public int currentHealth;
	public bool isPlayer = false;
	public WaveManager wM;
	public GameManager gM;
	public Score sS;
	public ParticleSystem ps;
	public AudioSource dieAudio, takeDamageAudio;
	private bool isDead = false;
	
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	private void Start()
	{
		currentHealth = maxHealth;
		wM = FindFirstObjectByType<WaveManager>().GetComponent<WaveManager>();
		gM = FindFirstObjectByType<GameManager>().GetComponent<GameManager>();
		sS = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<_Game._Scripts.Leaderboard.Score>();
		//ps.Stop();
	}
	
	public void LoseHealth(int h, bool isEnemy)
	{
		takeDamageAudio.Play();
		currentHealth -= h;
		if (isEnemy) sS.IncreaseDamage(h);
		if (currentHealth <= 0 && !isDead)
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
		isDead = true;
		
		dieAudio.Play();
		if (!isPlayer)
		{
			
			wM.EnemyDied();
			ps.Play();
			ps.transform.SetParent(transform.parent);
			ps.gameObject.GetComponent<DestroyAfterTime>().DAT(1.5f);
			Destroy(gameObject);
			return;
		}
		
		wM.PlayerDied();
		
	}
}
