using System;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Transform pointPrefab;
    public Transform[] points;
    [Range(10, 100)] public int resolution = 10;
    public GraphFunctionName function;

    static GraphFunction[] functions = { SineFunction, Sine2DFunction, MultiSineFunction };
    private void Awake()
    {
        float step = 2f / resolution;
        Vector3 scale = Vector3.one * step;
        Vector3 position;
        position.y = 0;
        position.z = 0f;
        points = new Transform[resolution * resolution];

        for (int i = 0, z = 0; z < resolution; z++)
        {
            position.z = (z + 0.5f) * step - 1f;
            for (int x = 0; x < resolution; x++, i++)
            {
                Transform point = Instantiate(pointPrefab, transform, false);
                position.x = (x + 0.5f) * step - 1f;
                point.localPosition = position;
                point.localScale = scale;
                points[i] = point;
            }
        }
    }

    private void Update()
    {
        float t = Time.time;

        for (int i = 0; i < points.Length; i++)
        {
            Transform point = points[i];
            Vector3 position = point.localPosition;
            position.y = functions[(int)function](position.x, position.z, t);
            point.localPosition = position;
        }
    }

    private const float pi = Mathf.PI;

    static float SineFunction(float x, float z, float t)
    {
        return Mathf.Sin(Mathf.PI * (x + t));
    }

    static float MultiSineFunction(float x, float z, float t)
    {
        float y = Mathf.Sin(pi * (x + t));
        y += Mathf.Sin(2f * pi * (x + 2f * t)) / 2f;
        y *= 2f / 3f;
        return y;
    }

    static float Sine2DFunction(float x, float z, float t)
    {
        float y = Mathf.Sin(pi * (x + t));
        y += Mathf.Sin(pi * (z + t));
        y *= 0.5f;
        return y;
    }
}
