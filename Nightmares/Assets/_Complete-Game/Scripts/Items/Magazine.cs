using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CompleteProject
{
	public class Magazine : IMagazine
	{
		public int Capacity { private set; get; }
		public int AmmoCount { private set; get; }

		private Action<IMagazine> observers;

		public Magazine(int capacity = 30)
		{
			Capacity = capacity;
			AmmoCount = Capacity;
		}

		public bool CanFeed()
		{
			if (AmmoCount - 1 >= 0)
			{
				AmmoCount--;
				Notify();
				return true;
			}
			else
			{
				return false;
			}
		}

		public void Reload()
		{
			AmmoCount = Capacity;
			Notify();
		}


		public void Add(IObserver<IMagazine> observer)
		{
			observers += observer.Update;
		}

		public void Remove(IObserver<IMagazine> observer)
		{
			observers -= observer.Update;
		}

		public void Notify()
		{
			if (observers != null)
			{
				observers.Invoke(this);
			}
		}
	}
}
