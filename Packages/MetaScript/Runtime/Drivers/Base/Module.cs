using System;

using UnityEngine;

using Vanilla.MetaScript.DataAssets;
using Vanilla.MetaScript.DataSources;

namespace Vanilla.MetaScript.Drivers
{
    
    [Serializable]
    public abstract class Module<T,S,A,M,D>
        where S : IDataSource<T>
        where A : DataAsset<T,S>
        where M : Module<T,S,A,M,D>
        where D : Driver<T,S,A,M,D>
    {

        public virtual void OnValidate(D driver) { }

        public abstract void Init(D driver);
        
        public abstract void DeInit(D driver);

        // This function checks if the Asset and Asset.Source exist first.

        protected virtual bool ValidReferences(D driver) => driver.Asset != null && driver.Asset.Source != null;

        // This will automatically connect a HandleSet if it can. Put it in override Init if desired.

        protected void TryConnectSet(D driver)
        {
            if (!ValidReferences(driver))
            {
                Debug.LogError("Connecting Set failed due to an invalid reference");

                return;
            }

            driver.Asset.Source.OnSet += HandleSet;
            
            HandleSet(driver.Asset.Source.Value);
        }
        
        protected virtual void HandleSet(T incoming) { }
        
        // This will automatically disconnect a HandleSet if it can. Put it in override DeInit if desired.

        protected void TryDisconnectSet(D driver)
        {
            if (!ValidReferences(driver))
            {
                Debug.LogError("Disconnecting Set failed due to an invalid reference");
                
                return;
            }

            driver.Asset.Source.OnSet -= HandleSet;
        }
        
        // This will automatically connect a HandleSetWithHistory if it can. Put it in override Init if desired.

        protected void TryConnectSetWithHistory(D driver)
        {
            if (!ValidReferences(driver))
            {
                Debug.LogError("Connecting Set With History failed due to an invalid reference");

                return;
            }

            driver.Asset.Source.OnSetWithHistory += HandleSetWithHistory;

            HandleSetWithHistory(driver.Asset.Source.Value,
                                 driver.InitialValue);
        }
        
        protected virtual void HandleSetWithHistory(T incoming, T outgoing) { }
        
        // This will automatically disconnect a HandleSetWithHistory if it can. Put it in override DeInit if desired.

        protected void TryDisconnectSetWithHistory(D driver)
        {
            if (!ValidReferences(driver))
            {
                Debug.LogError("Disconnecting Set With History failed due to an invalid reference");
                
                return;
            }

            driver.Asset.Source.OnSetWithHistory -= HandleSetWithHistory;
        }

        // " " but for AtMin


        protected void TryConnectAtMinSet(D driver)
        {
            if (!ValidReferences(driver))
            {
                Debug.LogError("Connecting AtMin failed due to an invalid reference");

                return;
            }
            
            if (driver.Asset.Source is not IRangedDataSource<T> source)
            {
				Debug.LogError("Connecting AtMin failed because the drivers Asset.Source is not of type RangedDataSource<T>");
				
                return;
            }
            
            source.AtMin.OnSet += HandleAtMinSet;
			
            HandleAtMinSet(source.AtMin.Value);
        }
        
        protected virtual void HandleAtMinSet(bool atMin) { }
        
        protected void TryDisconnectAtMinSet(D driver)
        {
            if (!ValidReferences(driver))
            {
                Debug.LogError("Disconnecting AtMin failed due to an invalid reference");

                return;
            }
            
            if (driver.Asset.Source is not IRangedDataSource<T> source)
            {
                Debug.LogError("Disconnecting AtMin failed because the drivers Asset.Source is not of type RangedDataSource<T>");
				
                return;
            }
            
            source.AtMin.OnSet -= HandleAtMinSet;
        }
        
        // " " but for AtMax
        
        
        protected void TryConnectAtMaxSet(D driver)
        {
            if (!ValidReferences(driver))
            {
                Debug.LogError("Connecting AtMax failed due to an invalid reference");

                return;
            }
            
            if (driver.Asset.Source is not IRangedDataSource<T> source)
            {
                Debug.LogError("Connecting AtMax failed because the drivers Asset.Source is not of type RangedDataSource<T>");
				
                return;
            }
            
            source.AtMax.OnSet += HandleAtMaxSet;
			
            HandleAtMaxSet(source.AtMax.Value);
        }
        
        protected virtual void HandleAtMaxSet(bool atMax) { }
        
        protected void TryDisconnectAtMaxSet(D driver)
        {
            if (!ValidReferences(driver))
            {
                Debug.LogError("Disconnecting AtMax failed due to an invalid reference");

                return;
            }
            
            if (driver.Asset.Source is not IRangedDataSource<T> source)
            {
                Debug.LogError("Disconnecting AtMax failed because the drivers Asset.Source is not of type RangedDataSource<T>");
				
                return;
            }
            
            source.AtMax.OnSet -= HandleAtMaxSet;
        }


//        public virtual void Init(Driver<T> driver)
//        {
//            driver.Asset.Source.OnSet += HandleSet;
//
//            HandleSet(driver.Asset.Source.Value);
//        }
//
//        public virtual void DeInit(Driver<T> driver) => driver.Asset.Source.OnSet -= HandleSet;
//
//        public abstract void HandleSet(T value);

    }
}
