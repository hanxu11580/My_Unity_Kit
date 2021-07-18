using UnityEngine;
using XhO_OKit;

public class LvMgr : Singleton<LvMgr>
{
    public int lvmax = 25;

    private const string CurrRealLvKey = "CurrRealLvKey";
    public int CurrRealLv { get; private set; }
    
    private const string SumLvKey = "SumLvKey";
    public int SumLv{ get; private set; }

    public LvMgr()
    {
        CurrRealLv = PlayerPrefs.GetInt(CurrRealLvKey, 1);
        SumLv = PlayerPrefs.GetInt(SumLvKey, 1);
        if (CurrRealLv > lvmax)
        {
            SetLv(lvmax);
        }
    }

    public void LvAddOne()
    {
        CurrRealLv += 1;
        if (CurrRealLv > lvmax) CurrRealLv = 1;
        SumLv += 1;
        PlayerPrefs.SetInt(CurrRealLvKey, CurrRealLv);
        PlayerPrefs.SetInt(SumLvKey, SumLv);
    }

    public void SetLv(int lvNum)
    {
        PlayerPrefs.SetInt(CurrRealLvKey, lvNum);
        PlayerPrefs.SetInt(SumLvKey, lvNum);
        CurrRealLv = lvNum;
        SumLv = lvNum;
    }
    
}
