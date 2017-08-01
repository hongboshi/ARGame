using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMono : MonoBehaviour {

    #region 协程封装
    /// <summary>
    /// 延迟执行
    /// </summary>
    /// <param name="time"></param>
    /// <param name="action"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public static IEnumerator DelaySometimeExcute(float time, System.Action<object> action, object param)
    {

        yield return new WaitForSeconds(time);
        action(param);
    }
    public static IEnumerator DelaySometimeExcute(float time, System.Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }
    public static IEnumerator DelayOneframeExcute(System.Action<object> action, object param)
    {
        yield return null;
        action(param);
    }
    public static IEnumerator DelayOneframeExcute(System.Action action)
    {
        yield return null;
        action();
    }
    #endregion
}
