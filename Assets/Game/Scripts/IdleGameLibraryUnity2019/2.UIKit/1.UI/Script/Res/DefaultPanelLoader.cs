/****************************************************************************
 * 
 * 
*
 *
*
*
 * 
*
 ****************************************************************************/

namespace QFramework {
    using UnityEngine;

    /// <summary>
    /// Default
    /// </summary>
    public class DefaultPanelLoader : IPanelLoader {
        //ResLoader mResLoader = ResLoader.Allocate();

        public GameObject LoadPanelPrefab(string panelName) {
            return Resources.Load<GameObject>(panelName);
        }

        public GameObject LoadPanelPrefab(string assetBundleName, string panelName) {
            return Resources.Load<GameObject>(panelName);
        }

        public void Unload() {

        }
    }
}