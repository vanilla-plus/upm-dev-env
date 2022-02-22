using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Vanilla.MediaLibrary
{

    [Serializable]
    public class State
    {

        [SerializeField] private Toggle _toggle;

        [SerializeField] private Normal _normal;
        public                   Toggle Toggle => _toggle;
        public                   Normal Normal => _normal;
        
        public                   float  seconds = 1.0f;

         private float _speed = 1.0f;

        public State(bool startingState,
                     float startingValue)
        {
            _toggle = new Toggle(startingState: startingState);

            _normal = new Normal(startingValue: startingValue);
        }


        internal void OnValidate() => _speed = 1.0f / seconds;


        public void Init()
        {
            _toggle.onTrue += () => _normal.Fill(conditional: _toggle,
                                                 targetCondition: true,
                                                 speed: _speed);

            _toggle.onFalse += () => _normal.Drain(conditional: _toggle,
                                                   targetCondition: false,
                                                   speed: _speed);
        }

    }

}