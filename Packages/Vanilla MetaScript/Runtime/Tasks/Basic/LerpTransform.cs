using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Vanilla.MetaScript
{

	[Serializable]
	public class LerpTransform : MetaTask
	{

		protected override bool CanDescribe() => target && start && end;

		protected override string DescribeTask() => $"Lerp [{target.name}] from [{start.name}] to [{end.name}]";

		public Transform target;

		public Transform start;
		public Transform end;

		public float secondsToTake = 2.0f;

		public bool position = true;
		public bool rotation = true;
		public bool scale    = false;

		public bool local = false;


		protected override async UniTask _Run(CancellationTokenSource cancellationTokenSource)
		{
			var t     = 0.0f;
			var scale = 1.0f / secondsToTake;

			var startPos = local ?
				               start.localPosition :
				               start.position;

			var startRot = local ?
				               start.localRotation :
				               start.rotation;

			var startScale = start.localScale;

			var endPos = local ?
				             end.localPosition :
				             end.position;

			var endRot = local ?
				             end.localRotation :
				             end.rotation;

			var endScale = end.localScale;

			while (t < 1.0f)
			{
//				if (cancellationTokenSource.IsCancellationRequested) return;
				cancellationTokenSource.Token.ThrowIfCancellationRequested();
				
				if (local)
				{
					if (position)
						target.localPosition = Vector3.Lerp(a: startPos,
						                                    b: endPos,
						                                    t: t);

					if (rotation)
						target.localRotation = Quaternion.Lerp(a: startRot,
						                                       b: endRot,
						                                       t: t);

					if (this.scale)
						target.localScale = Vector3.Lerp(a: startScale,
						                                 b: endScale,
						                                 t: t);
				}
				else
				{
					if (position)
						target.position = Vector3.Lerp(a: startPos,
						                               b: endPos,
						                               t: t);

					if (rotation)
						target.rotation = Quaternion.Lerp(a: startRot,
						                                  b: endRot,
						                                  t: t);

					if (this.scale)
						target.localScale = Vector3.Lerp(a: startScale,
						                                 b: endScale,
						                                 t: t);
				}



				t += Time.deltaTime * scale * Time.timeScale;

				await UniTask.Yield();
			}
		}

	}

}