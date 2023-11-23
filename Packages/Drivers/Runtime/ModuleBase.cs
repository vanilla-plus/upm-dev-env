using System;

using UnityEngine;

namespace Vanilla.Drivers.Modules
{

	[Serializable]
	public abstract class ModuleBase<T>
	{

		public abstract void OnValidate(T value);

		public abstract void HandleValueChange(T value);

	}

	[Serializable] public abstract class Vec1Module : ModuleBase<float> { }

	[Serializable] public abstract class Vec2Module : ModuleBase<UnityEngine.Vector2> { }

	[Serializable] public abstract class Vec3Module : ModuleBase<UnityEngine.Vector3> { }

	[Serializable] public abstract class ColorModule : ModuleBase<UnityEngine.Color> { }

}