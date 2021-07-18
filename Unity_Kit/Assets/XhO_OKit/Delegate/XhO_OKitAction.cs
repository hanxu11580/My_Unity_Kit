namespace XhO_OKit
{
    /*
     *  C# 中默认可以  父类 parent = new 子类();
     *  但是不可以这样 子类 child = new 父类();
     * 
     * 
     *  in 逆变 针对输入值:
     *      string->object
     *  out 协变 针对输出值 
     *      obejct->string
     *      
     *  忘的话参考本文件夹内2个study文件
     */


    public delegate void XhO_OKitAction();

    /// <summary>
    /// 没有返回值的1个参数委托
    /// </summary>
    public delegate void XhO_OKitAction<in T>(T arg);

    /// <summary>
    /// 没有返回值的2个参数委托
    /// </summary>
    public delegate void XhO_OKitAction<in T1, in T2>(T1 arg1, T2 arg2);

    //有返回值
    public delegate bool XhO_OKitFuncBool();

}
