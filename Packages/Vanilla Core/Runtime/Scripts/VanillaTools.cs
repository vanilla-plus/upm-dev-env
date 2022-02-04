//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//
//using UnityEngine;
//using UnityEngine.Events;
//
//using Vanilla.Core;
//
//using static UnityEngine.Debug;
//
//using Debug = UnityEngine.Debug;
//using Object = System.Object;
//
////using UnityEngine.Experimental.PlayerLoop;
//#if UNITY_EDITOR
//using UnityEditor;
//#endif
//
//namespace Vanilla.Core
//{
//
//	// ------------------------------------------------------------------------------------------- Threshold Modules //
//	
//	/// <summary>
//	/// 	This class is used to compare any kind of struct data against incoming updates and
//	/// 	alert us only about significant changes. This can be greatly beneficial in situations
//	/// 	where an expensive operation needs to happen frequently but not necessarily constantly.
//	///
//	/// 	A great example is firing a raycast from the users mouse position. We could fire one
//	/// 	every frame but many more invocations will occur than we really need. We could put it
//	/// 	on a timer... but there may be incidental frames that miss their target as a result.
//	///
//	/// 	If we only fire the ray when the users input position has changed by some amount, we're
//	/// 	only really firing the ray when we need to which saves us a lot. But what if something
//	/// 	moves in front of the ray while the user doesn't update their pose? Hm...!
//	/// </summary>
//	/// <typeparam name="T"></typeparam>
//	[Serializable]
//	public abstract class BaseThresholdModule<T>
//		where T : struct
//	{
//		[SerializeField, ReadOnly]
//		protected T _delta;
//
//		/// <summary>
//		/// 	The current cached value. When this value is changed, its compared against its old
//		/// 	value and an event is invoked if the value difference is significant enough.
//		///
//		/// 	This is a great and easy optimization for ensuring things don't happen any more
//		/// 	frequently than they need to. Of course there's a small cost associated with comparing
//		/// 	in the first place... so always do your own benchmarks using a Stopwatch!
//		/// </summary>
//		public T delta
//		{
//			get => _delta;
//			set
//			{
//				if (!SignificantChange(value)) return;
//
//				_delta = value;
//
//				onThresholdBreach?.Invoke(_delta);
//			}
//		}
//		
//		/// <summary>
//		/// 	This event will only been invoked when a 'significant' difference is detected in the old and
//		/// 	new values.
//		/// </summary>
//		public UnityAction<T> onThresholdBreach;
//
//		public void Compare
//		(
//			T newValue)
//		{
//			delta = newValue;
//		}
//		
//		/// <summary>
//		/// 	This function internally checks for 'significant difference' between the delta and new values.
//		///
//		/// 	What constitutes 'significance' is highly contextually subjective, so it's left to be overridden.
//		/// </summary>
//		/// <param name="newValue"></param>
//		/// <returns></returns>
//		protected abstract bool SignificantChange(T newValue);
//
//	}
//	
//	/// <summary>
//	/// 	This class is used to compare any kind of struct data against incoming updates and
//	/// 	alert us only about significant changes. This can be greatly beneficial in situations
//	/// 	where an expensive operation needs to happen frequently but not necessarily constantly.
//	///
//	/// 	A great example is firing a raycast from the users mouse position. We could fire one
//	/// 	every frame but many more invocations will occur than we really need. We could put it
//	/// 	on a timer... but there may be incidental frames that miss their target as a result.
//	///
//	/// 	If we only fire the ray when the users input position has changed by some amount, we're
//	/// 	only really firing the ray when we need to which saves us a lot. But what if something
//	/// 	moves in front of the ray while the user doesn't update their pose? Hm...!
//	/// </summary>
//	/// <typeparam name="T"></typeparam>
//	[Serializable]
//	public abstract class BaseRecordableThresholdModule<T>
//		where T : struct
//	{
//		[SerializeField, ReadOnly]
//		protected T _delta;
//
//		/// <summary>
//		/// 	The current cached value. When this value is changed, its compared against its old
//		/// 	value and an event is invoked if the value difference is significant enough.
//		///
//		/// 	This is a great and easy optimization for ensuring things don't happen any more
//		/// 	frequently than they need to. Of course there's a small cost associated with comparing
//		/// 	in the first place... so always do your own benchmarks using a Stopwatch!
//		/// </summary>
//		public T delta
//		{
//			get => _delta;
//			set
//			{
//				if (allowMultipleBreachesPerFrame)
//				{
//					while (SignificantChange(value))
//					{
//						UpdateDelta(value);
//					}
//				}
//				else
//				{
//					if (SignificantChange(value))
//					{
//						UpdateDelta(value);
//					}
//				}
//			}
//		}
//		
//		/// <summary>
//		/// 	This event will only been invoked when a 'significant' difference is detected in the old and
//		/// 	new values.
//		/// </summary>
//		public UnityAction<T> onThresholdBreach;
//
//		/// <summary>
//		/// 	If true, delta will step towards new values (based on the threshold) rather than be set instantly.
//		///
//		/// 	This is useful if the intermediary values need reacting to as well as the target value itself.
//		/// </summary>
//		public bool clampDeltaUpdates = true;
//		
//		/// <summary>
//		/// 	If true, delta will be clamped by threshold each time it breaches. As a result, it will be
//		/// 	possible for onThresholdBreach to be invoked multiple times per frame. This is useful for higher
//		/// 	precision and recording of all the intermediary values delta moves through on its way to a new
//		/// 	value, although it is potentially more expensive to perform as a result.
//		/// </summary>
//		public bool allowMultipleBreachesPerFrame = true;
//		
//		public void Compare
//		(
//			T newValue)
//		{
//			delta = newValue;
//		}
//		
//		/// <summary>
//		/// 	This function internally checks for 'significant difference' between the delta and new values.
//		///
//		/// 	What constitutes 'significance' is highly contextually subjective, so it's left to be overridden.
//		/// </summary>
//		/// <param name="newValue"></param>
//		/// <returns></returns>
//		protected abstract bool SignificantChange(T newValue);
//		
//		protected abstract T StepTowardsNewValue(T newValue);
//
//		/// <summary>
//		/// 	This function quietly sets the backing field for delta without triggering any events.
//		///
//		/// 	This is useful if you need to turn the module on and off again without leaving an undesired
//		/// 	delta record trail.
//		/// </summary>
//		/// 
//		/// <param name="newValue">
//		///		The new value for _delta.
//		/// </param>
//		public void ResetDelta
//		(
//			T newValue)
//		{
//			_delta = newValue;
//		}
//		
//		private void UpdateDelta
//		(
//			T newValue)
//		{
//			_delta = clampDeltaUpdates ?
//				         StepTowardsNewValue(newValue) :
//				         newValue;
//
//			onThresholdBreach?.Invoke(_delta);
//		}
//	}
//
//	/// <summary>
//	/// 	When comparing positional vectors, a distance check can be executed quickly by comparing the square
//	/// 	magnitude of their difference. This works for both 2D and 3D vectors, so we share a base class
//	/// 	that allows us to reuse the overlapping functionality. 
//	/// </summary>
//	/// <typeparam name="T"></typeparam>
//	[Serializable]
//	public abstract class PositionVectorThresholdModule<T> : BaseRecordableThresholdModule<T>
//		where T : struct
//	{
//		public const float _worldSpaceThresholdMin = 0.001f;
//		public const float _worldSpaceThresholdMax = 10.0f;
//		
//		[SerializeField, ReadOnly]
//		protected float _sqrMagThreshold;
//
//		[SerializeField]
//		protected float _worldSpaceThreshold = 0.01f;
//		public float worldSpaceThreshold
//		{
//			get => _worldSpaceThreshold;
//			set
//			{
//				_worldSpaceThreshold = value.GetClamp(min: _worldSpaceThresholdMin, 
//				                                      max: _worldSpaceThresholdMax);
//
//				_sqrMagThreshold = _worldSpaceThreshold * _worldSpaceThreshold;
//			}
//		}
//		
//		public void Validate<B>(B behaviour) => worldSpaceThreshold = _worldSpaceThreshold;
//	}
//	
//	/// <summary>
//	/// 	When comparing directional vectors, it can be intuitive to think of the difference we want to
//	/// 	observe as measured in degrees. This works for both 2D and 3D directional vector comparisons,
//	/// 	so we reuse the overlap with this base class.
//	/// </summary>
//	/// <typeparam name="T">
//	///		A vector that can be treated as a direction.
//	/// </typeparam>
//	[Serializable]
//	public abstract class DirectionVectorThresholdModule<T> : BaseRecordableThresholdModule<T>
//		where T : struct
//	{
//		public const float _degreeThresholdMin = 0.01f;
//		public const float _degreeThresholdMax = 90.0f;
//		
//		[SerializeField]
//		protected float _thresholdInDegrees = 0.1f;
//		public float thresholdInDegrees
//		{
//			get => _thresholdInDegrees;
//			set => _thresholdInDegrees = value.GetClamp(min: _degreeThresholdMin,
//			                                            max: _degreeThresholdMax);
//		}
//		
//		public void Validate<B>(B behaviour) => thresholdInDegrees = _thresholdInDegrees;
//	}
//
//	/// <summary>
//	/// 	Bug : Recorded values aren't correctly normalized leading to inaccurate data. Unity
//	/// 	Bug : doesn't have a built-in Vector2.RotateTowards() so I have to make my own and I'm
//	/// 	Bug : very tired.
//	/// </summary>
//	[Serializable]
//	public class DirectionVector2DThresholdModule : DirectionVectorThresholdModule<Vector2>
//	{
//
//		protected override bool SignificantChange
//		(
//			Vector2 newValue)
//		{
//			return !_delta.IsAlignedWith(direction: newValue,
//			                             degrees: _thresholdInDegrees);
//		}
//
//		
//		protected override Vector2 StepTowardsNewValue
//		(
//			Vector2 newValue)
//		{
//			return _delta + ( newValue - _delta ).normalized * ( _thresholdInDegrees * Mathf.Deg2Rad );
//
////			return delta + (( newValue - _delta ).normalized * _thresholdInDegrees);
//
////			return delta.GetRotated(_thresholdInDegrees);
//
//
//
////			return Vector2.MoveTowards(current: _delta,
////			                           target: newValue,
////			                           maxDistanceDelta: _thresholdInDegrees * Mathf.Deg2Rad);
//
////			return Vector2.RotateTowards(current: _delta,
////			                             target: newValue,
////			                             maxRadiansDelta: _thresholdInDegrees * Mathf.Deg2Rad,
////			                             maxMagnitudeDelta: 1.0f);
//		}
//
//	}
//
//	[Serializable]
//	public class DirectionVector3DThresholdModule : DirectionVectorThresholdModule<Vector3>
//	{
//
//		protected override bool SignificantChange
//		(
//			Vector3 newValue)
//		{
//			return !_delta.IsAlignedWith(direction: newValue, 
//			                                degrees: _thresholdInDegrees);
//		}
//		
//		protected override Vector3 StepTowardsNewValue
//		(
//			Vector3 newValue)
//		{
//			return Vector3.RotateTowards(current: _delta,
//			                             target: newValue,
//			                             maxRadiansDelta: _thresholdInDegrees * Mathf.Deg2Rad,
//			                             maxMagnitudeDelta: 1.0f).normalized;
//		}
//	}
//
//		
//	/// <summary>
//	/// 	This module will take in a Vector2, treated as a position, and invoke the onThresholdBreach
//	/// 	event automatically when a change in distance greater than worldSpaceThreshold is detected.
//	/// </summary>
//	[Serializable]
//	public class Position2DThresholdModule : PositionVectorThresholdModule<Vector2>
//	{
//
//		protected override bool SignificantChange
//		(
//			Vector2 newValue)
//		{
//			return ( newValue - _delta ).sqrMagnitude > _sqrMagThreshold;
//		}
//
//		protected override Vector2 StepTowardsNewValue
//		(
//			Vector2 newValue)
//		{
//			return _delta + ( newValue - _delta ).normalized * _worldSpaceThreshold;
//		}
//	}
//	
//	/// <summary>
//	/// 	This module will take in a Vector3, treated as a position, and invoke the onThresholdBreach
//	/// 	event automatically when a change in distance greater than worldSpaceThreshold is detected.
//	/// </summary>
//	[Serializable]
//	public class Position3DThresholdModule : PositionVectorThresholdModule<Vector3>
//	{
//		protected override bool SignificantChange
//		(
//			Vector3 newValue)
//		{
//			return ( newValue - _delta ).sqrMagnitude > _sqrMagThreshold;
//		}
//
//		protected override Vector3 StepTowardsNewValue
//		(
//			Vector3 newValue)
//		{
//			return _delta + ( newValue - _delta ).normalized * _worldSpaceThreshold;
//		}
//	}
//
//	/// <summary>
//	///		This module is the exact same as Position3DThresholdModule except that each time the new value
//	/// 	breaches the delta, the delta is updated, radially clamped and the onNewDeltaRecord event
//	/// 	is invoked. This can happen multiple times a frame if the delta breach is significant enough.
//	///
//	/// 	For example, if thresholdInMetres was 1.0f and a new value 10.0f metres away from delta was
//	/// 	recorded, this event would be invoked 10 times, each time with delta a metre closer to the new
//	/// 	value. This is useful for systems that require extra accuracy like navigation, analytics,
//	/// 	replay systems, etc.
//	/// </summary>
// 	[Serializable]
//	public class RecordedPosition3DThresholdModule : Position3DThresholdModule
//	{
//
//		public Action<Vector3> onNewDeltaRecord;
//
//		protected override bool SignificantChange
//		(
//			Vector3 newPosition)
//		{
//			var breachFrame = false;
//
//			while (( newPosition - _delta ).sqrMagnitude > _sqrMagThreshold)
//			{
//				_delta += ( newPosition - _delta ).normalized * _worldSpaceThreshold;
//
//				onNewDeltaRecord?.Invoke(_delta); // We want to invoke this as many times as necessary.
//
//				breachFrame = true;
//			}
//
//			if (breachFrame) onThresholdBreach?.Invoke(newPosition); // We only want to invoke this once.
//
//			return breachFrame;
//		}
//
//	}
//
//	/// <summary>
//    ///     This module will invoke its onNewPosition event when a given position Vector3 is far enough away from a new value.
//    /// 	In other words... subscribe to onThresholdBreached, feed in new values in some kind of update loop and you'll only hear back when that position is further than
//    ///     the threshold in metres away! It's a great way of doing something using a transforms
//    ///     position without having to worry about it happening every single frame.
//    /// </summary>
//    [Serializable]
//	public class PositionComparisonModule
//	{
//
//		protected Vector3 _deltaPosition;
//
//		[SerializeField] [ReadOnly]
//		protected float _sqrMagThreshold;
//		[SerializeField]
//		[Range(min: 0.01f,
//			max: 10.0f)]
//		private float _thresholdInMetres = 0.05f;
//
//		public Action<Vector3> onNewPosition;
//
//		public float thresholdInMetres
//		{
//			get => _thresholdInMetres;
//			set
//			{
//				_thresholdInMetres = value;
//
//				_sqrMagThreshold = value * value;
//			}
//		}
//
//
//		public void Validate<T>(T behaviour) => thresholdInMetres = _thresholdInMetres;
//
//
//		public virtual bool Compare(Vector3 newPosition)
//		{
//			if (( newPosition - _deltaPosition ).sqrMagnitude < _sqrMagThreshold) return false;
//
//			_deltaPosition = newPosition;
//
//			onNewPosition?.Invoke(newPosition);
//
//			return true;
//		}
//
//	}
//
//    /// <summary>
//    ///     This module is the exact same as PositionDiffModule except that when the delta position is
//    ///     breached and recorded, the new value will be radially clamped to the last value. In other words,
//    ///     you always know that each breach position can only ever be exactly thresholdInMetres away from
//    ///     the last. This is useful for wayfinding like measuring and visualising navigation paths.
//    /// 
//    ///     This module is the exact same as PositionDiffModule except for that it takes the extra effort to
//    ///     invoke all the exact delta positions taken on the way to reach newPosition when breach occurs.
//    /// 
//    ///     This would be useful for wayfinding systems, like visualising a navigated path or recorded the
//    ///     exact distance a transform has travelled in metres.
//    /// </summary>
//    [Serializable]
//	public class PrecisePositionComparisonModule : PositionComparisonModule
//	{
//
//		public Action<Vector3> onNewDeltaRecord;
//
//
//		public override bool Compare(Vector3 newPosition)
//		{
//			var breachFrame = false;
//
//			while (( newPosition - _deltaPosition ).sqrMagnitude > _sqrMagThreshold)
//			{
//				_deltaPosition += ( newPosition - _deltaPosition ).normalized * thresholdInMetres;
//
//				onNewDeltaRecord?.Invoke(_deltaPosition); // We want to invoke this as many times as necessary.
//
//				breachFrame = true;
//			}
//
//			if (breachFrame) onNewPosition?.Invoke(newPosition); // We only want to invoke this once.
//
//			return breachFrame;
//		}
//
//	}
//    
//    // ----------------------------------------------------------------------------------------- Render Texture Slot //
//
//    /// <summary>
//    /// 	A class containing the basic necessities for creating and wielding a custom RenderTexture at run-time.
//    /// </summary>
//    [Serializable]
//    public class RenderTextureSlot
//    {
//
//	    [SerializeField, ReadOnly]
//	    private RenderTexture _texture;
//	    public RenderTexture texture
//	    {
//		    get
//		    {
//			    if (_texture != null) return _texture;
//
//			    _texture = new RenderTexture(width: pixelSize,
//			                                       height: pixelSize,
//			                                       depth: depth,
//			                                       format: format);
//
//			    if (!_texture.Create())
//			    {
//				    LogError("Creating RenderTexture failed.");
//			    }
//
//			    return _texture;
//		    }
//	    }
//
//	    [SerializeField]
//	    public int pixelSize = 1024;
//
//	    [SerializeField]
//	    public int depth = 24;
//
//	    [SerializeField]
//	    public RenderTextureFormat format;
//
//
//	    public RenderTextureSlot
//	    (
//		    int                 pixelSize = 1024,
//		    int                 depth     = 24,
//		    RenderTextureFormat format    = RenderTextureFormat.ARGB32)
//	    {
//		    this.pixelSize = pixelSize;
//		    this.depth = depth;
//		    this.format = format;
//	    }
//
//
//	    public void Validate<T>
//	    (
//		    T behaviour)
//	    {
//		    if (depth != 0  ||
//		        depth != 16 ||
//		        depth != 24)
//		    {
//			    // Run this weird obtuse snapping to the only valid values
//
//			    if (depth < 8)
//			    {
//				    depth = 0;
//			    }
//			    else if (depth < 20)
//			    {
//				    depth = 16;
//			    }
//			    else if (depth < 28)
//			    {
//				    depth = 24;
//			    }
//			    else
//			    {
//				    depth = 32;
//			    }
//		    }
//
//		    pixelSize.ToNearestPowerOfTwo();
//	    }
//
//	    public void Fill(Color color)
//	    {
//		    RenderTexture.active = texture;
//
//		    GL.Clear(clearDepth: true,
//		             clearColor: true,
//		             backgroundColor: color);
//
//		    RenderTexture.active = null;
//	    }
//    }
//   
//	// ----------------------------------------------------------------------------------------------- Material Slot //
//
//	// Allows for easy handling of Material instantiation or re-use
//
//	[Serializable]
//	public class VanillaMaterialSlot
//	{
//
//		[SerializeField] [ReadOnly]
//		private Material _materialInstance;
//
//		public bool     createInstance = true;
//		public Material sourceMaterial;
//		public Material materialInstance
//		{
//			get
//			{
//				if (_materialInstance != null) return _materialInstance;
//
//				if (createInstance)
//				{
//					_materialInstance = new Material(sourceMaterial);
//
//					onMaterialInstanceCreated?.Invoke(_materialInstance);
//				}
//				else
//				{
//					_materialInstance = sourceMaterial;
//				}
//				
//				return _materialInstance;
//			}
//		}
//
//		public UnityAction<Material> onMaterialInstanceCreated;
//
////		public override void Validate<V>(V behaviour) { }
//
////		public override void Reset()
////		{
////			_materialInstance = null;
////		}
//
//	}
//	
//    // ---------------------------------------------------------------------------------------------------- Ref Slot //
//
//    /// <summary>
//    ///     Only one 'T' fits at a time, where T is a reference type.
//    /// 
//    ///     We're able to listen for when an old T is outgoing or when a new T is incoming to the slot.
//    /// </summary>
//    [Serializable]
//    public class ReferenceSlot<T>
//		where T : class, new()
//    {
//
//	    [SerializeField]
//	    public T _current;
//	    public virtual T current
//	    {
//		    get => _current;
//		    set
//		    {
//			    if (ReferenceEquals(objA: _current,
//			                        objB: value))
//				    return;
//
//			    onSlotChange?.Invoke(arg0: _current,
//			                        arg1: value);
//
//			    _current = value;
//		    }
//	    }
//
//	    public UnityAction<T, T> onSlotChange;
//
////	    public override void Reset()
////	    {
////		    _current = default;
////
////		    onSlotChange.RemoveAllListeners();
////	    }
//
//    }
//
//    /// <summary>
//    /// 	
//    /// </summary>
//    /// 
//    /// <typeparam name="T">
//    ///		Some kind of value type.
//    /// </typeparam>
//    [Serializable]
//    public class ValueSlot<T>
//		where T : struct
//    {
//
//	    [SerializeField]
//	    public T _current;
//	    public virtual T current
//	    {
//		    get => _current;
//		    set
//		    {
//			    if (EqualityComparer<T>.Default.Equals(x: _current,
//			                                           y: value))
//				    return;
//
//			    onSlotChange?.Invoke(arg0: _current,
//			                        arg1: value);
//
//			    _current = value;
//		    }
//	    }
//		
//	    [SerializeField]
//	    public UnityAction<T, T> onSlotChange;
//
////	    public override void Reset()
////	    {
////		    _current = default;
////
////		    onSlotChange.RemoveAllListeners();
////	    }
//
//    }
//    
//	// ----------------------------------------------------------------------------------------------- HashSet Queue //
//
//	/// <summary>
//	/// 	This collection features both a HashSet and a Queue and uses one or the other depending
//	/// 	on the operation for best performance.
//	/// </summary>
//	[Serializable]
//	public class HashSetQueue<T> : IEnumerable<T>
//		where T : class
//	{
//		[SerializeField]
//		private HashSet<T> _hashSet = new HashSet<T>();
//
//		[SerializeField]
//		private Queue<T> _queue = new Queue<T>();
//
//		public int Count
//			=> _hashSet.Count;
//		
//		public bool Contains
//			(T item) =>
//				_hashSet.Contains(item);
//
//		public void Clear()
//		{
//			_hashSet.Clear();
//			_queue.Clear();
//		}
//
//		public bool Enqueue
//		(
//			T item)
//		{
//			if (!_hashSet.Add(item)) return false;
//
//			_queue.Enqueue(item);
//
//			return true;
//		}
//
//
//		public bool Remove
//		(
//			T item)
//		{
//			if (!Contains(item)) return false;
//
//			_hashSet.Remove(item);
//			_queue = new Queue<T>(_queue.Where(s => s != item));
//
//			return true;
//		}
//
//		public T Dequeue()
//		{
//			var item = _queue.Dequeue();
//
//			_hashSet.Remove(item);
//
//			return item;
//		}
//
//		public T Peek() => _queue.Peek();
//
//		public IEnumerator<T> GetEnumerator() => _queue.GetEnumerator();
//
//		IEnumerator IEnumerable.GetEnumerator() => _queue.GetEnumerator();
//
//	}
//	
//	// ----------------------------------------------------------------------------------------------- New Smoothies //
//
//	[Serializable]
//	public abstract class VanillaSmoothie<T>
//	{
//
//		public Queue<T> contents = new Queue<T>(capacity: 16);
//
//		[SerializeField]
//		private int _capacity = 16;
//		public int capacity
//		{
//			get => _capacity;
//			set
//			{
//				if (value <= 0) return;
//				
//				_capacity = value;
//				
//				contents = new Queue<T>(capacity: _capacity);
//			}
//		}
//		
//		void Add(T newItem)
//		{
//			while (contents.Count >= capacity) contents.Dequeue();
//			
//			contents.Enqueue(item: newItem);				
//		}
//
//		protected abstract T Blend();
//	}
//
//	[Serializable]
//	public class FloatSmoothie : VanillaSmoothie<float>
//	{
//
//		protected override float Blend() => contents.Average();
//
//	}
//	
//	[Serializable]
//	public class IntSmoothie : VanillaSmoothie<int>
//	{
//
//		protected override int Blend() => (int) contents.Average();
//
//	}
//	
//	[Serializable]
//	public class LongSmoothie : VanillaSmoothie<long>
//	{
//
//		protected override long Blend() => (long) contents.Average();
//
//	}
//	
//	[Serializable]
//	public class Vector2Smoothie : VanillaSmoothie<Vector2>
//	{
//
//		protected override Vector2 Blend() => contents.Average();
//
//	}
//
//	
//	[Serializable]
//	public class Vector3Smoothie : VanillaSmoothie<Vector3>
//	{
//
//		protected override Vector3 Blend() => contents.Average();
//
//	}
//
//	
//	// --------------------------------------------------------------------------------------------- Old Smoothies //
////	
////	[Serializable]
////	public abstract class VanillaSmoothie<T>
////		where T : struct
////	{
////
////		[SerializeField] [ReadOnly]
////		protected List<T> _contents = new List<T>();
////		[SerializeField] [ReadOnly]
////		public T output;
////
////		public VanillaSmoothie(int capacity) => _contents.Capacity = capacity;
////
////		public List<T> contents => _contents;
////
////
////		/// <summary>
////		///     Add some data for the smoothie.
////		/// </summary>
////		/// <param name="input"></param>
////		public void Add(T input) => contents.Cycle(input);
////
////
////        /// <summary>
////        ///     Averages the contents of all contained data and stores it in output.
////        /// </summary>
////        public abstract void Blend();
////
////
////        /// <summary>
////        ///     Adds some data, blends it, updates output and returns it immediately.
////        /// </summary>
////        /// <param name="input"></param>
////        /// <returns></returns>
////        public T Smooth(T input)
////        {
////	        Add(input: input);
////
////			Blend();
////
////			return output;
////		}
////
////	}
////
////	// ---------------------------------------------------------------------------------------------- Float Smoothie //
////
////	[Serializable]
////	public class FloatSmoothie : VanillaSmoothie<float>
////	{
////
////		public FloatSmoothie(int capacity) : base(capacity) { }
////
////
////		public override void Blend() => output = contents.Average();
////
////	}
////
////	// ------------------------------------------------------------------------------------------------ Int Smoothie //
////
////	[Serializable]
////	public class IntSmoothie : VanillaSmoothie<int>
////	{
////
////		public IntSmoothie(int capacity) : base(capacity) { }
////
////
////		public override void Blend() => output = contents.GetAverage();
////
////	}
////
////	// ------------------------------------------------------------------------------------------------ Int Smoothie //
////
////	[Serializable]
////	public class LongSmoothie : VanillaSmoothie<long>
////	{
////
////		public LongSmoothie(int capacity) : base(capacity) { }
////
////
////		public override void Blend() => output = contents.Sum() / contents.Count;
////
////	}
////
////	// -------------------------------------------------------------------------------------------- Vector2 Smoothie //
////
////	[Serializable]
////	public class Vector2Smoothie : VanillaSmoothie<Vector2>
////	{
////
////		public Vector2Smoothie(int capacity) : base(capacity) { }
////
////
////		public override void Blend() => output = contents.GetAverage();
////
////	}
////
////	// -------------------------------------------------------------------------------------------- Vector3 Smoothie //
////
////	[Serializable]
////	public class Vector3Smoothie : VanillaSmoothie<Vector3>
////	{
////
////		public Vector3Smoothie(int capacity) : base(capacity) { }
////
////		public override void Blend() => output = contents.GetAverage();
////
////	}
//
//	// ----------------------------------------------------------------------------------------- Quaternion Smoothie //
//
////	[Serializable]
////	public class QuaternionSmoothie : VanillaSmoothie<Quaternion>
////	{
////
////		[SerializeField]
////		public bool normalize;
////
////		public QuaternionSmoothie(int capacity) : base(capacity) { }
////
////		public override void Blend() => output = contents.GetAverage1();
////
////	}
//
//	// ------------------------------------------------------------------------------------------- Vanilla StopWatch //
//
////	[Serializable]
////	public class VanillaStopwatch
////	{
////
////		[SerializeField]
////		private bool _running;
////		[SerializeField] [ReadOnly]
////		public long milliseconds;
////		[SerializeField] [ReadOnly]
////		private LongSmoothie millisecondSmoothie = new LongSmoothie(32);
////
////		[SerializeField] [ReadOnly]
////		public long ticks;
////		[SerializeField] [ReadOnly]
////		private LongSmoothie tickSmoothie = new LongSmoothie(32);
////
////		[SerializeField]
////		public Stopwatch watch;
////
////		public bool running
////		{
////			get => _running;
////			set
////			{
////				_running = value;
////
////				if (_running)
////				{
////					if (watch == null) Start();
////				}
////				else
////				{
////					if (watch != null) Stop();
////				}
////			}
////		}
////
////
////		public void Start()
////		{
////			running = true;
////
////			watch = Stopwatch.StartNew();
////		}
////
////
////		public void Stop()
////		{
////			var t = watch.ElapsedTicks;
////			var m = watch.ElapsedMilliseconds;
////
////			watch.Stop();
////
////			ticks        = tickSmoothie.Smooth(t);
////			milliseconds = millisecondSmoothie.Smooth(m);
////
////			running = false;
////		}
////
////
////		public void Validate<T>(T behaviour)
////		{
////			if (Application.isPlaying) running = _running;
////
////			tickSmoothie.contents.Capacity        = 16;
////			millisecondSmoothie.contents.Capacity = 16;
////		}
////
////	}
//
//	// ----------------------------------------------------------------------------------------------------- Min/Max //
//
//	[Serializable]
//	public struct FloatRange
//	{
//		
//		[SerializeField]
//		private float _min;
//		public float min
//		{
//			get => _min;
//			set
//			{
//				if (value > _max)
//				{
//					#if DEBUG
//					Debug.LogWarning(culprit: "Range",
//					                message: "Min cannot be set to a value higher than max.\n" +
//					                         $"i.e. [{value}] is higher than [{_max}].");
//					#endif
//					
//					_min = _max;
//
//					return;
//				}
//
//				_min = value;
//			}
//		}
//
//		[SerializeField]
//		private float _max;
//		public float max
//		{
//			get => _max;
//			set
//			{
//				if (value < _min)
//				{
//					#if DEBUG
//					Debug.LogWarning(culprit: "Range",
//					                message: "Max cannot be set to a value lower than min.\n" +
//					                         $"i.e. [{value}] is lower than [{_min}].");
//					#endif
//
//					_max = _min;
//
//					return;
//				}
//
//				_max = value;
//			}
//		}
//
//
//		public FloatRange
//		(
//			float min,
//			float max)
//		{
//			_min = min;
//			_max = max;
//		}
//
//        /// <summary>
//        ///     Returns a float interpolated between min and max based on the given value.
//        /// </summary>
//        public float Interpolate(float value) => Mathf.Lerp(a: _min, 
//                                                            b: _max,
//                                                            t: value);
//
//        public float GetRandomValue() => UnityEngine.Random.Range(minInclusive: _min,
//                                                                  maxInclusive: _max);
//
//		public bool InclusivelyContains(float value) => value.IsWithinInclusiveRange(min: _min,
//		                                                                             max: _max);
//
//		public bool ExclusivelyContains(float value) => value.IsWithinExclusiveRange(min: _min,
//		                                                                             max: _max);
//
//	}
//
//	[Serializable]
//	public struct IntRange
//	{
//
//		[SerializeField]
//		private int _min;
//		public int min
//		{
//			get => _min;
//			set
//			{
//				if (value > _max)
//				{
//					#if DEBUG
//					Debug.LogWarning(culprit: "Range",
//					                message: "Min cannot be set to a value higher than max.\n" +
//					                         $"i.e. [{value}] is higher than [{_max}].");
//					#endif
//
//					_min = _max;
//
//					return;
//				}
//
//				_min = value;
//			}
//		}
//
//		[SerializeField]
//		private int _max;
//		public int max
//		{
//			get => _max;
//			set
//			{
//				if (value < _min)
//				{
//					#if DEBUG
//					Debug.LogWarning(message: "Max cannot be set to a value lower than min.\n" +
//					                          $"i.e. [{value}] is lower than [{_min}].");
//					#endif
//
//					_max = _min;
//
//					return;
//				}
//
//				_max = value;
//			}
//		}
//
//
//		public IntRange
//		(
//			int min,
//			int max)
//		{
//			_min = min;
//			_max = max;
//		}
//
//
//        /// <summary>
//        ///     Returns a float interpolated between min and max based on the given value.
//        /// </summary>
//        public int Lerp(float value) => (int) Mathf.Lerp(a: _min,
//                                                         b: _max,
//                                                         t: value);
//
//        public int Random() => Mathf.RoundToInt(f: UnityEngine.Random.Range(minInclusive: _min,
//                                                                            maxInclusive: _max));
//
//		public bool InclusivelyContains(int value) => value.IsWithinInclusiveRange(min: _min,
//		                                                                           max: _max);
//
//		public bool ExclusivelyContains(int value) => value.IsWithinExclusiveRange(min: _min,
//		                                                                           max: _max);
//
//	}
//
//	// -------------------------------------------------------------------------------------------------- Sin Module //
//
//	[Serializable]
//	public class VanillaSinModule
//	{
//
//		public float rate  = 1.0f;
//		public float scale = 1.0f;
//
//		[ReadOnly]
//		public float sin;
//		public float timeOffset;
//		public float valueOffset;
//
//		public float Update() => sin = ( valueOffset + Mathf.Sin(f: ( Time.time + timeOffset ) * rate) ) * scale;
//
//		public float Update(float time) => sin = ( valueOffset + Mathf.Sin(f: ( time + timeOffset ) * rate) ) * scale;
//
//	}
//
//}