using UnityEngine;
using System.Collections;

namespace CompleteProject
{
	public abstract class EnemyMovement : MonoBehaviour
	{
		protected Transform player;               // Reference to the player's position.
		protected PlayerHealth playerHealth;      // Reference to the player's health.
		protected EnemyHealth enemyHealth;        // Reference to this enemy's health.
		protected UnityEngine.AI.NavMeshAgent nav;               // Reference to the nav mesh agent.

		protected virtual void Awake()
		{
			// Set up the references.
			player = GameObject.FindGameObjectWithTag("Player").transform;
			playerHealth = player.GetComponent<PlayerHealth>();
			enemyHealth = GetComponent<EnemyHealth>();
			nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
		}


		protected virtual void Update()
		{
			// If the enemy and the player have health left...
			if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
			{
				// ... set the destination of the nav mesh agent to the player.
				nav.SetDestination(player.position);
			}
			// Otherwise...
			else
			{
				// ... disable the nav mesh agent.
				nav.enabled = false;
			}
		}
	}
}