using System.Collections;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

using UnityEngine;

namespace Vanilla.JNode
{

    public class CollectionEditor<I> : MonoBehaviour
        where I : JNode
    {

        public bool prettyPrintStringOutput = true;

        public string input;

        [SerializeField]
        public I data;

        [TextArea(10,
                  40)]
        public string stringOutput;


        private void OnValidate()
        {
            if (!string.IsNullOrEmpty(value: input))
            {
                data.FromJson(json: input);
            }

            data.OnValidate();

            stringOutput = data.ToJson(prettyPrint: prettyPrintStringOutput);

            parseOutput.FromJson(json: stringOutput);
        }


        [SerializeField]
        public I parseOutput;


        private void OnEnable()
        {
//            parseOutput.OnPopulateSuccess += HandleParseSuccess;
//            parseOutput.OnPopulateFail    += HandleParseFailed;
        }


        private void HandleParseSuccess() => Debug.Log(message: "Parse successful!");

        private void HandleParseFailed() => Debug.LogError(message: "Parse failed!");


        private void OnDisable()
        {
//            parseOutput.OnPopulateSuccess -= HandleParseSuccess;
//            parseOutput.OnPopulateFail    -= HandleParseFailed;
        }

    }

}
