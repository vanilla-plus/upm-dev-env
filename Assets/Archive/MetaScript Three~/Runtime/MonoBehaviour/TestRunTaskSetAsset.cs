using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Vanilla.DataAssets;

namespace Vanilla.MetaScript.Three
{
    public class TestRunTaskSetAsset : MonoBehaviour
    {

        public Task_Set_Asset asset;

        public int what;
        
        public IntSocket testInt;

        public Text text;
        
        public void Awake()
        {
            Debug.Log("Awake");
            
            testInt.PlugIn();

            testInt.OnChanged += (a,
                                b) => text.text = $"Changed from {a} to {b}";

//            asset.set.Run();

//            asset.set.Debug_Run();

//            if (asset.set._async)
//            {
//                Run_Async();
//            }
//            else
//            {
//                Run();
//            }
        }


//        public void Run()
//        {
//            Debug.Log("Run damnit");
//            
//            asset.set.Run();
//        }
//
//
//        public async void Run_Async()
//        {
//            Debug.Log("Run async damnit");
//
//            await asset.set.Run_Async();
//        }


	    void Update()
	    {
		    if (Input.GetKeyDown(KeyCode.Alpha1))
		    {
			    testInt.Set(testInt.Get() + what);
		    }
	    }

    }
}
