using System;

using Cysharp.Threading.Tasks;

namespace Vanilla
{

    public interface INormal
    {

        Toggle Empty
        {
            get;
        }

        Toggle Full
        {
            get;
        }

        float Value
        {
            get;
            set;
        }

        Action<float> OnChange
        {
            get;
            set;
        }

        Action<float> OnIncrease
        {
            get;
            set;
        }

        Action<float> OnDecrease
        {
            get;
            set;
        }

        void OnValidate();

        UniTask Fill(Toggle conditional,
                     bool targetCondition = true,
                     float speed = 1.0f);

        UniTask Drain(Toggle conditional,
                      bool targetCondition = true,
                      float speed = 1.0f);

    }

}