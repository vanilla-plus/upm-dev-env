using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets.Sources
{

//	public interface IFloatSource : IDataSource<float> { }

	public abstract class Float_Source : IDataSource<float>
	{

		public abstract float Value
		{
			get;
			set;
		}

	}

	[Serializable]
	public class Direct_Float_Source : IDataSource<float>
	{

		[SerializeField]
		public float _Value;
		public float Value
		{
			get => _Value;
			set => _Value = value;
		}

	}

	[Serializable]
	public class Asset_Float_Source : IDataSource<float>
	{

		[SerializeField]
		public FloatAsset asset;

		public float Value
		{
			get => asset.Delta.Value;
			set => asset.Delta.Value = value;
		}

	}

	[Serializable]
	public class Delta_Float_Source : IDataSource<float>
	{

		[SerializeField]
		public DeltaFloat Delta = new();

		public float Value
		{
			get => Delta.Value;
			set => Delta.Value = value;
		}

	}

}