#if UNITY_EDITOR
using System.IO;
using ET;
using ILRuntime.Runtime.CLRBinding;
using UnityEditor;

public class ILRuntimeCLRBinding
{
    [MenuItem("Tools/ILRuntime/Generate CLR Binding")]
    static void GenerateCLRBindingByAnalysis()
    {
        //分析热更dll调用引用来生成绑定代码
        ILRuntime.Runtime.Enviorment.AppDomain domain = new ILRuntime.Runtime.Enviorment.AppDomain();
        using (FileStream fsHotfix = new FileStream("Assets/Res/Code/Hotfix.dll.bytes", FileMode.Open, FileAccess.Read))
        {
            domain.LoadAssembly(fsHotfix);
            ILHelper.InitILRuntime(domain);
            BindingCodeGenerator.GenerateBindingCode(domain, "Assets/Model/Module/ILRuntime/Generated");
        }

        AssetDatabase.Refresh();
    }
}
#endif