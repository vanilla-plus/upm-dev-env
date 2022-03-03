using System;
using System.Collections;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

using UnityEngine;

namespace Vanilla.Collection
{

    [Serializable]
    public class TestPayload : JNode
    {

        private Action _onPopulateFail;

        public bool Parsed
        {
            get => _parsed;
            set => _parsed = value;
        }

        public  Action OnPopulateFail
        {
            get => _onPopulateFail;
            set => _onPopulateFail = value;
        }

        private Action _onPopulateSuccess;
        public Action OnPopulateSuccess
        {
            get => _onPopulateSuccess;
            set => _onPopulateSuccess = value;
        }


        public void OnValidate()
        {
            
        }

        public  string Name;
        private bool   _parsed;

    }
    
    public class Dumb : MonoBehaviour
    {

        public string json;
        
        public TestPayload TestPayload;
        
        [ContextMenu("Parse")]
        public void Parse()
        {
            
        }
    }
    
    // ---------------------------------------------------------------------------------------- //

    public class SampleCollectionItem : CollectionItem
    {

        

    }

    public class SampleCollection : Collection<SampleCollectionItem>
    {

        private string _itemArrayKey;

        protected override string ItemArrayKey => _itemArrayKey;

    }

}
