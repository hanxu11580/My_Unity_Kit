using XhO_OKit;
using XhO_OKit.CommonUI;

public class StartProceduce : ProcedureBase
{
    bool isClose;
    
    public override void OnInit(IFsm<IProcedureManager> fsm)
    {
        FrameworkEntry.GetManager<EventManager>().Subscribe<UICloseEventArgs>(UICloseCall);
    }

    public override void OnEnter(IFsm<IProcedureManager> fsm)
    {
        isClose = false;
    }
 
    public override void OnUpdate(IFsm<IProcedureManager> fsm, float elapseSeconds, float realElapseSeconds)
    {
        if (isClose)
        {
            ChangeState<GameProceduce>(fsm);
        }
    }
    
    protected virtual void UICloseCall(object sender, GlobalEventArgs e)
    {

    }
}

