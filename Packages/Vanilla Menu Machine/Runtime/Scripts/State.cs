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
        public                   Toggle toggle => _toggle;

        [SerializeField] private Normal _normal;
        public                   Normal normal => _normal;

        [SerializeField] public float speed = 2.0f;

        public State(bool startingState,
                     float startingValue)
        {
            _toggle = new Toggle(startingState: startingState);

            _normal = new Normal(startingValue: startingValue);
        }


        public void Init()
        {
            _toggle.onTrue += () => _normal.Fill(conditional: _toggle,
                                                 targetCondition: true,
                                                 speed: speed);

            _toggle.onFalse += () => _normal.Drain(conditional: _toggle,
                                                   targetCondition: false,
                                                   speed: speed);
        }

    }

}