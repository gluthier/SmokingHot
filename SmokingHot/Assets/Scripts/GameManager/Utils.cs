using UnityEngine;
using Unity.Mathematics;

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


    public static string GetDisplayableNum(float num)
    {
        string numDisplay = "";

        if (num >= 100000 ||
            num <= -100000 ||
            IsNumDisplayedInteger(num))
        {
            numDisplay = ((int)num).ToString();
        }
        else
        {
            numDisplay = num.ToString("F2");

            if (numDisplay.Contains('.'))
            {
                numDisplay = numDisplay.TrimEnd('0');
            }
        }

        return numDisplay;
    }

    private static bool IsNumDisplayedInteger(float num)
    {
        float parseNum = num;
        float.TryParse(num.ToString("F2"), out parseNum);

        // test if float is an integer
        return parseNum == math.floor(parseNum);
    }
}
