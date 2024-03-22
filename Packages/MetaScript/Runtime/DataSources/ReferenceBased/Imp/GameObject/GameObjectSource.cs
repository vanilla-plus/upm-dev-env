using System;

namespace Vanilla.MetaScript.DataSources.GameObject
{

//	[Serializable]
	public interface IGameObjectSource : IRefSource<UnityEngine.GameObject, IGameObjectSource>
	{


//		public abstract UnityEngine.GameObject Value
//		{
//			get;
//			set;
//		}
//
//		[NonSerialized]
//		private Action<UnityEngine.GameObject> _onSet;
//		public Action<UnityEngine.GameObject> OnSet
//		{
//			get => _onSet;
//			set => _onSet = value;
//		}
//
//		[NonSerialized]
//		private Action<UnityEngine.GameObject, UnityEngine.GameObject> _onSetWithHistory;
//		public Action<UnityEngine.GameObject, UnityEngine.GameObject> OnSetWithHistory
//		{
//			get => _onSetWithHistory;
//			set => _onSetWithHistory = value;
//		}
//
//		public abstract void OnBeforeSerialize();
//
//		public abstract void OnAfterDeserialize();
//
//
//		public static implicit operator UnityEngine.GameObject(GameObjectSource input) => input.Value;

	}

}