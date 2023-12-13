using System;

using UnityEngine;

using Vanilla.DeltaValues;

namespace Vanilla.DataAssets.Sources
{

//    public interface IBoolSource : IDataSource<bool> { }

    public abstract class Bool_Source : IDataSource<bool>
    {
        
        public abstract bool Value
        {
            get;
            set;
        }

        public void Toggle() => Value = !Value;

    }

    [Serializable]
    public class Direct_Bool_Source : Bool_Source
    {

        [SerializeField]
        public bool _Value;
        public override bool Value
        {
            get => _Value;
            set => _Value = value;
        }

    }

    [Serializable]
    public class Asset_Bool_Source : Bool_Source
    {

        [SerializeField]
        public BoolAsset asset;
        
        public override bool Value
        {
            get => asset.Delta.Value;
            set => asset.Delta.Value = value;
        }

    }

    [Serializable]
    public class Delta_Bool_Source : Bool_Source
    {

        [SerializeField]
        public DeltaBool Delta = new DeltaBool();

        public override bool Value
        {
            get => Delta;
            set => Delta.Value = value;
        }

    }

}