using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vanilla.DataAssets.Samples
{

    public class SampleEditor : SampleBase
    {

        public Toggle boolInput;
        public Slider floatInput;
        public Slider intInput;
        public Slider vector3XInput;
        public Slider vector3YInput;
        public Slider vector3ZInput;
        public InputField stringInput;
        public Button eventButton;
        public Button gameObjectButton;
        
        protected override void OnEnable()
        {
            base.OnEnable();

            boolInput.isOn = boolBinding.value;
            
            floatInput.value = floatBinding.value;
            
            intInput.value = intBinding.value;
            
            vector3XInput.value = vector3Binding.value.x;
            vector3YInput.value = vector3Binding.value.y;
            vector3ZInput.value = vector3Binding.value.z;

            stringInput.text = stringBinding.value;
            
            boolInput.onValueChanged.AddListener(call: BoolEdit);
            
            floatInput.onValueChanged.AddListener(call: FloatEdit);
            
            intInput.onValueChanged.AddListener(call: IntEdit);
            
            vector3XInput.onValueChanged.AddListener(call: Vector3XEdit);
            vector3YInput.onValueChanged.AddListener(call: Vector3YEdit);
            vector3ZInput.onValueChanged.AddListener(call: Vector3ZEdit);
            
            stringInput.onValueChanged.AddListener(call: StringEdit);

            eventButton.onClick.AddListener(call: eventBinding.Broadcast);
            
            gameObjectButton.onClick.AddListener(call: AssignGameObject);
        }


        private void BoolEdit(bool input) => boolBinding.value = input;

        private void FloatEdit(float input) => floatBinding.value = input;

        private void IntEdit(float input) => intBinding.value = Mathf.RoundToInt(f: input);


        private void Vector3XEdit(float input)
        {
            var v = vector3Binding.value;
            
            v.x = input;

            vector3Binding.value = v;
        }


        private void Vector3YEdit(float input)
        {
            var v = vector3Binding.value;
            
            v.y = input;

            vector3Binding.value = v;
        }


        private void Vector3ZEdit(float input)
        {
            var v = vector3Binding.value;
            
            v.z = input;

            vector3Binding.value = v;
        }


        private void StringEdit(string input) => stringBinding.value = input;


        private void AssignGameObject()
        {
            gameObjectBinding.value = Camera.main.gameObject;
        }
    }

}