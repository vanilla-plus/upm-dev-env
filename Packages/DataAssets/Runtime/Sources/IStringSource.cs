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
	public class Direct_String_Source : String_Source
	{

		[SerializeField]
		public string _value;
		public override string Value
		{
			get => _value;
			set => _value = value;
		}


		public Direct_String_Source(string defaultValue) => Value = defaultValue;

	}

	[Serializable]
	public class Asset_String_Source : String_Source
	{

		[SerializeField]
		public StringAsset asset;
		public override string Value
		{
			get => asset.Delta.Value;
			set => asset.Delta.Value = value;
		}

	}

	
	[Serializable]
	public class Delta_String_Source : String_Source
	{

		[SerializeField]
		public DeltaString Delta;
		public override string Value
		{
			get => Delta.Value;
			set => Delta.Value = value;
		}

	}
	
}