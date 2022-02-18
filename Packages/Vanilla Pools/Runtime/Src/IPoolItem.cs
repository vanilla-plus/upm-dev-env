using Cysharp.Threading.Tasks;

namespace Vanilla.Pools
{

	public interface IPoolItem
	{

		bool LeasedFromPool
		{
			get;
			set;
		}
		
		IPool<IPoolItem> Pool
		{
			get;
			set;
		}

		UniTask OnGet();

		UniTask OnRetire();

		UniTask RetireSelf();

	}

}