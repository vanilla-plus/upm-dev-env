using System;

using UnityEngine;

namespace Vanilla.DataAssets.Three
{
	[CreateAssetMenu(fileName = "[String] ",
	                 menuName = "Vanilla/Data Assets/3/Ref/String")]
	[Serializable]
	public class StringAsset : RefAsset<string, StringSource>
	{

		[SerializeField]
		public string initializationValue;

		protected override void OnEnable()
		{
			base.OnEnable();

			source?.Set(initializationValue);
		}

	}

}