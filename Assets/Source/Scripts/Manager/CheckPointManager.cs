using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : Singleton<CheckPointManager>
{
    [SerializeField] private StartPoint startPoint;
    [SerializeField] private CheckPoint currentPoint;
    public CheckPoint CurrentPoint
    {
        get
        {
            if (this.currentPoint == null)
            {
                if (this.startPoint == null) Debug.LogWarning("StartPoint is not exist!");
                this.currentPoint = this.startPoint;
            }
            return this.currentPoint;
        }
    }

    public void SavePoint(CheckPoint checkPoint)
    {
        if (checkPoint.IsSaved) return;
        this.currentPoint = checkPoint;
    }
}