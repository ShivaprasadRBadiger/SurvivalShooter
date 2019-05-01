using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{

	public class MagazineFactory
	{
		private static MagazineFactory _Instance;
		public static MagazineFactory Instance
		{
			get
			{
				if (_Instance == null)
				{
					_Instance = new MagazineFactory();
				}
				return _Instance;
			}
		}
		private MagazineFactory() { }

		public IMagazine GetMagazine()
		{
			return new Magazine();
		}
	}
}

