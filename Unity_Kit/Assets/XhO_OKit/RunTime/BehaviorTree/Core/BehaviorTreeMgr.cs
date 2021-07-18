
namespace XhO_OKit.BehaviorTree
{
    /// <summary>
    /// 行为树管理
    /// 没有Editor辅助 纯代码行为树 太高的深度 很难管理
    /// 使用感觉就是：
    ///     1、嵌套Node返回的3个状态容易混乱
    ///     2、类太多了，以行为为Node 导致每个行为一个类，并且判断Node，组合Node都需要根据具体情况编写
    /// 好处就是拓展感觉挺好拓展的
    ///     1、添加新分支 只需在初始化 添加新Node
    /// 
    /// </summary>
    public class BehaviorTreeMgr : ManagerBase
    {
        //TODO
        //1、创建出常用的组合Node

        internal override void InitModule()
        {
            
        }

        internal override void UpdateModule(float elapseSeconds, float realElapseSeconds)
        {
            
        }

        internal override void ShutDownModule()
        {
            
        }
    }
    
}

