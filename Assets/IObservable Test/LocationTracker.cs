using System;
using System.Collections.Generic;

using UnityEngine;

[Serializable]
public class LocationTracker : IObservable<Location>
{

	[SerializeField]
	public LocationTracker() => observers = new List<IObserver<Location>>();

	[SerializeField]
	private List<IObserver<Location>> observers;


	public IDisposable Subscribe(IObserver<Location> observer)
	{
		if (!observers.Contains(observer)) observers.Add(observer);

		return new Unsubscriber(observers: observers,
		                        observer: observer);
	}


	[Serializable]
	public class Unsubscriber : IDisposable
	{

		private List<IObserver<Location>> _observers;

		private IObserver<Location>       _observer;


		public Unsubscriber(List<IObserver<Location>> observers,
		                    IObserver<Location> observer)
		{
			_observers = observers;
			_observer  = observer;
		}


		public void Dispose()
		{
			if (_observer != null &&
			    _observers.Contains(_observer)) _observers.Remove(_observer);
		}

	}


	public void TrackLocation(Location? loc)
	{
		foreach (var observer in observers)
		{
			if (!loc.HasValue) 
				observer.OnError(new LocationUnknownException());
			else
				observer.OnNext(loc.Value);
		}
	}


	public void EndTransmission()
	{
		foreach (var observer in observers.ToArray())
			if (observers.Contains(observer))
				observer.OnCompleted();

		observers.Clear();
	}

}

[Serializable]
public class LocationUnknownException : Exception
{

	internal LocationUnknownException() { }

}