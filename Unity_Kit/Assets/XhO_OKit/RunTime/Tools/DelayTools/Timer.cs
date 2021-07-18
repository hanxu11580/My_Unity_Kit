using UnityEngine;
using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Object = UnityEngine.Object;

/// <summary>
/// Allows you to run events on a delay without the use of <see cref="Coroutine"/>s
/// or <see cref="MonoBehaviour"/>s.
/// 
/// To create and start a Timer, use the <see cref="Register"/> method.
/// </summary>
public class Timer
{
    
    #region Public Properties/Fields

    /// <summary>
    /// 消耗多久时间
    /// </summary>
    public float duration { get; private set; }

    /// <summary>
    /// 计时是否循环
    /// </summary>
    public bool isLooped { get; set; }

    /// <summary>
    /// 定时器是否完成运行
    /// </summary>
    public bool isCompleted { get; private set; }

    /// <summary>
    /// 是否使用真实时间，不会受timeScale影响
    /// </summary>
    public bool usesRealTime { get; private set; }

    /// <summary>
    /// 是否暂停
    /// </summary>
    public bool isPaused => _timeElapsedBeforePause.HasValue;

    /// <summary>
    /// 定时器是否被取消
    /// </summary>
    public bool isCancelled => _timeElapsedBeforeCancel.HasValue;

    /// <summary>
    /// Get whether or not the timer has finished running for any reason.
    /// </summary>
    public bool isDone => isCompleted || isCancelled || isOwnerDestroyed;

    #endregion

    #region Public Static Methods

    /// <summary>
    /// 注册一个定时器
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="onComplete"></param>
    /// <param name="onUpdate"></param>
    /// <param name="isLooped"></param>
    /// <param name="useRealTime"></param>
    /// <param name="autoDestroyOwner"></param>
    /// <returns></returns>
    public static Timer Register(float duration, Action onComplete, Action<float> onUpdate = null,
        bool isLooped = false, bool useRealTime = false, MonoBehaviour autoDestroyOwner = null)
    {
        if (_manager == null)
        {
            TimerManager managerInScene = Object.FindObjectOfType<TimerManager>();
            if (managerInScene != null)
            {
                _manager = managerInScene;
            }
            else
            {
                GameObject managerObject = new GameObject { name = "TimerManager" };
                _manager = managerObject.AddComponent<TimerManager>();
            }
        }

        Timer timer = new Timer(duration, onComplete, onUpdate, isLooped, useRealTime, autoDestroyOwner);
        _manager.RegisterTimer(timer);
        return timer;
    }

    /// <summary>
    /// 取消定时器
    /// </summary>
    /// <param name="timer"></param>
    public static void Cancel(Timer timer)
    {
        if (timer != null)
        {
            timer.Cancel();
        }
    }

    /// <summary>
    /// 暂停一个定时器
    /// </summary>
    public static void Pause(Timer timer)
    {
        if (timer != null)
        {
            timer.Pause();
        }
    }

    /// <summary>
    /// 恢复一个定时器
    /// </summary>
    /// <param name="timer"></param>
    public static void Resume(Timer timer)
    {
        if (timer != null)
        {
            timer.Resume();
        }
    }
    
