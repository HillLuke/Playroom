

namespace Assets.Scripts.UI.ItemCollections
{
    public class DragObject
    {
        public UIItemCollection UIItemCollection;
        public UIItemSlot UIItemSlot;

        public DragObject(UIItemSlot slot)
        {
            UIItemSlot = slot;
            UIItemCollection = slot.GetComponent<UIItemCollection>();
        }
    }
}
