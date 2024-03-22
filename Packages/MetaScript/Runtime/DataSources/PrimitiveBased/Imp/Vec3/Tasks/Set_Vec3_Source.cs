using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.DataAssets
{
    
	[Serializable]
	public class Set_Vec3_Source : Set_Source<Vector3, Vec3Source, Vec3Asset, AssetVec3Source> { }

}