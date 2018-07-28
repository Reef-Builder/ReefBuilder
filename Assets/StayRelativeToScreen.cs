using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayRelativeToScreen : AdaptToScreenSize {

    public Vector3[] rockPositions;
    public Vector3[] leftSeaweedPositions;
    public Vector3[] rightSeaweedPositions;
    public Vector3[] chestPositions;

    public GameObject rocks;
    public GameObject leftSeaweed;
    public GameObject rightSeaweed;
    public GameObject chest;

    public override void ApplyChanges(int aspectRatioIndex, float aspectRatio)
    {
        var rockPos = rockPositions[aspectRatioIndex];
        var leftSWPos = leftSeaweedPositions[aspectRatioIndex];
        var rightSWPos = rightSeaweedPositions[aspectRatioIndex];
        var chestPos = chestPositions[aspectRatioIndex];

        rocks.transform.localPosition = rockPos;
        leftSeaweed.transform.localPosition = leftSWPos;
        rightSeaweed.transform.localPosition = rightSWPos;
        chest.transform.localPosition = chestPos;

        if(aspectRatio >= 1)
        {
           // Camera.main.rect = new Rect(new Vector2(0, 0), new Vector3(aspectRatio, 1));
        } else
        {
           // Camera.main.rect = new Rect(new Vector2(0, 0), new Vector3(1, 1/aspectRatio));
        }
    }
}
