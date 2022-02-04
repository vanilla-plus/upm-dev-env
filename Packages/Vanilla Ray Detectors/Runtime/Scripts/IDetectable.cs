using UnityEngine;

namespace Vanilla.RayDetectors
{

    public interface IDetector<TDetector, TDetectable>
        where TDetector : class, IDetector<TDetector, TDetectable>
        where TDetectable : Component, IDetectable<TDetector, TDetectable>
    {

        

    }

    public interface IDetectable<in TDetector, TDetectable>
        where TDetector : class, IDetector<TDetector, TDetectable>
        where TDetectable : Component, IDetectable<TDetector, TDetectable>
    {

        /// <summary>
        ///     This is called automatically by a DetectionSystem when it's ray is has first started making contact.
        /// </summary>
        /// <param name="rayDetector"></param>
        void OnDetectedBegin(TDetector rayDetector);


        /// <summary>
        ///     This is called automatically by a DetectionSystem once it's ray is no longer making contact.
        /// </summary>
        /// <param name="rayDetector"></param>
        void OnDetectedEnd(TDetector rayDetector);

    }

}