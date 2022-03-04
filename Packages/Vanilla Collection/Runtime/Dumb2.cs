using System;
using System.Collections;
using System.Collections.Generic;

using Unity.RemoteConfig;

using UnityEngine;

namespace Vanilla.Collection
{

    [Serializable]
    public class Something
    {

        public string  _name;
        public bool    _available;
        public int     _index;
        public float[] _latLong;

        public string somethingElseEntirely;

    }

    [Serializable]
    public class Dumb2 : MonoBehaviour
    {

        public struct userAttributes { }

        public struct appAttributes { }

        [SerializeField]
        public Something something;


        void Start()
        {
            ConfigManager.FetchCompleted += response =>
                                            {
                                                JsonUtility.FromJsonOverwrite(json: ConfigManager.appConfig.GetJson(key: "something",
                                                                                                                    defaultValue: string.Empty),
                                                                              objectToOverwrite: something);
                                            };


        }


        private void OnEnable() => ConfigManager.FetchConfigs(userAttributes: new userAttributes(),
                                                              appAttributes: new appAttributes());

    }

}