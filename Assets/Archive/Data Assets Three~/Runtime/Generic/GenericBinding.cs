using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.TypeMenu;

namespace Vanilla.DataAssets.Three
{

	public abstract class GenericBinding<TType, TSource> : MonoBehaviour,
	                                                       IInitiable
		where TSource : GenericSource<TType>
	{

		// If we want to get really technical, it's unlikely that it will ever matter that GenericDrivers keep track of being initialized.
		// They're just a Mono-derived wrapper for GenericSource to receive Initialize, and those already track and guard their own Initialized state.
		// In that way, it's tempting to just make Initialized => true and call it a day here.

		public bool Initialized => true;

		[SerializeReference]
		[TypeMenu]
		public TSource source;


		protected virtual void OnValidate()
		{
			if (!Application.isPlaying) return;

			source.Validate();
		}


		public virtual async void Awake() => await source.Initialize();

		public virtual async UniTask Initialize() => await source.Initialize();


		public virtual async UniTask DeInitialize() => await source.DeInitialize();

//		public virtual async void OnDestroy() => await source.DeInitialize(); // Not needed?

	}

}