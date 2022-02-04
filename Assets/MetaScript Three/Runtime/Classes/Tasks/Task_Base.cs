using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.Three
{

	public interface ITask : IValidatable
	{

		bool async
		{
			get;
			set;
		}

		UniTask Run();

	}

}