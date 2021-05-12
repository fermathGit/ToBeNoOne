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

namespace QFramework
{
    using UnityEngine;
    
    public interface IPanelLoader
    {
        GameObject LoadPanelPrefab(string panelName);

        void Unload();
    }
}