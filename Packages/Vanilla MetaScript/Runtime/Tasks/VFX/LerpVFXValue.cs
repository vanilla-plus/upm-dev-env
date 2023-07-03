using System;

using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;
using System.Threading;

using Vanilla.MetaScript;

[Serializable]
public class LerpVFXValue : MetaTask
{
	protected override bool CanDescribe() => _vfxgraph;

	protected override string DescribeTask() => $"Lerp [{ShaderProperty}] from [{StartValue}] to [{EndValue}]";

	[SerializeField]
	public UnityEngine.VFX.VisualEffect _vfxgraph;
	[SerializeField]
	public float StartValue = 0.0f;
	[SerializeField]
	public float EndValue = 1.0f;
	[SerializeField]
	public float secondsToTake = 1.0f;
	[SerializeField]
	public string ShaderProperty = "_Alpha";


	protected override async UniTask _Run(CancellationTokenSource cancellationTokenSource)
	{
		var t     = 0.0f;
		var scale = 1.0f / secondsToTake;

		var startFade = StartValue;
		var endFade = EndValue;
		
		float value = 0.0f;

		while (t < 1.0f)
		{
			value = Mathf.Lerp(startFade, endFade,t);

			_vfxgraph.SetFloat(ShaderProperty, value);

			t += Time.deltaTime * scale * Time.timeScale;

			await UniTask.Yield();
		}

		_vfxgraph.SetFloat(ShaderProperty, EndValue);
	}

}