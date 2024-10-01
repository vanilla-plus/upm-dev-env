using UnityEngine;

public abstract class AnimationArray : MonoBehaviour
{

	[SerializeField]
	protected Transform[] transforms; // Array of transforms to animate

	[SerializeField]
	public float offsetPerElement = 0.0675f;

	[SerializeField]
	protected float timePerElement = 1.0f; // Time allocated to each transform
	
	[SerializeField]
	public float preTime = 1.0f;

	[SerializeField]
	public float postTime = 1.0f;

	[SerializeField]
	public float time = 0.0f;

	[SerializeField]
	public float totalElementTime = 0.0f;

	[SerializeField]
	public float speed = 1.0f;


	private void OnValidate()
	{
		#if debug
		UpdateTotalElementTime();
		#endif
	}


	public void UpdateTotalElementTime() => totalElementTime = (transforms.Length - 1) * offsetPerElement + timePerElement;


	protected virtual void Update()
	{
		time = Mathf.Repeat(t: time         + Time.deltaTime * speed,
		                    length: preTime + totalElementTime + postTime);

		var innerElementTime = time - preTime;

		for (var i = 0;
		     i < transforms.Length;
		     i++)
		{
			var elementStartTime = i * offsetPerElement;
			var elementEndTime   = elementStartTime + timePerElement;

			var normalizedElementTime = Mathf.Clamp01((innerElementTime - elementStartTime) / (elementEndTime - elementStartTime));

			AnimateTransform(transform: transforms[i],
			                 timeSlice: normalizedElementTime);
		}
	}


	protected abstract void AnimateTransform(Transform transform,
	                                         float timeSlice);

}