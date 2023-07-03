using System.IO;
using System.Linq;
using System.Text;

using UnityEngine;

using Vanilla.StringFormatting;

namespace Vanilla.PlatformReport
{

    public static class Reports
    {

        public static void LogPlatformReport(bool application     = true,
                                             bool device          = true,
                                             bool operatingSystem = true,
                                             bool processors      = true,
                                             bool memory          = true,
                                             bool storage         = true,
                                             bool caching         = true,
                                             bool battery         = true,
                                             bool audio           = true,
                                             bool hardware        = true,
                                             bool rendering       = true,
                                             bool graphics        = true,
                                             bool shader          = true,
                                             bool compute         = true,
                                             bool texture         = true,
                                             bool misc            = true)
        {
            #if DEBUG
            var sb = new StringBuilder(capacity: 2048);

            sb.Append(value: $"\n[ Device Report ]\n\n");

            #region Application



            if (application)
            {
                sb.Append(value: $"\n[ Application ]\n\n");

                sb.Append(value: $"Product name          [{Application.productName}]\n");
                sb.Append(value: $"Company name          [{Application.companyName}]\n");
                sb.Append(value: $"Version               [{Application.version}]\n");
                sb.Append(value: $"Platform              [{Application.platform}]\n");
                sb.Append(value: $"Can confirm genuinity [{Application.genuineCheckAvailable}]\n");
                if (Application.genuineCheckAvailable) sb.Append(value: $"Genuine               [{Application.genuine}]\n");
                sb.Append(value: $"Install mode          [{Application.installMode}]\n");
                sb.Append(value: $"Identifier            [{Application.identifier}]\n");
                sb.Append(value: $"Made with Unity       [{Application.unityVersion}]\n");
                sb.Append(value: $"Build Tags\n");
                Application.GetBuildTags().ToList().ForEach(action: t => sb.Append(value: $"    {t}"));
                sb.Append(value: $"Development build     [{Debug.isDebugBuild}]\n");
            }



            #endregion

            #region Device



            if (device)
            {
                sb.Append(value: $"\n[ Device ]\n\n");

                sb.Append(value: $"Name  [{SystemInfo.deviceName}]\n");
                sb.Append(value: $"Model [{SystemInfo.deviceModel}]\n");
                sb.Append(value: $"Type  [{SystemInfo.deviceType}]\n");
                sb.Append(value: $"ID    [{SystemInfo.deviceUniqueIdentifier}]\n");
            }



            #endregion

            #region Operating system



            if (operatingSystem)
            {
                sb.Append(value: $"\n[ Operating System ]\n\n");

                sb.Append(value: $"Name   [{SystemInfo.operatingSystem}]\n");
                sb.Append(value: $"Family [{SystemInfo.operatingSystemFamily}]\n");
            }



            #endregion

            #region Processors



            if (processors)
            {
                sb.Append(value: $"\n[ Processor/s ]\n\n");

                sb.Append(value: $"Type         [{SystemInfo.processorType}]\n");
                sb.Append(value: $"Speed in MHz [{(SystemInfo.processorFrequency * Frequency.MHz).AsFrequency()}]\n");
                sb.Append(value: $"Count        [{SystemInfo.processorCount}]\n");
            }



            #endregion

            #region Memory



            if (memory)
            {
                sb.Append(value: $"\n[ Memory ]\n\n");

                sb.Append(value: $"System Memory [{(SystemInfo.systemMemorySize * DataSize.Mb).AsDataSize()}]\n");
            }



            #endregion

            #region Storage



            if (storage)
            {
                sb.Append(value: $"\n[ Storage ]\n\n");

                #region Android



                #if UNITY_ANDROID

                var javaClass = new AndroidJavaClass(className: "android.os.Environment");
                var file = javaClass.CallStatic<AndroidJavaObject>(methodName: "getDataDirectory");
                var path = file.Call<string>(methodName: "getAbsolutePath");

                var stat = new AndroidJavaObject(className: "android.os.StatFs",
                                                 path);

                var blocks = stat.Call<long>(methodName: "getAvailableBlocksLong");
                var blockSize = stat.Call<long>(methodName: "getBlockSizeLong");

                sb.Append($"Free disk-space [Android]   [{(blocks * blockSize).AsDataSize()}]\n" );

                #endif



                #endregion

                #region Windows



                #if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN

                sb.Append(value: $"Free disk-space [Windows]\n");

                var allDrives = DriveInfo.GetDrives();

                foreach (var d in allDrives)
                {
                    sb.Append(value: $"Drive [{d.Name}]");
                    sb.Append(value: $"    Drive type:                      [{d.DriveType}]");

                    if (!d.IsReady) continue;

                    sb.Append(value: $"    Volume label:                    [{d.VolumeLabel}]");
                    sb.Append(value: $"    File system:                     [{d.DriveFormat}]");
                    sb.Append(value: $"    Available space to current user: [{d.AvailableFreeSpace.AsDataSize()}]");
                    sb.Append(value: $"    Total available space:           [{d.TotalFreeSpace.AsDataSize()}]");
                    sb.Append(value: $"    Total size of drive:             [{d.TotalSize.AsDataSize()}]");
                }

                #endif



                #endregion

                #region IOS



                #if UNITY_IOS

                // ???

                #endif



                #endregion
            }



            #endregion

            #region Caching



            if (caching)
            {
                sb.Append(value: $"\n[ Caching ]\n\n");

                sb.Append(value: $"Caching system ready        [{Caching.ready}]\n");

                if (Caching.ready &&
                    Caching.currentCacheForWriting.valid)
                {
                    sb.Append(value: $"Cache count                 [{Caching.cacheCount}]\n");
                    sb.Append(value: $"Current cache path          [{Caching.currentCacheForWriting.path}]\n");
                    sb.Append(value: $"Current cache maximum space [{Caching.currentCacheForWriting.maximumAvailableStorageSpace.AsDataSize()}]\n");
                    sb.Append(value: $"Current cache read-only     [{Caching.currentCacheForWriting.readOnly}]\n");
                    sb.Append(value: $"Current cache bytes used    [{Caching.currentCacheForWriting.spaceOccupied.AsDataSize()}]\n");
                    sb.Append(value: $"Current cache bytes free    [{Caching.currentCacheForWriting.spaceFree.AsDataSize()}]\n");
                }
            }



            #endregion

            #region Battery



            if (battery)
            {
                sb.Append(value: $"\n[ Battery ]\n\n");

                sb.Append(value: $"Battery level  [{SystemInfo.batteryLevel * 100.0f}%]\n");
                sb.Append(value: $"Battery status [{SystemInfo.batteryStatus}]\n");
            }



            #endregion

            #region Audio



            if (audio)
            {
                sb.Append(value: $"\n[ Audio ]\n\n");

                sb.Append(value: $"Audio device available [{SystemInfo.supportsAudio}]\n");
            }



            #endregion

            #region Hardware



            if (hardware)
            {
                sb.Append(value: $"\n[ Hardware sensors ]\n\n");

                sb.Append(value: $"Location Services [{SystemInfo.supportsLocationService}]\n");
                sb.Append(value: $"Gyroscope         [{SystemInfo.supportsGyroscope}]\n");
                sb.Append(value: $"Accelerometer     [{SystemInfo.supportsAccelerometer}]\n");
                sb.Append(value: $"Vibration         [{SystemInfo.supportsVibration}]\n");
            }



            #endregion

            #region Rendering



            if (rendering)
            {
                sb.Append(value: $"\n[ Rendering ]\n\n");

                sb.Append(value: $"Graphics Tier                            [{Graphics.activeTier}]\n");
                sb.Append(value: $"Active color gamut                       [{Graphics.activeColorGamut}]\n");
                sb.Append(value: $"Rendering threading mode                 [{SystemInfo.renderingThreadingMode}]\n");
                sb.Append(value: $"Simultaneous render targets (MRTs) count [{SystemInfo.supportedRenderTargetCount}]\n");
                sb.Append(value: $"Separated render target blending         [{SystemInfo.supportsSeparatedRenderTargetsBlend}]\n");
            }



            #endregion

            #region Graphics



            if (graphics)
            {
                sb.Append(value: $"\n[ Graphics ]\n\n");

                sb.Append(value: $"Name                           [{SystemInfo.graphicsDeviceName}]\n");
                sb.Append(value: $"Type                           [{SystemInfo.graphicsDeviceType}]\n");
                sb.Append(value: $"Memory                         [{(SystemInfo.graphicsMemorySize * DataSize.Mb).AsDataSize()}]\n");
                sb.Append(value: $"ID                             [{SystemInfo.graphicsDeviceID}]\n");
                sb.Append(value: $"Vendor                         [{SystemInfo.graphicsDeviceVendor}]\n");
                sb.Append(value: $"Vendor ID                      [{SystemInfo.graphicsDeviceVendorID}]\n");
                sb.Append(value: $"Version                        [{SystemInfo.graphicsDeviceVersion}]\n");
                sb.Append(value: $"GPU multi-threaded rendering   [{SystemInfo.graphicsMultiThreaded}]\n");
                sb.Append(value: $"DrawCall instancing            [{SystemInfo.supportsInstancing}]\n");
                sb.Append(value: $"Shadows                        [{SystemInfo.supportsShadows}]\n");
                sb.Append(value: $"Raw Shadow Depth sampling      [{SystemInfo.supportsRawShadowDepthSampling}]\n");
                sb.Append(value: $"Quad topology                  [{SystemInfo.supportsHardwareQuadTopology}]\n");
                sb.Append(value: $"32-bit index buffers           [{SystemInfo.supports32bitsIndexBuffer}]\n");
                sb.Append(value: $"Motion vectors                 [{SystemInfo.supportsMotionVectors}]\n");
                sb.Append(value: $"Asynchronous GPU data readback [{SystemInfo.supportsAsyncGPUReadback}]\n");
                sb.Append(value: $"RayTracing                     [{SystemInfo.supportsRayTracing}]\n");
                sb.Append(value: $"Hidden surface removal         [{SystemInfo.hasHiddenSurfaceRemovalOnGPU}]\n");
            }



            #endregion

            #region Shader



            if (shader)
            {
                sb.Append(value: $"\n[ Shader ]\n\n");

                sb.Append(value: $"Shader level                                       [{SystemInfo.graphicsShaderLevel}]\n");
                sb.Append(value: $"Geometry                                           [{SystemInfo.supportsGeometryShaders}]\n");
                sb.Append(value: $"Tessellation                                       [{SystemInfo.supportsTessellationShaders}]\n");
                sb.Append(value: $"Dynamic Uniform Array-Indexing In Fragment Shaders [{SystemInfo.hasDynamicUniformArrayIndexingInFragmentShaders}]\n");
                sb.Append(value: $"Support for binding constant buffers directly      [{SystemInfo.supportsSetConstantBuffer}]\n");
            }



            #endregion

            #region Compute



            if (compute)
            {
                sb.Append(value: $"\n[ Compute ]\n\n");

                sb.Append(value: $"Compute support      [{SystemInfo.supportsComputeShaders}]\n");
                sb.Append(value: $"Async Compute queues [{SystemInfo.supportsAsyncCompute}]\n");
                sb.Append(value: $"GraphicsFences       [{SystemInfo.supportsGraphicsFence}]\n");
            }



            #endregion

            #region Texture



            if (texture)
            {
                sb.Append(value: $"\n[ Texture ]\n\n");

                sb.Append(value: $"Maximum texture size               [{SystemInfo.maxTextureSize}]\n");
                sb.Append(value: $"Maximum CubeMap size               [{SystemInfo.maxCubemapSize}]\n");
                sb.Append(value: $"Multi-sampled textures             [{SystemInfo.supportsMultisampledTextures == 1}]\n");
                sb.Append(value: $"Multi-sampled texture auto-resolve [{SystemInfo.supportsMultisampleAutoResolve}]\n");
                sb.Append(value: $"UV Starts at top                   [{SystemInfo.graphicsUVStartsAtTop}]\n");
                sb.Append(value: $"Sparse textures                    [{SystemInfo.supportsSparseTextures}]\n");
                sb.Append(value: $"Texture wrap mirror once           [{SystemInfo.supportsTextureWrapMirrorOnce == 1}]\n");
                sb.Append(value: $"GPU supports partial mipmap chains [{SystemInfo.hasMipMaxLevel}]\n");
                sb.Append(value: $"Streaming of texture mip maps      [{SystemInfo.supportsMipStreaming}]\n");
                sb.Append(value: $"2D Array textures                  [{SystemInfo.supports2DArrayTextures}]\n");
                sb.Append(value: $"3D (volume) textures               [{SystemInfo.supports3DTextures}]\n");
                sb.Append(value: $"3D (volume) RenderTextures         [{SystemInfo.supports3DRenderTextures}]\n");
                sb.Append(value: $"CubeMap Array textures             [{SystemInfo.supportsCubemapArrayTextures}]\n");
                sb.Append(value: $"NPOT (Nearest power-of-two)        [{SystemInfo.npotSupport}]\n");
            }



            #endregion

            #region Misc



            if (misc)
            {
                sb.Append(value: $"\n[ Misc ]\n\n");

                sb.Append(value: $"Random write target count       [{SystemInfo.supportedRandomWriteTargetCount}]\n");
                sb.Append(value: $"Reversed Z buffer               [{SystemInfo.usesReversedZBuffer}]\n");
                sb.Append(value: $"RenderBuffer Load/Store actions [{SystemInfo.usesLoadStoreActions}]\n");




                sb.Append(value: "\n");

                Debug.Log(message: sb.ToString());
            }



            #endregion

            #endif
        }

    }

}