using UnityEngine;

namespace Vanilla.MetaScript
{
    public class Persist : MonoBehaviour
    {

        void OnEnable() => DontDestroyOnLoad(gameObject);

    }
}
