using UnityEngine;
using ET;

public class GameStart_Event : AEvent<EventStruct.GameStart>
{
    protected override async ETTask Run(EventStruct.GameStart a)
    {


        await ETTask.CompletedTask;
    }
}
