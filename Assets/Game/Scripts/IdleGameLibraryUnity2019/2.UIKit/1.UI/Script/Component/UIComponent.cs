/****************************************************************************
 * .5 ~ 8 .
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
    public abstract class UIComponent : UIElement
    {
        public override UIMarkType GetUIMarkType()
        {
            return UIMarkType.Component;
        }
    }
}