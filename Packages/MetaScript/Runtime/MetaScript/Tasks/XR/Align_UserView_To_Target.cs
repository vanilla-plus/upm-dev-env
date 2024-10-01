using System;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript.XR
{

	[Serializable]
	public class Align_UserView_To_Target : MetaTask
	{

		[SerializeField] public Transform rigRoot;
		[SerializeField] public Transform usersHeadTransform;
		[SerializeField] public Transform targetInScene;

		[SerializeField] public float distanceFromTarget = -1.0f;

		protected override bool Validate => rigRoot && usersHeadTransform && targetInScene;

		protected override string CreateAutoName() => $"Align [{rigRoot.gameObject.name}] so that [{usersHeadTransform.gameObject.name}] faces [{targetInScene.gameObject.name}]";


		protected override UniTask<Scope> _Run(Scope scope)
		{
			var localRot = usersHeadTransform.rotation * Quaternion.Inverse(rigRoot.rotation);

			rigRoot.rotation =  targetInScene.rotation * Quaternion.Inverse(localRot);
			rigRoot.position += targetInScene.position - usersHeadTransform.position;

			rigRoot.RotateAround(usersHeadTransform.position,
			                     Vector3.up,
			                     180.0f);

			var e = rigRoot.eulerAngles;

			e.x = e.z = 0.0f;

			rigRoot.eulerAngles = e;

			var p = rigRoot.position;

			p.y = 0.0f;

			rigRoot.position = p;

			rigRoot.Translate(new Vector3(0,
			                              0,
			                              distanceFromTarget),
			                  usersHeadTransform);

			return UniTask.FromResult(scope);
		}

	}

}