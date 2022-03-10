using System;

using UnityEngine;

namespace Vanilla.JNode
{

    [Serializable]
    public abstract class JNodeEditor<I> : MonoBehaviour
        where I : JNode, new()
    {

        [Header("Input Json")]
        [TextArea(10,
                  40)]
        public string input;

        [Header("Editor Class")]
        [SerializeField]
        public I data = new();

        [Header("Output Json")]
        public bool prettyPrint = true;

        [TextArea(10,
                  40)]
        public string stringOutput;


        protected virtual void OnValidate()
        {
            if (!string.IsNullOrEmpty(value: input))
            {
                data.FromJson(json: input);
            }

            data.OnValidate();

            stringOutput = data.ToJson(prettyPrint: prettyPrint);
        }

    }

}