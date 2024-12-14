using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public Customer customerReference;
    public Color playerColor;
    public Color concurrentColor;
    public AudioClip winCustomerSound;
    public AudioClip looseCustomerSound;
    private AudioSource audioSource;

    private List<Customer> customers = new List<Customer>();
    public float margin;
    private int numRow = 10;
    private int numCol = 10;
    private int previousMarketShare;
    private Transform initialTransform;

    void Awake()
    {
        initialTransform = customerReference.transform;
    
        for (int row = 0; row < numRow; ++row)
        {
            for (int col = 0; col < numCol; ++col)
            {
                Customer customer = (row == 0 && col == 0) 
                    ? customerReference 
                    : Instantiate(customerReference, initialTransform.position + new Vector3(col * margin, 0, -row * margin), initialTransform.rotation, this.transform);

                customers.Add(customer);
            }
        }
    }

    void Start()
    {
        // Ensure there is an AudioSource component attached
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void InitialColors(float marketShare)
    {
        int roundedMarketShare = Mathf.RoundToInt(marketShare * 100);

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

    public void HandleMarketShare(float marketShare)
    {
        int roundedMarketShare = Mathf.RoundToInt(marketShare * 100);
        int marketShareDelta = roundedMarketShare - previousMarketShare;

        if (marketShareDelta > 0)
        {
            HandleCustomerWin(previousMarketShare, roundedMarketShare, marketShareDelta);
        }
        else if (marketShareDelta < 0)
        {
            HandleCustomerLoss(previousMarketShare, roundedMarketShare, marketShareDelta);
        }

        previousMarketShare = roundedMarketShare;
    }

    private void HandleCustomerLoss(int previousMarketShare, int roundedMarketShare, int marketShareDelta)
    {
        audioSource.PlayOneShot(looseCustomerSound);

        for (int i = previousMarketShare - 1; i >= roundedMarketShare && i >= 0; --i)
        {
            customers[i].Jump();
            customers[i].StartColorTransition(concurrentColor);
        }
    }

    private void HandleCustomerWin(int previousMarketShare, int roundedMarketShare, int marketShareDelta)
    {
        audioSource.PlayOneShot(winCustomerSound);

        for (int i = previousMarketShare; i < roundedMarketShare && i < customers.Count; ++i)
        {
            customers[i].Jump();
            customers[i].StartColorTransition(playerColor);
        }        
    }
}
