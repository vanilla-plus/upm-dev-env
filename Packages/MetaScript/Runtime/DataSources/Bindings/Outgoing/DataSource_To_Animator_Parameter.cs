using System;

using UnityEngine;

using Vanilla.MetaScript.DataSources.Strings;

namespace Vanilla.MetaScript.DataSources.Bindings
{

	[SerializeField]
	public abstract class DataSource_To_Animator_Parameter<T, S> : MonoBehaviour
		where S : IDataSource<T>
	{

		[SerializeField] public Animator Animator;

		[SerializeReference] public S Source;

		[SerializeReference] public StringSource ParameterName;

		[NonSerialized] protected int _parameterID = -1;


		void OnEnable()
		{
			HandleNameSet(ParameterName.Value);

			Source.OnSet        += HandleDataSet;
			ParameterName.OnSet += HandleNameSet;
		}


		protected abstract void HandleDataSet(T value);


		void OnDisable()
		{
			ParameterName.OnSet -= HandleNameSet;
			Source.OnSet        -= HandleDataSet;
		}


		private void HandleNameSet(string newName) => _parameterID = Animator.StringToHash(newName);

	}

}