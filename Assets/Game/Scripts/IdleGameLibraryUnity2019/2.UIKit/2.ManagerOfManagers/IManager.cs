/****************************************************************************
 * Copyright (c) 2017 ~ 2018.7 .
 * 
*
 *
*
*
 * 
*
 ****************************************************************************/


namespace QFramework
{
	using System;

	/// <summary>
	/// Support Manager of Manager
	/// </summary>
	public interface IManager 
	{
		void Init();
		
		void RegisterEvent<T>(T msgId, OnEvent process) where T : IConvertible;

		void UnRegistEvent<T>(T msgEvent, OnEvent process) where T : IConvertible;
		
		void SendEvent<T>(T eventId) where T : IConvertible;

		void SendMsg(QMsg msg);
	}
}