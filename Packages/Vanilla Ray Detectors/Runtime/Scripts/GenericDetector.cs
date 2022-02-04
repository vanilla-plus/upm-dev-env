using System;

using UnityEngine;

namespace Vanilla.RayDetectors
{

	[Serializable]
	public class GenericDetector<TDetector, TDetectable> : GameObjectDetector, IDetector<TDetector, TDetectable>
		where TDetector : class, IDetector<TDetector, TDetectable>
		where TDetectable : Component, IDetectable<TDetector, TDetectable>
	{

		public enum GetComponentStyle
		{

			OnTarget,
			InChildren,
			InParents

		}

		public GetComponentStyle getComponentStyle = GetComponentStyle.OnTarget;

		public bool componentDetected = false;

		[SerializeField]
		private TDetectable _currentComponent;
		public TDetectable currentComponent
		{
			get => _currentComponent;
			set
			{
				if (ReferenceEquals(objA: _currentComponent,
				                    objB: value)) return;
				
				ComponentChangeDetected(outgoing: _currentComponent,
				                        incoming: value);

				_currentComponent = value;
			}
		}

		public Action<TDetectable, TDetectable> onComponentChangeDetected;


		protected override void ColliderChangeDetected(Collider outgoing,
		                                               Collider incoming)
		{
			base.ColliderChangeDetected(outgoing: outgoing,
			                            incoming: incoming);
			
			currentComponent = colliderDetected ? GetComponentDynamic(target: incoming) : null;
		}


		protected virtual void ComponentChangeDetected(TDetectable outgoing,
		                                               TDetectable incoming)
		{
			#if DEBUG_RAY_DETECTORS
			Debug.Log(message: $"GenericDetector component result changed from [{outgoing}] to [{incoming}]");
			#endif

			// If this is still true, it means outgoing isn't null yet.
			if (componentDetected) 
			{
				outgoing.OnDetectedEnd(rayDetector: this as TDetector);
			}

			componentDetected = !ReferenceEquals(objA: incoming,
			                                     objB: null);

			if (componentDetected)
			{
				incoming.OnDetectedBegin(rayDetector: this as TDetector);
			}

			onComponentChangeDetected?.Invoke(arg1: outgoing,
			                                  arg2: incoming);
		}
		
		protected TDetectable GetComponentDynamic(Collider target) =>
			getComponentStyle switch
			{
				GetComponentStyle.OnTarget   => target.GetComponent<TDetectable>(),
				GetComponentStyle.InChildren => target.GetComponentInChildren<TDetectable>(),
				GetComponentStyle.InParents  => target.GetComponentInParent<TDetectable>(),
				_                            => throw new ArgumentOutOfRangeException()
			};

	}

}