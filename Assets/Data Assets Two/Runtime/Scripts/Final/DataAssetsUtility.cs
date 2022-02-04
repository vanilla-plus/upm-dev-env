using System;
using System.Collections.Generic;

using UnityEngine;

using static UnityEngine.Debug;

namespace Vanilla.DataAssets
{

	public static class DataAssetsUtility
	{

		public const string Unknown = "?";

		public const string Component_X = "_z";
		public const string Component_Y = "_y";
		public const string Component_Z = "_z";


		public static void LogSubscriptions(string name,
		                                    IReadOnlyList<Delegate> currentInvocations,
		                                    IReadOnlyList<Delegate> newInvocations)
		{
			bool newSubscription;

			Delegate action;

			// _onBroadcast always starts as null, which would mean we can't count it without an error.
			// If its null, we must be adding our first handler.
			if (currentInvocations == null)
			{
				newSubscription = true;

				action = newInvocations[index: newInvocations.Count - 1];
			}

			// If the newState is null... I have no idea? Does that happen in an unsubscription?
			else if (newInvocations == null)
			{
				newSubscription = false;

				action = currentInvocations[index: currentInvocations.Count - 1];
			}

			// Otherwise...
			else
			{
				// Count the amount of invocations on the outgoing list and the incoming list
				var oldDelCount = currentInvocations.Count;
				var newDelCount = newInvocations.Count;

				// If the incoming list is longer, it means this is a new event subscription.
				newSubscription = newDelCount > oldDelCount;

				// If its a new subscription, the action we want to log out is on the incoming list.
				// If its an un-subscription, the action we want is still available on the outgoing list.
				action = newSubscription ?
					         newInvocations[index: newDelCount     - 1] :
					         currentInvocations[index: oldDelCount - 1];
			}

			// Just a quick cache since we're going to access this string a fair few times.
//			var targetString = action.Target.ToString();

//			Log(targetString);

			// Snip out the name of the GameObject, right at the start
//			var gameObjectName = targetString.Substring(startIndex: 0,
//			                                            length: targetString.IndexOf(value: '(') - 1);


			// Snip out the name of the Component, found right at the end of the targetString
//			var indexOfLastPeriod = targetString.LastIndexOf(value: '(');

//			var componentName = targetString.Substring(startIndex: indexOfLastPeriod + 1,
//			                                           length: targetString.Length   - (indexOfLastPeriod + 2));

			// We can easily access the method name itself from the action.
//			Log(message: $"Broadcast subscription event:\n\nFrame\t[{Time.frameCount}]\nScene\t[{GameObject.Find(name: gameObjectName).scene.name}]\nGameObject\t[{gameObjectName}]\nComponent\t[{componentName}]\nMethod\t\t[{action.Method.Name}]\n\n{(newSubscription ? "subscribed to" : "un-subscribed from")}\n\nData Asset\t\t[{name}]\n");

			Log(message: $"Broadcast subscription event:\n\nFrame\t\t[{Time.frameCount}]\n\nName\t\t[{name}]\n\nMethod\t\t[{action.Method.Name}]\n\n{(newSubscription ? "subscribed to" : "un-subscribed from")}\n\nData Asset\t\t[{name}]\n");

		}


		public static void LogValueChange<T>(string name,
		                                     string typeName,
		                                     T from,
		                                     T to) => Log(message: $"Value changed:\n\nFrame\t[{Time.frameCount}]\nAsset\t[{name}]\nType\t[{typeName}]\nFrom\t[{from}]\nTo\t[{to}]\n");

	}

}