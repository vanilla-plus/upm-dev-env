using System;
using System.Diagnostics;
using System.Linq;

using Cysharp.Threading.Tasks;

using Unity.Profiling;

using UnityEngine;
using UnityEngine.Profiling;

using Vanilla.StringFormatting;

using Debug = UnityEngine.Debug;

namespace Vanilla.MetaScript
{
    public class TaskTester : MonoBehaviour
    {

        [SerializeReference]
        [SerializeReferenceButton]
        [SerializeReferenceUIRestrictionIncludeTypes(typeof(IMetaScriptTask))]
        private IMetaScriptTask target;

        public int testCount = 100;

        [SerializeField]
        public readonly ProfilerCategory[] profilerCategories = {
                                                                    ProfilerCategory.Ai, 
                                                                    ProfilerCategory.Animation, 
                                                                    ProfilerCategory.Audio, 
                                                                    ProfilerCategory.Gui, 
                                                                    ProfilerCategory.Input,
                                                                    ProfilerCategory.Internal,
                                                                    ProfilerCategory.Lighting,
                                                                    ProfilerCategory.Loading,
                                                                    ProfilerCategory.Memory,
                                                                    ProfilerCategory.Network,
                                                                    ProfilerCategory.Particles,
                                                                    ProfilerCategory.Physics,
                                                                    ProfilerCategory.Render,
                                                                    ProfilerCategory.Scripts,
                                                                    ProfilerCategory.Video,
                                                                    ProfilerCategory.Vr,
                                                                    ProfilerCategory.VirtualTexturing
                                                                };
        
        [ContextMenu(itemName: "Run StopWatch Test")]
        public async void RunStopwatchTest()
        {
            var testID = -1;

            var testTicks        = new long[testCount];
            var testMilliseconds = new long[testCount];

            while (++testID < testCount)
            {
                var s = Stopwatch.StartNew();

                await target.Run();

                s.Stop();

                testTicks[testID] = s.ElapsedTicks;
                testMilliseconds[testID] = s.ElapsedMilliseconds;
            }

//            Debug.Log($"Stopwatch Test completed for [{taskRunner.task.name}]:\n\n - Average ticks\t\t[{testTicks.Average()}]\n - Average milliseconds\t[{testMilliseconds.Average()}]\n");
        }
        
        [ContextMenu(itemName: "Run Profiler Tests")]
        public async void RunProfilerTests()
        {
            Profiler.enabled = true;

            var profilers       = new ProfilerRecorder[profilerCategories.Length];
            var profilerTallies = new long[profilerCategories.Length][];

            for (var i = 0;
                 i < profilerTallies.Length;
                 i++)
            {
                profilerTallies[i] = new long[testCount];
            }

            var testID = -1;

            while (++testID < testCount)
            {
                for (var i = 0;
                     i < profilerCategories.Length;
                     i++)
                {
                    profilers[i] = ProfilerRecorder.StartNew(category: profilerCategories[i],
                                                             statName: profilerCategories[i].Name,
                                                             capacity: 1,
                                                             options: ProfilerRecorderOptions.Default);
                }

                await UniTask.NextFrame();

                await target.Run();

                await UniTask.NextFrame();

                for (var i = 0;
                     i < profilerCategories.Length;
                     i++)
                {
                    profilerTallies[i][testID] = profilers[i].LastValue;
                }
            }

            await UniTask.NextFrame();

            var output = $"Profiler Report completed for [{target.GetDescription()}]:\n\n";

//            foreach (var l in profilerTallies)
//            {
//                foreach (var x in l)
//                {
//                    Debug.Log(x); // 1700 logs if 100 tests; careful!
//                }
//            }
            
            for (var i = 0;
                 i < profilers.Length;
                 i++)
            {
                if (i == 1 ||
                    i == 16)
                {
                    output += $" - {profilerCategories[i]}\t{((long)profilerTallies[i].Average()).AsDataSize()}\n";
                }
                else
                {
                    output += $" - {profilerCategories[i]}\t\t{((long)profilerTallies[i].Average()).AsDataSize()}\n";
                }
                
                profilers[i].Dispose();
            }

            Debug.Log(message: output);

            Profiler.enabled = false;
        }


        private void OnValidate() => target.OnValidate();

    }
}
