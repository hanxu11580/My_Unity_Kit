//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2021年5月1日 18:37:25
//------------------------------------------------------------

using LitJson;

namespace Hotfix
{
    public class LitJsonHelper
    {
        /// <summary>
        /// 注册一些类型
        /// </summary>
        public static void Init()
        {
            JsonMapper.RegisterExporter<float>((obj, writer)=>writer.Write(obj.ToString()));
            JsonMapper.RegisterImporter<string, float>(input => float.Parse(input));

        }
    }
}