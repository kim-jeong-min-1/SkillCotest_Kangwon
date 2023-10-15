using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierObject : MonoBehaviour
{
    protected Vector3[] staticPoints = new Vector3[2];
    protected Vector3[] upadatePoints = new Vector3[2];
    protected float bezierValue;
    protected bool isDone = false;

    [SerializeField] protected float bezierWidth;

    public void BezierStart(float time, Vector3 target)
    {
        SetBezier(target);
        StartCoroutine(bezier());
        IEnumerator bezier()
        {
            float curTime = 0;
            float percent = 0;

            while (percent < 1)
            {
                curTime += Time.deltaTime;
                percent = curTime / time;

                var pos = GetBezieorValue(percent, staticPoints, upadatePoints);
                Rotate(pos);
                transform.position = pos;
                yield return null;
            }
            isDone = true;
        }
    }

    public void SetBezier(Vector3 pos)
    {
        staticPoints[0] = transform.position;
        staticPoints[1] = transform.position + Vector3.up * bezierWidth;

        upadatePoints[0] = pos + Vector3.up * bezierWidth;
        upadatePoints[1] = pos;
    }

    public void BezierUpdate(Vector3 pos)
    {
        upadatePoints[0] = pos + Vector3.up * bezierWidth;
        upadatePoints[1] = pos;
    }

    protected Vector3 GetBezieorValue(float value, Vector3[] sPoints, Vector3[] uPoints)
    {
        Vector3 a = Vector3.Lerp(sPoints[0], sPoints[1], value);
        Vector3 b = Vector3.Lerp(sPoints[1], uPoints[0], value);
        Vector3 c = Vector3.Lerp(uPoints[0], uPoints[1], value);

        Vector3 d = Vector3.Lerp(a, b, value);
        Vector3 e = Vector3.Lerp(b, c, value);

        Vector3 f = Vector3.Lerp(d, e, value);
        return f;
    }

    protected void Rotate(Vector3 pos)
    {
        var dir = (pos - transform.position).normalized;
        if (dir == Vector3.zero) return;

        transform.rotation = Quaternion.LookRotation(dir);
    }
}
