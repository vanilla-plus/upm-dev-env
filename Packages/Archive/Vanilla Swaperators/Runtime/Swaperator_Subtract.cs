using System;

using UnityEngine;

namespace Vanilla.Swaperators
{

	[Serializable]
	public struct Swaperator_Subtract : ISwaperator
	{

		public sbyte Apply(sbyte a,
		                   sbyte b) => (sbyte) (a - b);


		public byte Apply(byte a,
		                  byte b) => (byte) (a - b);


		public short Apply(short a,
		                   short b) => (short) (a - b);


		public ushort Apply(ushort a,
		                    ushort b) => (ushort) (a - b);


		public int Apply(int a,
		                 int b) => a - b;


		public uint Apply(uint a,
		                  uint b) => a - b;


		public long Apply(long a,
		                  long b) => a - b;


		public ulong Apply(ulong a,
		                   ulong b) => a - b;


		public float Apply(float a,
		                   float b) => a - b;


		public double Apply(double a,
		                    double b) => a - b;


		public Vector2 Apply(Vector2 a,
		                     Vector2 b) => a - b;


		public Vector3 Apply(Vector3 a,
		                     Vector3 b) => a - b;


		public Vector4 Apply(Vector4 a,
		                     Vector4 b) => a - b;

	}

}