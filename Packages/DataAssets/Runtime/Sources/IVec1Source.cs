using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets.Sources
{

//	public interface IVec1Source : IDataSource<float> { }

	public abstract class Vec1_Source : IDataSource<float>
	{

		public abstract float Value
		{
			get;
			set;
		}

	}

	[Serializable]
	public class Direct_Vec1_Source : IDataSource<float>
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
	public class Asset_Vec1_Source : IDataSource<float>
	{

		[SerializeField]
		public Vec1Asset asset;

		public float Value
		{
			get => asset.Delta.Value;
			set => asset.Delta.Value = value;
		}

	}

	[Serializable]
	public class Delta_Vec1_Source : IDataSource<float>
	{

		[SerializeField]
		public DeltaVec1 Delta = new();

		public float Value
		{
			get => Delta.Value;
			set => Delta.Value = value;
		}

	}

}