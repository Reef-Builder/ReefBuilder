using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class AdaptToScreenSize : MonoBehaviour
{

    public Vector2[] aspectRatios = new Vector2[] { new Vector2(1, 1),
                                                    new Vector2(1, 2),
                                                    new Vector2(1, 3),
                                                    new Vector2(3, 1),
                                                    new Vector2(2, 1),
                                                    new Vector2(5, 4),
                                                    new Vector2(4, 5),
                                                    new Vector2(16, 9),
                                                    new Vector2(9, 16),
                                                    new Vector2(11, 16)};

    private float aspectRatio = -1;

    // Update is called once per frame
    void Update()
    {
        var aspectRatio = GetScreenAspectRatio();

        if (aspectRatio != this.aspectRatio)
        {
            this.aspectRatio = aspectRatio;

            ApplySettings();
        }
    }

    private void ApplySettings()
    {
        //Find index of closest aspect ratio in our array

        var minDiff = float.MaxValue;
        var minIndex = 0;

        for (var i = 0; i < aspectRatios.Length; i++)
        {
            var ratioDiff = Mathf.Abs((aspectRatios[i].x / aspectRatios[i].y) - aspectRatio);

            if (ratioDiff < minDiff)
            {
                minDiff = ratioDiff;
                minIndex = i;
            }
        }

        ApplyChanges(minIndex, aspectRatio);
    }

    private float GetScreenAspectRatio()
    {
        return (Screen.width * 1.0f) / (Screen.height * 1.0f);
    }

    public abstract void ApplyChanges(int aspectRatioIndex, float aspectRatio);
}
