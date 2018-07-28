using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AdaptableGrid : AdaptToScreenSize
{
    public Vector2[] cellSizes = new Vector2[] {    new Vector2(180, 180),
                                                    new Vector2(280, 280),
                                                    new Vector2(350, 350),
                                                    new Vector2(100, 100),
                                                    new Vector2(130, 130),
                                                    new Vector2(145, 145),
                                                    new Vector2(180, 180),
                                                    new Vector2(140, 140),
                                                    new Vector2(300, 220),
                                                    new Vector2(230, 230)};

    private GridLayoutGroup layout;

    // Use this for initialization
    void Start()
    {
        layout = GetComponent<GridLayoutGroup>();
    }

    public override void ApplyChanges(int aspectRatioIndex, float aspectRatio)
    {
        var cellSizeToUse = cellSizes[aspectRatioIndex];

        layout.cellSize = cellSizeToUse;
    }
}
