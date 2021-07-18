using ET;

namespace Hotfix
{
    public abstract class AUIEvent
    {
        public abstract ETTask<UI> OnCreate(UIComponent uiComponent, string uiType);
        public abstract void OnRemove(UIComponent uiComponent);
    }
}