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
    public class LoadAndPlayVideo : MetaTask
    {

        [SerializeField]
        public string relativeVideoPath = "fetch/test/video.mp4";

        [SerializeField]
        public VideoPlayer videoPlayer;
        
        protected override bool CanAutoName() => videoPlayer && !string.IsNullOrEmpty(relativeVideoPath) && Path.HasExtension(relativeVideoPath);


        protected override string CreateAutoName() => $"Play video [{relativeVideoPath}]";


        protected override async UniTask<Tracer> _Run(Tracer tracer)
        {
            videoPlayer.url = Path.Combine(Application.persistentDataPath,
                                           relativeVideoPath);

            if (!File.Exists(videoPlayer.url))
            {
                Debug.LogWarning($"Video file not found [{videoPlayer.url}]");

                tracer.Continue = false;

                return tracer;
            }
            
            videoPlayer.Prepare();

            await UniTask.WaitUntil(() => videoPlayer.isPrepared);

            while (!videoPlayer.isPrepared)
            {
                if (tracer.Cancelled(this)) return tracer;
                
                await UniTask.Yield();
            }
            
            videoPlayer.Play();

            while (videoPlayer.isPlaying)
            {
                if (tracer.Cancelled(this)) return tracer;
                
                await UniTask.Yield();
            }
            
            return tracer;
        }

    }
}
