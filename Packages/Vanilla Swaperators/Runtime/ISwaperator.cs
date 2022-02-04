using UnityEngine;

namespace Vanilla.Swaperators
{

	public interface ISwaperator
	{

		// C#


		sbyte Apply(sbyte a,
		            sbyte b);


		byte Apply(byte a,
		           byte b);


		short Apply(short a,
		            short b);


		ushort Apply(ushort a,
		             ushort b);


		int Apply(int a,
		          int b);


		uint Apply(uint a,
		           uint b);


		long Apply(long a,
		           long b);


		ulong Apply(ulong a,
		            ulong b);


		// Unity


		float Apply(float a,
		            float b);


		double Apply(double a,
		             double b);


		Vector2 Apply(Vector2 a,
		              Vector2 b);


		Vector3 Apply(Vector3 a,
		              Vector3 b);


		Vector4 Apply(Vector4 a,
		              Vector4 b);

	}

}