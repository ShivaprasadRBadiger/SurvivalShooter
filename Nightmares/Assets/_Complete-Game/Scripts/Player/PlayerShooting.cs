using System;
using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace CompleteProject
{
	public class PlayerShooting : MonoBehaviour
	{
		public int damagePerShot = 20;                           // The damage inflicted by each bullet.
		public float timeBetweenBullets = 0.15f;                 // The time between each shot.
		public float range = 100f;                               // The distance the gun can fire.
		public float reloadTime = 3;                             //Time it takes to reload
		public Light faceLight;                                  // Reference to the light component of face.

		[SerializeField]
		private AudioClip gunShot;                               //Reference to gun shot audio clip
		[SerializeField]
		private AudioClip dryFire;                               //Reference to dry fire audio clip
		[SerializeField]
		private AudioClip reload;                                //Reference to reload audio clip

		public IMagazine currentMag { private set; get; }         // Magazine currently in use by player
		private MagazineFactory magazineFactory;                  //Factory class that generates new magazine

		private float timer;                                      // A timer to determine when to fire.
		private Ray shootRay = new Ray();                         // A ray from the gun end forwards.
		private RaycastHit shootHit;                              // A raycast hit to get information about what was hit.
		private int shootableMask;                                // A layer mask so the raycast only hits things on the shootable layer.
		private ParticleSystem gunParticles;                      // Reference to the particle system.
		private LineRenderer gunLine;                             // Reference to the line renderer.
		private AudioSource gunAudio;                             // Reference to the audio source.
		private Light gunLight;                                   // Reference to the light component.
		private float effectsDisplayTime = 0.2f;                  // The proportion of the timeBetweenBullets that the effects will display for.
		private bool isReloading = false;

		void Awake()
		{
			// Create a layer mask for the Shootable layer.
			shootableMask = LayerMask.GetMask("Shootable");
			magazineFactory = MagazineFactory.Instance;

			currentMag = magazineFactory.GetMagazine();
			// Set up the references.
			gunParticles = GetComponent<ParticleSystem>();
			gunLine = GetComponent<LineRenderer>();
			gunAudio = GetComponent<AudioSource>();
			gunLight = GetComponent<Light>();

			gunAudio.clip = gunShot;

			//faceLight = GetComponentInChildren<Light> ();
		}


		void Update()
		{
			// Add the time since Update was last called to the timer.
			timer += Time.deltaTime;

#if !MOBILE_INPUT
			// If the Fire1 button is being press and it's time to fire...
			if (Input.GetButton("Fire1")
				&& timer >= timeBetweenBullets
				&& Time.timeScale != 0
				&& !isReloading)
			{
				// ... shoot the gun.
				Shoot();
			}
			if (Input.GetButtonDown("Fire2") && Time.timeScale != 0 && !isReloading)
			{
				// ... shoot the gun.
				StartReload();
			}
			if (isReloading && timer > reloadTime)
			{
				FinalizeReload();
			}
#else
            // If there is input on the shoot direction stick and it's time to fire...
            if ((CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0) && timer >= timeBetweenBullets)
            {
                // ... shoot the gun
                Shoot();
            }
#endif
			// If the timer has exceeded the proportion of timeBetweenBullets that the effects should be displayed for...
			if (timer >= timeBetweenBullets * effectsDisplayTime)
			{
				// ... disable the effects.
				DisableEffects();
			}
		}

		private void FinalizeReload()
		{
			currentMag.Reload();
			gunAudio.clip = gunShot;
			isReloading = false;
		}

		private void StartReload()
		{
			isReloading = true;
			gunAudio.clip = reload;
			gunAudio.Play();
		}

		public void DisableEffects()
		{
			// Disable the line renderer and the light.
			gunLine.enabled = false;
			faceLight.enabled = false;
			gunLight.enabled = false;
		}


		void Shoot()
		{
			// Reset the timer.
			timer = 0f;

			// Play the gun shot audioclip.
			gunAudio.Play();

			if (!currentMag.CanFeed())
			{
				if (gunAudio.clip != dryFire)
					gunAudio.clip = dryFire;
				return;
			}

			// Enable the lights.
			gunLight.enabled = true;
			faceLight.enabled = true;

			// Stop the particles from playing if they were, then start the particles.
			gunParticles.Stop();
			gunParticles.Play();

			// Enable the line renderer and set it's first position to be the end of the gun.
			gunLine.enabled = true;
			gunLine.SetPosition(0, transform.position);

			// Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
			shootRay.origin = transform.position;
			shootRay.direction = transform.forward;

			// Perform the raycast against gameobjects on the shootable layer and if it hits something...
			if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
			{
				// Try and find an EnemyHealth script on the gameobject hit.
				EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

				// If the EnemyHealth component exist...
				if (enemyHealth != null)
				{
					// ... the enemy should take damage.
					enemyHealth.TakeDamage(damagePerShot, shootHit.point);
				}

				// Set the second position of the line renderer to the point the raycast hit.
				gunLine.SetPosition(1, shootHit.point);
			}
			// If the raycast didn't hit anything on the shootable layer...
			else
			{
				// ... set the second position of the line renderer to the fullest extent of the gun's range.
				gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
			}
		}


	}
}