

namespace QFramework
{
	public abstract class Singleton<T> : ISingleton where T : Singleton<T>
	{
		protected static T mInstance;

		static object mLock = new object();

		protected Singleton()
		{
		}

		public static T Instance
		{
			get
			{
				lock (mLock)
				{
					if (mInstance == null)
					{
						mInstance = SingletonCreator.CreateSingleton<T>();
					}
				}

				return mInstance;
			}
		}

		public virtual void Dispose()
		{
			mInstance = null;
		}

		public virtual void OnSingletonInit()
		{
		}
	}

	[System.Obsolete("弃用啦，建议用 Singleton")]
	public abstract class QSingleton<T> : Singleton<T> where T : QSingleton<T>
	{
		protected QSingleton()
		{
		}
	}
}