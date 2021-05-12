/****************************************************************************
 * .3 .

 ****************************************************************************/

namespace QFramework {
    using UnityEngine;

    public class Hide : MonoBehaviour {
        private void Awake() {
            this.gameObject.SetActive(false);
        }
    }
}