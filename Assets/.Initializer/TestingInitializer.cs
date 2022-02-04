using System.Collections;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using UnityEngine;

using Vanilla.Initializer;

public class TestingInitializer : MonoBehaviour,
                                  IInitiable
{

	[SerializeField]
	private bool _initialized;
	public bool Initialized
	{
		get => _initialized;
		set => _initialized = value;
	}

	public  bool DeInitialized = false;

	public async UniTask Initialize()
	{
		Debug.Log($"I'm being initialized! Is the Initializer... uh... initializing? [{Initializer.initializationState}]");

		await UniTask.Delay(1000);
		
		Debug.Log("Let's rock!");

		Initialized = true;
		
		gameObject.SetActive(true);
	}


	public async UniTask DeInitialize()
	{
		Debug.Log($"I'm being de-initialized! Is the Initializer... uh... de-initializing? [{Initializer.initializationState}]");

		await UniTask.Delay(1000);
		
		Debug.Log("Goodnight everybody!");

		DeInitialized = true;
		
		gameObject.SetActive(false);
	}

}