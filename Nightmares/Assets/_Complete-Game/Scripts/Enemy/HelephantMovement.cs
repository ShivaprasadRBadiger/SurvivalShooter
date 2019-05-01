using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{
	public class HelephantMovement : EnemyMovement
	{
		[SerializeField]
		float enragingDistance;
		[SerializeField]
		float enragedSpeedMultiplier;

		[SerializeField]
		Renderer hellephantRenderer;

		[SerializeField]
		[ColorUsage(true, true)]
		Color normalColor, enragedColor;
		[SerializeField]
		private AudioClip enraged;

		private Animator anim;
		private AudioSource audioSource;
		private AudioClip prevClip;

		private bool isEnraged;


		protected override void Awake()
		{
			base.Awake();
			audioSource = GetComponent<AudioSource>();
			anim = GetComponent<Animator>();
		}
		void OnEnable()
		{
			hellephantRenderer.material.SetColor("_EmissionColor", normalColor);
		}
		protected override void Update()
		{
			base.Update();
			if (!isEnraged &&
					(player.position - transform.position).sqrMagnitude
					< enragingDistance * enragingDistance)
			{
				Enrage();
			}
		}

		private void Enrage()
		{
			isEnraged = true;
			anim.SetTrigger("Enrage");
			hellephantRenderer.material.SetColor("_EmissionColor", enragedColor);
			nav.speed = enragedSpeedMultiplier * nav.speed;
			nav.isStopped = true;
			prevClip = audioSource.clip;
			audioSource.clip = enraged;
			audioSource.Play();
			Invoke("OnEnrageComplete", 3);
		}

		public void OnEnrageComplete()
		{
			audioSource.clip = prevClip;
			anim.SetFloat("SpeedMultiplaier", enragedSpeedMultiplier);
			nav.isStopped = false;
		}

		private void OnDisable()
		{
			hellephantRenderer.material.SetColor("_EmissionColor", normalColor);
			nav.speed = nav.speed / enragedSpeedMultiplier;
			anim.SetFloat("SpeedMultiplaier", 1);
		}
	}
}

