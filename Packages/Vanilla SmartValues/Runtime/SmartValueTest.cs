#if UNITY_EDITOR || DEVELOPMENT_BUILD
#define debug
#endif

using UnityEngine;

namespace Vanilla.SmartValues
{
    public class SmartValueTest : MonoBehaviour
    {

//	    [SerializeField]
//	    public SmartBool serializedSmartBool = new("Serialized SmartBool",
//	                                               false);

//	    [NonSerialized]
//	    public SmartBool nonSerializedSmartBool = new("NonSerialized SmartBool",
//	                                                  false);
//
//	    [SerializeField]
//	    public SmartFloat test = new SmartFloat("Serialized SmartFloat",
//	                                            0.1f,
//	                                            0.0f,
//	                                            1.0f,
//	                                            Mathf.Epsilon,
//	                                            0.05f);

	    [SerializeField]
	    public SmartFloat test = new SmartFloat("Serialized SmartFloat",
	                                            0.1f);
        
//        [NonSerialized]
//        public SmartFloat nonSerializedSmartFloat = new SmartFloat("NonSerialized SmartFloat",
//                                                                                       0.0f,
//                                                                                       -1.0f,
//                                                                                       1.0f,
//                                                                                       Mathf.Epsilon);
//        
//
//	    [SerializeField]
//	    public SmartSinWave test = new SmartSinWave("Smart SinWave",
//	                                                   0.0f,
//	                                                   -1.0f,
//	                                                   1.0f,
//	                                                   Mathf.Epsilon);

//	    [SerializeField]
//	    public SmartInt test = new SmartInt("SmartInt");

//	    [SerializeField]
//	    public SmartByte test = new("SmartByte",
//	                                10,
//	                                10,
//	                                74);

	    
        private void OnEnable() => test.OnValueChanged += HandleFloat;

        private void OnDisable() => test.OnValueChanged -= HandleFloat;

        void HandleFloat(float value,
                           float old) => transform.position = new Vector3(0,
                                                                          value,
                                                                          0);


        void HandleInt(int value,
                       int old) => transform.position = new Vector3(0,
                                                                    value,
                                                                    0);
        
        void HandleByte(byte value,
                        byte old) => transform.position = new Vector3(0,
                                                                      value,
                                                                      0);
        
        void Update()
        {
//	        test.Update(Time.deltaTime);
	        test.Value += Time.deltaTime;
//	        test.Value += 1;
        }

    }
}