

using System.Collections.Generic;

namespace QFramework
{
    /// Automatic Reference Counting (ARC)
    /// is used internally to prevent pooling retained Objects.
    /// If you use retain manually you also have to
    /// release it manually at some point.
    /// SafeARC checks if the object has already been
    /// retained or released. It's slower, but you keep the information
    /// about the owners.
    public sealed class SafeARC : IRefCounter
    {
        public int RefCount
        {
            get { return mOwners.Count; }
        }

        public HashSet<object> Owners
        {
            get { return mOwners; }
        }

        readonly HashSet<object> mOwners = new HashSet<object>();

        public void Retain(object refOwner)
        {
            if (!Owners.Add(refOwner))
            {
                Log.E("ObjectIsAlreadyRetainedByOwnerException");
            }
        }

        public void Release(object refOwner)
        {
            if (!Owners.Remove(refOwner))
            {
                Log.E("ObjectIsNotRetainedByOwnerExceptionWithHint");
            }
        }
    }
}