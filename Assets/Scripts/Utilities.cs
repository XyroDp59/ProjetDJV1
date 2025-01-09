using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Utilities
{
    public static IEnumerator ExecActionAfterTime(float time, UnityAction unityAction)
    {
        yield return new WaitForSeconds(time);
        unityAction();
    }

    public static IEnumerator ExecActionAfterTime(WaitForSeconds delay, UnityAction unityAction)
    {
        yield return delay;
        unityAction();
    }
}
