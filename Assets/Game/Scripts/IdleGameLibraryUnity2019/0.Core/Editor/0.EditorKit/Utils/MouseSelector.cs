/****************************************************************************
 * 
 * .3 .

 ****************************************************************************/

namespace QFramework
{
    using UnityEditor;
    using System.IO;

    public static class MouseSelector
    {
        public static string GetSelectedPathOrFallback()
        {
            var path = string.Empty;

            foreach (var obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if (path.IsNotNullAndEmpty() && File.Exists(path))
                {
                }
            }

            return path;
        }
    }
}