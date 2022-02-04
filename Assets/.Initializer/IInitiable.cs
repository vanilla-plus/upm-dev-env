using Cysharp.Threading.Tasks;

public interface IInitiable
{
	
	bool Initialized
	{
		get;
	}

	UniTask Initialize();

	UniTask DeInitialize();

}