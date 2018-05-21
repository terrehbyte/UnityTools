using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTools;

public class ExampleReadOnly : MonoBehaviour
{
    [ReadOnly]
    public float readonlyNumber;

    [ReadOnly]
    public float[] readonlyArray = new float[5];

    [ReadOnly("CanWrite")]
    public float conditionalWriteNumber;
    public float plainNumber;

    public bool conditionPass;

    private bool CanWrite()
    {
        return conditionPass;
    }

    private void Start()
    {
        readonlyNumber = Random.Range(float.MinValue, float.MaxValue);
        plainNumber = Random.Range(float.MinValue, float.MaxValue);
    }
}
