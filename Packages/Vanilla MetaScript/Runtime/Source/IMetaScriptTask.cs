using System.Threading;
using System.Threading.Tasks;

using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript
{

	public interface IMetaScriptTask
	{

		string GetDescription();

		void OnValidate();

//		bool Running();

		UniTask Run();

//		void Cancel();

	}

}