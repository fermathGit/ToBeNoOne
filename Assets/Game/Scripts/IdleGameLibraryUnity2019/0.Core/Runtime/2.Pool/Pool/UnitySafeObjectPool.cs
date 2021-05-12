namespace QFramework {
    using System;
    using UnityEngine;

    public class UnitySafeObjectPool<T> : Pool<T> where T : IPoolable {


        public void Init( int maxCount, int initCount ) {
            MaxCacheCount = maxCount;

            if ( maxCount > 0 ) {
                initCount = Math.Min( maxCount, initCount );
            }

            if ( CurCount < initCount ) {
                for ( var i = CurCount; i < initCount; ++i ) {
                    Recycle( mFactory.Create() );
                }
            }
        }

        /// <summary>
        /// Gets or sets the max cache count.
        /// </summary>
        /// <value>The max cache count.</value>
        public int MaxCacheCount {
            get { return mMaxCount; }
            set {
                mMaxCount = value;

                if ( mCacheStack != null ) {
                    if ( mMaxCount > 0 ) {
                        if ( mMaxCount < mCacheStack.Count ) {
                            int removeCount = mCacheStack.Count - mMaxCount;
                            while ( removeCount > 0 ) {
                                mCacheStack.Pop();
                                --removeCount;
                            }
                        }
                    }
                }
            }
        }

        public override T Allocate() {
            var result= base.Allocate();
            result.IsRecycled = false;
            return result;
        }

        public override bool Recycle( T t ) {
            if ( t == null || t.IsRecycled ) {
                return false;
            }

            if ( mMaxCount > 0 ) {
                if ( mCacheStack.Count >= mMaxCount ) {
                    t.OnRecycled();
                    return false;
                }
            }

            t.IsRecycled = true;
            t.OnRecycled();
            mCacheStack.Push( t );

            return true;
        }
    }
}
