using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
	public int damageAmount = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	// OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
	private void OnCollisionEnter(Collision collisionInfo)
	{
		if (collisionInfo.gameObject.CompareTag("Player") || collisionInfo.gameObject.CompareTag("Enemy"))
		{
			var health = collisionInfo.gameObject.GetComponent<Health>();
			
			health.LoseHealth(damageAmount);
			// Destroy VFX
			Destroy(gameObject);
		}
	}
}
