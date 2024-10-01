using UnityEngine;

namespace Vanilla.MetaScript.DataSources.Bindings
{

	[SerializeField]
	public class DataSource_To_Animator_Parameter_Int : DataSource_To_Animator_Parameter<int>
	{

		protected override void HandleDataSet(int value) => Animator.SetInteger(_parameterID,
		                                                                        value);

	}

}