using UnityEngine;

namespace Vanilla.MetaScript.DataSources.Bindings
{

	[SerializeField]
	public class DataSource_To_Animator_Parameter_Float : DataSource_To_Animator_Parameter<float, FloatSource>
	{

		protected override void HandleDataSet(float value) => Animator.SetFloat(_parameterID,
		                                                                        value);

	}

}