    /// <summary>
    /// 取消所有定时器
    /// </summary>
    public static void CancelAllRegisteredTimers()
    {
        if (_manager != null)
        {
            _manager.CancelAllTimers();
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// 取消
    /// </summary>
    public void Cancel()
    {
        if (isDone)
        {
            return;
        }

        _timeElapsedBeforeCancel = GetTimeElapsed();
        _timeElapsedBeforePause = null;
    }

    /// <summary>
    /// 暂停
    /// </summary>
    public void Pause()
    {
        if (isPaused || isDone)
        {
            return;
        }

        _timeElapsedBeforePause = GetTimeElapsed();
    }

    /// <summary>
    /// 恢复
    /// </summary>
    public void Resume()
    {
        if (!isPaused || isDone)
        {
            return;
        }

        _timeElapsedBeforePause = null;
    }

    /// <summary>
    /// 已经经过时间（从定时器开始到现在时间）
    /// </summary>
    /// <returns></returns>
    public float GetTimeElapsed()
    {
        //当前时间大于定时完成时间
        if (isCompleted || GetWorldTime() >= GetFireTime())
        {
            return duration;
        }
        
        return _timeElapsedBeforeCancel ??
               _timeElapsedBeforePause ??
               GetWorldTime() - _startTime;
    }

    /// <summary>
    /// 还剩多久完成
    /// </summary>
    /// <returns></returns>
    public float GetTimeRemaining()
    {
        return duration - GetTimeElapsed();
    }

    /// <summary>
    /// 完成比例
    /// </summary>
    /// <returns></returns>
    public float GetRatioComplete()
    {
        return GetTimeElapsed() / duration;
    }

    /// <summary>
    /// 未完成比例
    /// </summary>
    /// <returns></returns>
    public float GetRatioRemaining()
    {
        return GetTimeRemaining() / duration;
    }

    #endregion

    #region Private Static Properties/Fields

    // responsible for updating all registered timers
    private static TimerManager _manager;

    #endregion

    #region Private Properties/Fields

    private bool isOwnerDestroyed => _hasAutoDestroyOwner && _autoDestroyOwner == null;

    private readonly Action _onComplete;
    private readonly Action<float> _onUpdate;
    
    // 定时开始时间
    private float _startTime;
    
    //上一帧时间
    private float _lastUpdateTime;

    //取消时经过时间
    private float? _timeElapsedBeforeCancel;
    // 暂停时经过时间
    // 如果存在值 说明此时还是暂停状态
    private float? _timeElapsedBeforePause;

    /// <summary>
    /// 自动销毁所有者 定时器将会失效
    /// </summary>
    private readonly MonoBehaviour _autoDestroyOwner;
    private readonly bool _hasAutoDestroyOwner;

    #endregion

    #region Private Constructor (use static Register method to create new timer)

    private Timer(float duration, Action onComplete, Action<float> onUpdate,
        bool isLooped, bool usesRealTime, MonoBehaviour autoDestroyOwner)
    {
        this.duration = duration;
        this._onComplete = onComplete;
        this._onUpdate = onUpdate;

        this.isLooped = isLooped;
        this.usesRealTime = usesRealTime;

        this._autoDestroyOwner = autoDestroyOwner;
        this._hasAutoDestroyOwner = autoDestroyOwner != null;

        this._startTime = this.GetWorldTime();
        this._lastUpdateTime = this._startTime;
    }

    #endregion

    #region Private Methods
    
    /// <summary>
    /// 程序开始过去了多久,根据usesRealTime不同影响
    /// </summary>
    /// <returns></returns>
    private float GetWorldTime()
    {
        return usesRealTime ? Time.realtimeSinceStartup : Time.time;
    }
    
    /// <summary>
    /// 获取完成时间
    /// </summary>
    /// <returns></returns>
    private float GetFireTime()
    {
        return _startTime + duration;
    }
    
    /// <summary>
    /// 帧时间差
    /// </summary>
    /// <returns></returns>
    private float GetTimeDelta()
    {
        return GetWorldTime() - _lastUpdateTime;
    }

    private void Update()
    {
        if (isDone)
        {
            return;
        }

        if (isPaused)
        {
            //每次暂停都会重新启动时间
            _startTime += GetTimeDelta();
            _lastUpdateTime = GetWorldTime();
            return;
        }

        _lastUpdateTime = GetWorldTime();
        
        _onUpdate?.Invoke(this.GetTimeElapsed());
        
        if (GetWorldTime() >= GetFireTime()) //完成定时
        {
            _onComplete?.Invoke();
            
            if (isLooped)
            {
                _startTime = GetWorldTime();
            }
            else
            {
                isCompleted = true;
            }
        }
    }

    #endregion

    #region Manager Class (implementation detail, spawned automatically and updates all registered timers)
    
    private class TimerManager : MonoBehaviour
    {
        private List<Timer> _timers = new List<Timer>();
        
        private List<Timer> _timersToAdd = new List<Timer>();

        public void RegisterTimer(Timer timer)
        {
            _timersToAdd.Add(timer);
        }

        public void CancelAllTimers()
        {
            foreach (Timer timer in _timers)
            {
                timer.Cancel();
            }

            _timers.Clear();
            _timersToAdd.Clear();
        }
        
        [UsedImplicitly]
        private void Update()
        {
            UpdateAllTimers();
        }

        private void UpdateAllTimers()
        {
            if (_timersToAdd.Count > 0)
            {
                _timers.AddRange(_timersToAdd);
                _timersToAdd.Clear();
            }

            foreach (Timer timer in _timers)
            {
                timer.Update();
            }

            _timers.RemoveAll(t => t.isDone);
        }
    }

    #endregion

}
