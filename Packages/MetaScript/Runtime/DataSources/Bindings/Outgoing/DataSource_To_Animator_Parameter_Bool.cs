using UnityEngine;

namespace Vanilla.MetaScript.DataSources.Bindings
{

	[SerializeField]
	public class DataSource_To_Animator_Parameter_Bool : DataSource_To_Animator_Parameter<bool, BoolSource>
	{

		protected override void HandleDataSet(bool value) => Animator.SetBool(_parameterID,
		                                                                      value);

	}

}