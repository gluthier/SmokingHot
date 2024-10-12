using UnityEngine;

public static class Utils
{
    public static int SubOrMin(int startingVal, int substractVal, int minVal)
    {
        int tmpVal = startingVal - substractVal;
        return Mathf.Max(tmpVal, minVal);
    }

    public static int AddOrMax(int startingVal, int addVal, int maxVal)
    {
        int tmpVal = startingVal + addVal;
        return Mathf.Min(tmpVal, maxVal);
    }
}
