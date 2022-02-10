using System;

using UnityEngine;

namespace Vanilla.MenuMachine
{

    [Serializable]
    public abstract class CatalogueBuilder<C, I> : MonoBehaviour
        where C : ICatalogue<I>
        where I : ICatalogueItem
    {

        [Header("Input")]
        [TextArea(minLines: 10,
                  maxLines: 100)]
        [SerializeField]
        [Tooltip("If you need to import a pre-existing catalogue json state, copy and paste it here. If you want to simply edit below, leave this field empty.")]
        public string input;

        [Header("Editor")]
        [SerializeField]
        public C catalogue;

        [Header("Output")]
        [TextArea(minLines: 10,
                  maxLines: 100)]
        [SerializeField]
        public string output;


        private void OnValidate()
        {
            if (!string.IsNullOrEmpty(value: input))
            {
                ImportFromJson();
            }

            catalogue.Initialized = false;
            
            ExportToJson();
        }


        private void Awake()
        {
            #if !UNITY_EDITOR
            Destroy(this);
            #endif
        }


        private void ImportFromJson()
        {
            try
            {
                catalogue = JsonUtility.FromJson<C>(json: input);
            }
            catch (Exception e)
            {
                Debug.LogException(exception: e);
            }
        }


        private void ExportToJson() => output = JsonUtility.ToJson(obj: catalogue);

    }

}