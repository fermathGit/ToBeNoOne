/****************************************************************************
 * Copyright (c) 2017 ~ 2018.5 .
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
	using UnityEngine.Events;
	
	/// <inheritdoc />
	/// <summary>
	/// 全局唯一继承于MonoBehaviour的单例类，保证其他公共模块都以App的生命周期为准
	/// 这个东西很基类，没什么用。概念也不太清晰
	/// </summary>
	[QMonoSingletonPath("[Framework]/QFramework")]
	public class Framework : QMgrBehaviour, ISingleton
	{
		/// <summary>
		/// 组合的方式实现单例的模板
		/// </summary>
		/// <value>The instance.</value>
        public static Framework Instance
		{
            get { return MonoSingletonProperty<Framework>.Instance; }
		}
		
		public override int ManagerId
		{
			get { return QMgrID.Framework; }
		}

		public void OnSingletonInit()
		{
		}

		public void Dispose()
		{
		}

        private Framework()
		{
		}

		#region 全局生命周期回调

		public UnityAction OnUpdateEvent = delegate { };
		public UnityAction OnFixedUpdateEvent = delegate { };
		public UnityAction OnLateUpdateEvent = delegate { };
		public UnityAction OnGUIEvent = delegate { };
		public UnityAction OnDestroyEvent = delegate { };
		public UnityAction OnApplicationQuitEvent = delegate { };

		private void Update()
		{
			OnUpdateEvent.InvokeGracefully();
		}

		private void FixedUpdate()
		{
			OnFixedUpdateEvent.InvokeGracefully();
		}

		private void LateUpdate()
		{
			OnLateUpdateEvent.InvokeGracefully();
		}

		private void OnGUI()
		{
			OnGUIEvent.InvokeGracefully();
		}

		protected override void OnDestroy()
		{
			OnDestroyEvent.InvokeGracefully();
		}

		private void OnApplicationQuit()
		{
			OnApplicationQuitEvent.InvokeGracefully();
		}

		#endregion
	}
}