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
        public bool parseInput = true;
        public bool parseOutput = true;
        public bool prettyPrint = true;

        [TextArea(10,
                  40)]
        public string stringOutput;


        protected virtual void OnValidate()
        {
            if (parseInput && !string.IsNullOrEmpty(value: input))
            {
                data.FromJson(json: input);
            }

            gameObject.name = GetName() + " Editor";

            data.OnValidate();

            if (parseOutput)
            {
                stringOutput = data.ToJson(prettyPrint: prettyPrint);
            }
        }


        protected abstract string GetName();

    }

}