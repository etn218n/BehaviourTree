using System.Collections.Generic;
using UnityEngine;

public class PatrolPoints
{
    public List<Vector3> Points { get; private set; }
    public int CurrentIndex     { get; private set; }

    public PatrolPoints()
    {
        Points = new List<Vector3>();

        CurrentIndex = 0;
    }

    public PatrolPoints(int nPoint) : this()
    {
        for (int i = 0; i < nPoint; i++)
        {
            // Temporary value for testing
            Points.Add(RandomizedPoint(-8f, 8f, -8f, 8f));
        }
    }

    public Vector3 GetNext()
    {
        if (CurrentIndex >= Points.Count)
            CurrentIndex = 0;

        return Points[CurrentIndex++];
    }

    public Vector3 RandomizedPoint(float xMin, float xMax, float yMin, float yMax)
    {
        return new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0f);
    }
}
