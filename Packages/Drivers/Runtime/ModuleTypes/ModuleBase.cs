using System;

namespace Vanilla.Drivers.Snrubs
{

	[Serializable]
	public abstract class SnrubBase<T>
	{

		public abstract void OnValidate(T value);

		public abstract void Init(DriverSocket<T> socket);

		public abstract void DeInit(DriverSocket<T> socket);

		public abstract void HandleValueChange(T value);
		
	}

	[Serializable] public abstract class Vec1Snrub : SnrubBase<float> { }

	[Serializable] public abstract class Vec2Snrub : SnrubBase<UnityEngine.Vector2> { }

	[Serializable] public abstract class Vec3Snrub : SnrubBase<UnityEngine.Vector3> { }

	[Serializable] public abstract class Vec4Snrub : SnrubBase<UnityEngine.Vector4> { }

	[Serializable] public abstract class ColorSnrub : SnrubBase<UnityEngine.Color> { }

}