/****************************************************************************
 * .3 .

 ****************************************************************************/

namespace QFramework 
{
	using System;
	using System.Collections.Generic;
	
	/// <summary>
	/// manager基类
	/// </summary>
	public abstract class QMgrBehaviour : QMonoBehaviour,IManager
	{
		private readonly QEventSystem mEventSystem = NonPublicObjectPool<QEventSystem>.Instance.Allocate();

		#region IManager
		public virtual void Init() {}
		#endregion

		public abstract int ManagerId { get ; }

		public override IManager Manager
		{
			get { return this; }
		}

		public void RegisterEvent<T>(T msgId,OnEvent process) where T:IConvertible
		{
			mEventSystem.Register (msgId, process);
		}

		public void UnRegistEvent<T>(T msgEvent, OnEvent process) where T : IConvertible
		{
			mEventSystem.UnRegister(msgEvent, process);
		}

		public override void SendMsg(QMsg msg)
		{
            if (msg.ManagerID == ManagerId)
			{
                Process(msg.EventID, msg);
			}
			else 
			{
				QMsgCenter.Instance.SendMsg (msg);
			}
		}

        public override void SendEvent<T>(T eventId)
	    {
			SendMsg(QMsg.Allocate(eventId));
		}

		// 来了消息以后,通知整个消息链
		protected override void ProcessMsg(int eventId,QMsg msg)
		{
			mEventSystem.Send(msg.EventID,msg);
		}
		
		protected override void OnBeforeDestroy()
		{
			if (mEventSystem.IsNotNull())
			{
				mEventSystem.OnRecycled();
			}
		}
	}
}