using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets.Sources
{

//	public interface IStringSource : IDataSource<string> { }

	public abstract class String_Source : IDataSource<string>
	{

		public abstract string Value
		{
			get;
			set;
		}

	}

	[Serializable]
	public class Direct_String_Source : IDataSource<string>
	{

		[SerializeField]
		public string _value;
		public string Value
		{
			get => _value;
			set => _value = value;
		}

	}

	[Serializable]
	public class Asset_String_Source : IDataSource<string>
	{

		[SerializeField]
		public StringAsset asset;
		public string Value
		{
			get => asset.Delta.Value;
			set => asset.Delta.Value = value;
		}

	}

	
	[Serializable]
	public class Delta_String_Source : IDataSource<string>
	{

		[SerializeField]
		public DeltaString Delta;
		public string Value
		{
			get => Delta.Value;
			set => Delta.Value = value;
		}

	}
	
}