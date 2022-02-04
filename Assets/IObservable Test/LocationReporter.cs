using System;

using UnityEngine;

[Serializable]
public class LocationReporter : IObserver<Location>
{

	private IDisposable unsubscriber;
	
	[SerializeField]
	private string      instName;

	public LocationReporter(string name) => instName = name;

	public string Name => instName;


	public virtual void Subscribe(IObservable<Location> provider)
	{
		if (provider != null)
			unsubscriber = provider.Subscribe(this);
	}

	public virtual void OnCompleted()
	{
		Debug.Log($"The Location Tracker has completed transmitting data to {Name}.");
		Unsubscribe();
	}

	public virtual void OnError(Exception e) => Debug.Log($"{Name}: The location cannot be determined.");

	public virtual void OnNext(Location value) => Debug.Log($"{Name}: The current location is {value.Latitude}, {value.Longitude}");


	public virtual void Unsubscribe() => unsubscriber.Dispose();

}