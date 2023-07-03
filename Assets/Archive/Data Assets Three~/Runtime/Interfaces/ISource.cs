using Cysharp.Threading.Tasks;

using UnityEngine.Events;

namespace Vanilla.DataAssets.Three
{

	// ---------------------------------------------------------------------------------------------------------------------------------- Sources //

	public interface IDataSource<TType> : IGettable<TType>,
	                                      ISettable<TType>,
	                                      ISetReceiver<TType>,
	                                      IBroadcaster<TType>,
	                                      IBroadcastReceiver<TType>,
	                                      IInitiable,
	                                      IValidatable { }

	// ---------------------------------------------------------------------------------------------------------------------------------- Getting //

	public interface IGettable<TType>
	{

		TType Get();

	}

	public interface IAsyncGettable<TType>
	{

		UniTask<TType> Get();

	}

	// ---------------------------------------------------------------------------------------------------------------------------------- Setting //

	public interface ISettable<TType>
	{

		UnityEvent<TType, TType> OnSet
		{
			get;
		}

		void Set(TType newValue);

	}

	public interface IAsyncSettable<TType>
	{

		UnityEvent<TType, TType> OnSet
		{
			get;
		}

		UniTask<bool> Set(TType newValue);

	}

	public interface ISetReceiver<TType>
	{

		void SetReceived(TType outgoing,
		                 TType incoming);

	}

	// ----------------------------------------------------------------------------------------------------------------------------- Broadcasting //

	public interface IBroadcaster<TType>
	{

		UnityEvent<TType> OnBroadcast
		{
			get;
		}

		void Broadcast();


	}

	public interface IBroadcastReceiver<TType>
	{

		void BroadcastReceived(TType incoming);

	}

}