using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

using Vanilla.TypeMenu;

namespace Vanilla.MetaScript.Flow
{

	[Serializable]
	public class Send_Message : MetaTask
	{
		
		[SerializeField]
		private GameObject target;

		[SerializeField]
		private string methodName;

		[SerializeReference]
		[TypeMenu("magenta")]
		public ISendMessagePayload payload = null;

		[SerializeField]
		private SendMessageOptions options = SendMessageOptions.RequireReceiver;

		protected override bool CanAutoName() => target != null && !string.IsNullOrEmpty(methodName);

		protected override string CreateAutoName() => $"Call [{target.name}.{methodName}]";


		protected override UniTask<Scope> _Run(Scope scope)
		{
			if (scope.Cancelled) return UniTask.FromResult(scope);
			
			if (target == null)
			{
				Debug.LogError("SendMessage MetaTask: Target GameObject is not set.");

				return UniTask.FromResult(scope);
			}

			if (string.IsNullOrEmpty(methodName))
			{
				Debug.LogError("SendMessage MetaTask: Method name is not set.");

				return UniTask.FromResult(scope);
			}

			if (payload == null)
			{
				target.SendMessage(methodName: methodName,
				                   options: options);
			}
			else
			{
				target.SendMessage(methodName: methodName,
				                   value: payload.Value,
				                   options: options);
			}
			
			return UniTask.FromResult(scope);
		}

	}

	public interface ISendMessagePayload
	{

		object Value
		{
			get;
		}

	}

	[Serializable] public class Bool : ISendMessagePayload
	{

		[SerializeField]
		public bool value;

		public object Value => value;

	}

	[Serializable] public class Vec1 : ISendMessagePayload
	{

		[SerializeField]
		public float value;

		public object Value => value;
	}

	[Serializable] public class Vec2 : ISendMessagePayload
	{

		[SerializeField]
		public Vector2 value;

		public object Value => value;
	}

	[Serializable] public class Vec3 : ISendMessagePayload
	{

		[SerializeField]
		public Vector3 value;

		public object Value => value;
	}

	[Serializable] public class Int : ISendMessagePayload
	{

		[SerializeField]
		public int value;

		public object Value => value;
	}

	[Serializable] public class String : ISendMessagePayload
	{

		[SerializeField]
		public string value;

		public object Value => value;
	}

}