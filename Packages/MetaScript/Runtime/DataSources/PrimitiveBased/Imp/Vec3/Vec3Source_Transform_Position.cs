using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources;

[Serializable]
public class Vec3Source_Transform_Position : Vec3Source
{

	[SerializeField] public Transform target;
	
	public override void OnBeforeSerialize() { }

	public override void OnAfterDeserialize() { }
	
	public override Vector3 Value
	{
		get => target.position;
		set => target.position = value;
	}

}
