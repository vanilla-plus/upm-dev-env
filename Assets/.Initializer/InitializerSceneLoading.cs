using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.SceneManagement;

using Vanilla.Initializer;

namespace MagicalProject
{

    public class InitializerSceneLoading : MonoBehaviour
    {
        
        // Direct
        
        [ContextMenu("Load A Direct")]
        public void LoadADirect()
        {
            Debug.Log("Beginning Load A Direct...");

            SceneManager.LoadScene(sceneBuildIndex: 1,
                                   mode: LoadSceneMode.Additive);

            Debug.Log("Finished Load A Direct");
        }
        
        [ContextMenu("Unload A Direct")]
        public void UnloadADirect()
        {
            Debug.Log("Beginning Unload A Direct...");

            SceneManager.UnloadScene(sceneBuildIndex: 1);

            Debug.Log("Finished Unload A Direct");
        }
        
        [ContextMenu("Load B Direct")]
        public void LoadBDirect()
        {
            Debug.Log("Beginning Load B Direct...");

            SceneManager.LoadScene(sceneBuildIndex: 2,
                                   mode: LoadSceneMode.Additive);

            Debug.Log("Finished Load B Direct");
        }
        
        [ContextMenu("Unload B Direct")]
        public void UnloadBDirect()
        {
            Debug.Log("Beginning Unload B Direct...");

            SceneManager.UnloadScene(sceneBuildIndex: 2);

            Debug.Log("Finished Unload B Direct");
        }
        
        // Async

        [ContextMenu("Load A Async")]
        public async void LoadAAsync()
        {
            Debug.Log("Beginning Load A Async...");

            await SceneManager.LoadSceneAsync(sceneBuildIndex: 1,
                                              mode: LoadSceneMode.Additive);

            Debug.Log("Finished Load A Async");
        }


        [ContextMenu("Unload A Async")]
        public async void UnloadAAsync()
        {
            Debug.Log("Beginning Unload A Async");

            await SceneManager.UnloadSceneAsync(sceneBuildIndex: 1);

            Debug.Log("Finished Unload A Async");
        }


        [ContextMenu("Load B Async")]
        public async void LoadBAsync()
        {
            Debug.Log("Beginning Load B Async...");

            await SceneManager.LoadSceneAsync(sceneBuildIndex: 2,
                                              mode: LoadSceneMode.Additive);

            Debug.Log("Finished Load B Async");
        }


        [ContextMenu("Unload B Async")]
        public async void UnloadBAsync()
        {
            Debug.Log("Beginning Unload A Async...");

            await SceneManager.UnloadSceneAsync(sceneBuildIndex: 2);

            Debug.Log("Finished Unload B Async");
        }
        
        // Spawn

        public TestingInitializer spawnable;



        [ContextMenu("Spawn")]
//        public void Spawn() => Instantiate(original: spawnable).Initialize();
        public void Spawn() => Initializer.Instantiate(spawnable);

    }

}