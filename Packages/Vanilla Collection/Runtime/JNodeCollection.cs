using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vanilla.Collection
{
    public interface JNodeCollection<I>
        where I : JNode
    {

        I[] Items
        {
            get;
        }
        
        Action OnNewItem
        {
            get;
            set;
        }
        
        void FromJson(string json)
        {
            try
            {
                JsonUtility.FromJsonOverwrite(json: json,
                                              objectToOverwrite: this);

//                JsonUtility.FromJson(json: json,
//                                     type: type);
//
//                JsonUtility.FromJson<T>(json: json);

//                if (Parsed)
//                {
//                    OnPopulateSuccess?.Invoke();
//                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            
        }
        
        string ToJson(bool prettyPrint = false) => JsonUtility.ToJson(obj: this, prettyPrint: prettyPrint);
    }
}
