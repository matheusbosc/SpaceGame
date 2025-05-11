using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
	public int damageAmount = 2;

	public bool isEnemyBullet = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	// OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
	private void OnTriggerEnter(Collider other)
	{
		if ((isEnemyBullet ? other.gameObject.CompareTag("Player") : other.gameObject.CompareTag("Enemy")))
		{
			var health = other.gameObject.GetComponent<Health>();
			
			health.LoseHealth(damageAmount, isEnemyBullet);
			// Destroy VFX
			Destroy(gameObject);
		}
	}
}
