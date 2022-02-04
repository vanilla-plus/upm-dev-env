using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using JetBrains.Annotations;

using UnityEngine;

[Serializable]
public class Test : MonoBehaviour, INotifyPropertyChanged
{

	public float something;
	
	[SerializeField]
	public LocationReporter reporter;

	[SerializeField]
	public LocationTracker tracker;


	void Awake()
	{
		reporter = new LocationReporter("Download L.A.");

		tracker = new LocationTracker();
		
		reporter.Subscribe(tracker);

		PropertyChanged += (sender,
		                    args) => Debug.Log(args.PropertyName + " changed");
	}


	void Update()
	{
		tracker.TrackLocation(loc: new Location(latitude: Time.time  % 10,
		                                        longitude: Time.time % 12));

		something += Time.deltaTime;
	}


	public event PropertyChangedEventHandler PropertyChanged;


	[NotifyPropertyChangedInvocator]
	protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
	{
		PropertyChanged?.Invoke(this,
		                        new PropertyChangedEventArgs(propertyName));
	}

}
