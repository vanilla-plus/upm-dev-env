using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Vanilla.DataAssets;

using static UnityEngine.Debug;

public class DataTest : MonoBehaviour
{

	[Header(header: "Value Types")]

	[SerializeReference]
	public List<EventAsset> events;
	
	[SerializeReference]
	public List<BoolAsset> bools;

	[SerializeReference]
	public List<FloatAsset> floats;

	[SerializeReference]
	public List<IntAsset> ints;

	[SerializeReference]
	public List<StringAsset> strings;
	
	[SerializeReference]
	public List<Vector2Asset> Vector2s;
	
	[SerializeReference]
	public List<Vector3Asset> Vector3s;

	[Header(header: "Reference Types")]
	
	[SerializeReference]
	public List<GameObjectAsset> gameObjects;

	[SerializeReference]
	public List<MonoBehaviourAsset> monobehaviours;
	
	public void OnEnable()
	{
		foreach (var a in events) a.onBroadcast += EventOccurred;
		foreach (var a in bools) a.onBroadcast += BoolHandler;
		foreach (var a in floats) a.onBroadcast += FloatHandler;
		foreach (var a in ints) a.onBroadcast += IntHandler;
		foreach (var a in strings) a.onBroadcast += StringHandler;
		foreach (var a in Vector2s) a.onBroadcast += Vector2Handler;
		foreach (var a in Vector3s) a.onBroadcast += Vector3Handler;

		foreach (var a in gameObjects) a.onBroadcast += GameObjectHandler;
		foreach (var a in monobehaviours) a.onBroadcast += MonoBehaviourHandler;

		gameObjects[index: 0].value    = gameObject;
		monobehaviours[index: 0].value = this;
	}

	public void EventOccurred() => Log(message: "Some event occurred!");

	public void BoolHandler(bool value) => Log(message: $"[A {value.GetType().Name}] just changed to [{value.ToString()}]");

	public void FloatHandler(float value) => Log(message: $"[A {value.GetType().Name}] just changed to [{value.ToString()}]");

	public void IntHandler(int value) => Log(message: $"[A {value.GetType().Name}] just changed to [{value.ToString()}]");

	public void StringHandler(string value) => Log(message: $"[A {value.GetType().Name}] just changed to [{value}]");

	public void Vector2Handler(Vector2 value) => Log(message: $"[A {value.GetType().Name}] just changed to [{value.ToString()}]");

	public void Vector3Handler(Vector3 value) => Log(message: $"[A {value.GetType().Name}] just changed to [{value.ToString()}]");

	public void GameObjectHandler(GameObject value) => Log(message: $"[A {value.GetType().Name}] just changed to [{( value == null ? "null" : value.name )}]");

	public void MonoBehaviourHandler(MonoBehaviour value) => Log(message: $"[A {value.GetType().Name}] just changed to [{( value == null ? "null" : value.name )}]");

}
