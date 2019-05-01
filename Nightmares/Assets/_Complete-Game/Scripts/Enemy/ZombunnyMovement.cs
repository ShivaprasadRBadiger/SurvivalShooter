using UnityEngine;
namespace CompleteProject
{
	public class ZombunnyMovement : EnemyMovement
	{
		[SerializeField]
		float strafSpeed;
		[SerializeField]
		float minDodgeDelay;
		[SerializeField]
		float maxDodgeDelay;

		private float timer;
		private float dodgeDelay;
		protected override void Update()
		{
			timer += Time.deltaTime;
			base.Update();

			Debug.Log(GetPlayerLookAngle());
			if (timer > dodgeDelay && isPlayerLookAtMe())
			{
				Dodge();
			}
		}

		private bool isPlayerLookAtMe()
		{
			return (170f < GetPlayerLookAngle() && GetPlayerLookAngle() < 180f);
		}

		private float GetPlayerLookAngle()
		{
			return Vector3.Angle(transform.forward, player.transform.forward);
		}

		public void Dodge()
		{
			timer = 0;
			nav.velocity += GetRandomDirection() * strafSpeed;
			dodgeDelay = GetRandomDodgeDelay();
		}

		private float GetRandomDodgeDelay()
		{
			return Random.Range(minDodgeDelay, maxDodgeDelay);
		}

		private Vector3 GetRandomDirection()
		{
			return Random.value > 0.5f ? transform.right : -transform.right;
		}
	}
}
