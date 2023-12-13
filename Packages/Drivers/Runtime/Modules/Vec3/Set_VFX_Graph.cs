//using System;
//
//using UnityEngine;
//using UnityEngine.VFX;
//
//namespace Vanilla.Drivers.Snrubs.Vector3
//{
//
//	[Serializable]
//	public class Set_VFX_Graph : Vec3Snrub, 
//	                             IVFXGraphSnrub
//	{
//
//		[SerializeField]
//		private string propertyName = string.Empty;
//		public string PropertyName
//		{
//			get => propertyName;
//			set => propertyName = value;
//		}
//
//		[SerializeField]
//		private int propertyID = -1;
//		public int PropertyID
//		{
//			get => propertyID;
//			set => propertyID = value;
//		}
//
//		[SerializeField]
//		private VisualEffect[] graphs;
//		public VisualEffect[] Graphs
//		{
//			get => graphs;
//			set => graphs = value;
//		}
//
//
//		public override void OnValidate(UnityEngine.Vector3 value)
//		{
//			#if UNITY_EDITOR
//			PropertyID = Shader.PropertyToID(PropertyName);
//
//			// Is it safe to set VFXGraph values outside of Play Mode? Let's find out.
//
//			HandleValueChange(value);
//			#endif
//		}
//		
//		public override void Init(Vec1DriverSocket vec1DriverSocket) => PropertyID = Shader.PropertyToID(PropertyName);
//		
//		public override void HandleValueChange(UnityEngine.Vector3 value)
//		{
//			foreach (var g in Graphs)
//			{
//				if (g != null)
//					g.SetVector3(nameID: PropertyID,
//					             v: value);
//			}
//		}
//		
//	}
//
//}