using UnityEngine;
using UnityEngine.UI;

namespace BoEngine.UI
{
    public class EventGraphic : Graphic, ICanvasRaycastFilter
    {
        public override void Rebuild(CanvasUpdate update)
        {
            return;
        }

        public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            return true;
        }
    }
}
