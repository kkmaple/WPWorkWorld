﻿using UnityEngine;
using System.Collections;

/// <summary>
/// 描述 忽略TimeScale 而提供的一些时间 参数
/// 
/// Maintaince Logs:
/// 2015-03-30	WP			Initial version. 
/// </summary>
public class RealTime : MonoBehaviour
{

    static RealTime mInst;

    float mRealTime = 0f;
    float mRealDelta = 0f;

    /// <summary>
    /// Real time since startup.
    /// </summary>

    static public float time
    {
        get
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) return Time.realtimeSinceStartup;
#endif
            if (mInst == null) Spawn();
            return mInst.mRealTime;
        }
    }

    /// <summary>
    /// Real delta time.
    /// </summary>

    static public float deltaTime
    {
        get
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) return 0f;
#endif
            if (mInst == null) Spawn();
            return mInst.mRealDelta;
        }
    }

    static void Spawn()
    {
        GameObject go = new GameObject("_RealTime");
        DontDestroyOnLoad(go);
        mInst = go.AddComponent<RealTime>();
        mInst.mRealTime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        float rt = Time.realtimeSinceStartup;
        mRealDelta = Mathf.Clamp01(rt - mRealTime);
        mRealTime = rt;
    }
}
