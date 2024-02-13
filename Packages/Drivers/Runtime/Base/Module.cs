using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.Drivers
{
    
    [Serializable]
    public abstract class Module<T>
    {

        public virtual void OnValidate(Driver<T> driver)
        {
	        if (!driver.Asset)
	        {
		        Debug.LogError("Driver missing asset");

		        return;
	        }
	        
//	        Debug.Log(driver.Asset.Delta.Value);
	        
//            var value = driver.Asset.Delta.Value;

//            foreach (var m in Snrubs) m?.OnValidate(value);
        }


        public virtual void Init(Driver<T> driver)
        {
//            foreach (var m in Snrubs) m?.Init(driver);

            driver.Asset.Source.OnSet += HandleValueChange;

            HandleValueChange(driver.Asset.Source.Value);
        }
        
//        public void HandleValueChange(float value)
//        {
//            foreach (var m in Snrubs) m?.HandleValueChange(value);
//        }


        public virtual void DeInit(Driver<T> driver) => driver.Asset.Source.OnSet -= HandleValueChange;

        public abstract void HandleValueChange(T value);

    }
}
