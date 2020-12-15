using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish
{
    private FishSize fishSize;
    private string fishName;
    [TextArea(15, 20)]
    private string description;
    private Sprite backpackIcon;

    public Fish(FishSize _fishSize, string _fishName, string _description, Sprite _backpackIcon)
    {
        fishName = _fishName;
        fishSize = _fishSize;
        description = _description;
        backpackIcon = _backpackIcon;
    }

    public string GetFishName()
    {
        return this.fishName;
    }

    public string GetDescriptionName()
    {
        return this.description;
    }

    public int GetFishStrength()
    {
        switch (fishSize)
        {
            case FishSize.Tiny:
                return 1;
            case FishSize.Small:
                return 3;
            case FishSize.Medium:
                return 5;
            case FishSize.Large:
                return 7;
            default:
                return 1;
        }
    }
}