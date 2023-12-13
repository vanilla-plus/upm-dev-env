using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets.Sources
{

//	public interface IIntSource : IDataSource<int> { }

	public abstract class Int_Source : IDataSource<int>
	{

		public int Value
		{
			get;
			set;
		}

	}

	[Serializable]
	public class Direct_Int_Source : IDataSource<int>
	{

		[SerializeField]
		public int _value;
		public int Value
		{
			get => _value;
			set => _value = value;
		}

	}

	[Serializable]
	public class Asset_Int_Source : IDataSource<int>
	{

		[SerializeField]
		public IntAsset asset;
		public int Value
		{
			get => asset.Delta.Value;
			set => asset.Delta.Value = value;
		}

	}

	[Serializable]
	public class Delta_Int_Source : IDataSource<int>
	{

		[SerializeField]
		public DeltaInt Delta = new();
		public int Value
		{
			get => Delta.Value;
			set => Delta.Value = value;
		}

	}

}