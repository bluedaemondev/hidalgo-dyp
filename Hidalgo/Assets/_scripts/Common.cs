using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common
{
    public static int GetLayerFromMask(LayerMask aMask)
    {
        uint val = (uint)aMask.value;
        if (val == 0)
            return -1;
        for (int i = 0; i < 32; i++)
        {
            if ((val & (1 << i)) != 0)
                return i;
        }
        return -1;
    }

    public static List<int> GetLayersFromMask(LayerMask aMask)
    {
        var result = new List<int>();
        
        uint val = (uint)aMask.value;

        for (int i = 0; i < 32; i++)
        {
            if ((val & (1 << i)) != 0)
                result.Add(i);
        }

        if(result.Count == 0 || val == 0)
        {
            result.Add(-1);
        }

        return result;
    }
}
