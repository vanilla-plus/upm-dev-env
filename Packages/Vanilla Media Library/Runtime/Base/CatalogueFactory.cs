using System;

using Newtonsoft.Json;

using UnityEngine;

namespace Vanilla.MediaLibrary
{

    [Serializable]
    public abstract class CatalogueFactory<C, I> : MonoBehaviour
        where C : class, ICatalogue<I>
        where I : class, ICatalogueItem
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

        [SerializeField]
        public string escapedOutput;


        private void OnValidate()
        {
            if (!string.IsNullOrEmpty(value: input))
            {
                ImportFromJson();
            }

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


        private void ExportToJson()
        {
            output        = JsonUtility.ToJson(obj: catalogue);
            escapedOutput = JsonConvert.ToString(output);
        }


        protected abstract void ExportAsFetchForContextMenu();


        protected async void ExportAsFetch()
        {
            if (!Application.isPlaying) return;

            await MediaLibrary.FetchViaFactory(factory: this);
        }

    }

}