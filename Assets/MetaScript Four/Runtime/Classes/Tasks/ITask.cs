using Cysharp.Threading.Tasks;

namespace Vanilla.MetaScript.Four
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