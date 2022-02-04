using Cysharp.Threading.Tasks;

public interface IInitiable
{

	UniTask Initialize();

	UniTask DeInitialize();

}