using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CompleteProject
{
	public class AmmoHUD : MonoBehaviour, IObserver<IMagazine>
	{
		private const string DISPLAY_FORMAT = "Ammo: {0}/{1}";

		[SerializeField]
		private Text ammoText;            //Reference to text that displayed ammo state.
		[SerializeField]
		private PlayerShooting playerGun; //Reference to magazine currently used by player
		private IMagazine playerMagazine;

		private void OnEnable()
		{
			playerMagazine = playerGun.currentMag;
			playerMagazine.Add(this);
		}
		void IObserver<IMagazine>.Update(IMagazine data)
		{
			ammoText.text = string.Format(DISPLAY_FORMAT, data.AmmoCount, data.Capacity);
		}
		private void OnDisable()
		{
			playerMagazine.Remove(this);
		}
	}

}
