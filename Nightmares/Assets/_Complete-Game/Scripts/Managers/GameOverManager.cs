using UnityEngine;
using UnityEngine.UI;

namespace CompleteProject
{
	public class GameOverManager : MonoBehaviour
	{
		public PlayerHealth playerHealth;               // Reference to the player's health.

		[SerializeField]
		Text killStatsText;                             // Reference to the Text component.
		[SerializeField]
		Text endGameScoreText;                          // Reference to the Text component.
		Animator anim;                                  // Reference to the animator component.


		void Awake()
		{
			// Set up the reference.
			anim = GetComponent<Animator>();
		}


		void Update()
		{
			// If the player has run out of health...
			if (playerHealth.currentHealth <= 0)
			{
				endGameScoreText.text = "Score: " + ScoreManager.score.ToString();
				killStatsText.text = ScoreManager.GetKillStatString();
				// ... tell the animator the game is over.
				anim.SetTrigger("GameOver");
			}
		}
	}
}