using UnityEngine;
//using UnityEngine.SceneManagement;

namespace Vanilla.Init
{

	public class ExecutionOrderTester : MonoBehaviour, IInitiable
    {
                                                                                                 static      ExecutionOrderTester()         => Debug.Log("A => ExecutionOrderTester => Static Constructor");
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)] static void SubsystemRegistration()        => Debug.Log("B => ExecutionOrderTester => SubsystemRegistration");
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)] static void AfterAssembliesLoaded()        => Debug.Log("C => ExecutionOrderTester => AfterAssembliesLoaded");
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]    static void BeforeSplashScreen()           => Debug.Log("D => ExecutionOrderTester => BeforeSplashScreen");
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]       static void BeforeSceneLoad()              => Debug.Log("E => ExecutionOrderTester => BeforeSceneLoad");
        private                                                                                        void Awake()                         => Debug.Log("F => ExecutionOrderTester => Awake");
        private                                                                                        void OnEnable()                      => Debug.Log("G => ExecutionOrderTester => OnEnable");
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]       static void AfterSceneLoad()                => Debug.Log("H => ExecutionOrderTester => AfterSceneLoad");
        [RuntimeInitializeOnLoadMethod]                                                         static void RuntimeInitializeOnLoadMethod() => Debug.Log("I => ExecutionOrderTester => RuntimeInitializeOnLoadMethod");
        public                                                                                         void Init()                          => Debug.Log("J => ExecutionOrderTester => Init");
        public                                                                                         void PostInit()                      => Debug.Log("K => ExecutionOrderTester => PostInit");
        public                                                                                         void Start()                         => Debug.Log("L => ExecutionOrderTester => Start");


//        [ContextMenu("Test Scene Load")]
//        public void TestSceneLoad() => SceneManager.LoadScene(1,
//                                                              LoadSceneMode.Additive);

    }
}
