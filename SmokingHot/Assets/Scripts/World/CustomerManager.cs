using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public List<Customer> customers;
    public Color playerColor;
    public Color concurrentColor;

    private int previousMarketShare;

    void Start()
    {

    }

    public void InitialColors(float marketShare)
    {
        int roundedMarketShare = Mathf.RoundToInt(marketShare * 10);

        Debug.Log($"Initial market share: {roundedMarketShare}");

        previousMarketShare = roundedMarketShare;

        for (int i = 0; i < customers.Count; ++i)
        {
            if (i < roundedMarketShare)
            {
                customers[i].StartColorTransition(playerColor);
            }
            else
            {
                customers[i].StartColorTransition(concurrentColor);
            }
        }
    }

    public void HandleColors(float marketShare)
    {
        int roundedMarketShare = Mathf.RoundToInt(marketShare * 10);
        int marketShareDelta = roundedMarketShare - previousMarketShare;

        Debug.Log($"Market share: {marketShare}");
        Debug.Log($"Market share (rounded): {roundedMarketShare}");
        Debug.Log($"Market share (delta): {marketShareDelta}");

        if (marketShareDelta > 0)
        {
            for (int i = previousMarketShare; i < roundedMarketShare && i < customers.Count; ++i)
            {
                customers[i].StartColorTransition(playerColor);
            }
        }
        else if (marketShareDelta < 0)
        {
            for (int i = previousMarketShare - 1; i >= roundedMarketShare && i >= 0; --i)
            {
                Debug.Log($"previousMarketShare: {previousMarketShare}");
                customers[i].StartColorTransition(concurrentColor);
            }
        }

        previousMarketShare = roundedMarketShare;
    }
}
