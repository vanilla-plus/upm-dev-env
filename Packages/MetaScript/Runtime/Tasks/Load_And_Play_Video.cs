using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using Cysharp.Threading.Tasks;

using UnityEngine;
using UnityEngine.Video;

namespace Vanilla.MetaScript
{
    
    [Serializable]
    public class Load_And_Play_Video : MetaTask
    {

        [SerializeField]
        public string relativeVideoPath = "fetch/test/video.mp4";

        [SerializeField]
        public VideoPlayer videoPlayer;
        
        protected override bool CanAutoName() => videoPlayer && !string.IsNullOrEmpty(relativeVideoPath) && Path.HasExtension(relativeVideoPath);


        protected override string CreateAutoName() => $"Play video [{relativeVideoPath}]";


        protected override async UniTask<Scope> _Run(Scope scope)
        {
            videoPlayer.url = Path.Combine(Application.persistentDataPath,
                                           relativeVideoPath);

            if (!File.Exists(videoPlayer.url))
            {
                Debug.LogWarning($"Video file not found [{videoPlayer.url}]");

//                trace.Continue = false;
                scope.Cancel();

                return scope;
            }
            
            videoPlayer.Prepare();

            while (!videoPlayer.isPrepared)
            {
//                if (tracer.HasBeenCancelled(this)) return tracer;
                if (scope.Cancelled) return scope;

                await UniTask.Yield();
            }
            
            videoPlayer.Play();

            while (videoPlayer.isPlaying)
            {
//                if (tracer.HasBeenCancelled(this)) return tracer;
                if (scope.Cancelled) return scope;

                await UniTask.Yield();
            }
            
            return scope;
        }

    }
}
