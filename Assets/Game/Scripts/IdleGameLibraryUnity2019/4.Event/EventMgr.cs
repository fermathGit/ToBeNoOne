using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static class EventMgr
{
	public delegate void Callback();
	public delegate void Callback<T>(T arg1);
	public delegate void Callback<T, U>(T arg1, U arg2);
	public delegate void Callback<T, U, V>(T arg1, U arg2, V arg3);

	private static Dictionary<int, Delegate> _events = new Dictionary<int, Delegate>();

	#region Notify
	public static void Notify(int eventType) 
	{		
		Delegate d;
		if (_events.TryGetValue(eventType, out d)) 
		{
			Callback callback = d as Callback;			
			if (callback != null)
				callback();
		}
	}

	public static void Notify<T>(int eventType, T arg1) 
	{
		Delegate d;
		if (_events.TryGetValue(eventType, out d)) 
		{
#if UNITY_EDITOR
			System.Reflection.ParameterInfo[] pas = d.Method.GetParameters();
			if(pas[0].ParameterType != arg1.GetType())
			{
				Debug.Log("Notify failed, arg1 type should be " + pas[0].ParameterType.ToString());
				return;
			}
#endif
			Callback<T> callback = d as Callback<T>;

			if (callback != null) 
				callback(arg1);
		}
	}

	public static void Notify<T, U>(int eventType, T arg1, U arg2) 
	{		
		Delegate d;
		if (_events.TryGetValue(eventType, out d)) 
		{
#if UNITY_EDITOR
			System.Reflection.ParameterInfo[] pas = d.Method.GetParameters();
			if(pas[0].ParameterType != arg1.GetType())
			{
				Debug.Log("Notify failed, arg1 type should be " + pas[0].ParameterType.ToString());
				return;
			}
			if(pas[1].ParameterType != arg2.GetType())
			{
				Debug.Log("Notify failed, arg2 type should be " + pas[1].ParameterType.ToString());
				return;
			}
#endif

			Callback<T, U> callback = d as Callback<T, U>;
			if (callback != null)
				callback(arg1, arg2);
		}
	}

	public static void Notify<T, U, V>(int eventType, T arg1, U arg2, V arg3) 
	{
		Delegate d;
		if (_events.TryGetValue (eventType, out d)) 
		{
#if UNITY_EDITOR
			System.Reflection.ParameterInfo[] pas = d.Method.GetParameters();
			if(pas[0].ParameterType != arg1.GetType())
			{
				Debug.Log("Notify failed, arg1 type should be " + pas[0].ParameterType.ToString());
				return;
			}
			if(pas[1].ParameterType != arg2.GetType())
			{
				Debug.Log("Notify failed, arg2 type should be " + pas[1].ParameterType.ToString());
				return;
			}
			if(pas[2].ParameterType != arg3.GetType())
			{
				Debug.Log("Notify failed, arg3 type should be " + pas[2].ParameterType.ToString());
				return;
			}
#endif

			Callback<T, U, V> callback = d as Callback<T, U, V>;
			if (callback != null)
				callback (arg1, arg2, arg3);
		}
	}
	#endregion

	#region Register
	private static bool CheckRegist(int eventType, Delegate callback) {

		if (!_events.ContainsKey(eventType)) {
			_events.Add(eventType, null );
		}
		
		Delegate d = _events[eventType];
		if (d != null && d.GetType () != callback.GetType ()) 
		{
			Debug.LogError("CheckRegist error, " + d.GetType().ToString() + " and " + callback.GetType().ToString());
			return false;
		}

		return true;
	}

    // 注册事件
	public static void Register(int eventType, Callback callback)
	{
		if (CheckRegist (eventType, callback)) 
			_events[eventType] = (Callback)_events[eventType] + callback;
	}

	public static void Register<T>(int eventType, Callback<T> callback)
	{
		if (CheckRegist (eventType, callback)) 
			_events[eventType] = (Callback<T>)_events[eventType] + callback;
	}

	public static void Register<T, U>(int eventType, Callback<T, U> callback)
	{
		if (CheckRegist (eventType, callback)) 
			_events[eventType] = (Callback<T, U>)_events[eventType] + callback;
	}

	public static void Register<T, U, V>(int eventType, Callback<T, U, V> callback)
	{
		if (CheckRegist (eventType, callback)) 
			_events[eventType] = (Callback<T, U, V>)_events[eventType] + callback;
	}
	#endregion

	#region UnRegister
	private static bool CheckUnRegist(int eventType, Delegate callback)
	{		
		if (_events.ContainsKey(eventType)) 
		{
			Delegate d = _events[eventType];

			if(null == d)
			{
				Debug.LogError("CheckUnRegist error, d is null");
				return false;
			}


			if(d.GetType() != callback.GetType())
			{
				Debug.LogError("CheckUnRegist error, " + d.GetType().ToString() + " and " + callback.GetType().ToString());
				return false;
			}

			return true;
		} 

		return false;
	}

	public static void Unregister(int eventType, Callback callback)
    {
		if(CheckUnRegist(eventType, callback))
			_events[eventType] = (Callback)_events[eventType] - callback;
    }

	public static void Unregister<T>(int eventType, Callback<T> callback)
	{
		if(CheckUnRegist(eventType, callback))
			_events[eventType] = (Callback<T>)_events[eventType] - callback;
	}

	public static void Unregister<T, U>(int eventType, Callback<T, U> callback)
	{
		if(CheckUnRegist(eventType, callback))
			_events[eventType] = (Callback<T, U>)_events[eventType] - callback;
	}

	public static void Unregister<T, U, V>(int eventType, Callback<T, U, V> callback)
	{
		if(CheckUnRegist(eventType, callback))
			_events[eventType] = (Callback<T, U, V>)_events[eventType] - callback;
	}
	#endregion

	public static void Release()
    {
        _events.Clear();
    }
}

