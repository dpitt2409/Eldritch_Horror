using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetModel : MVC
{
    public List<Asset> assetDeck;
    public List<Asset> assetPool;
    public List<Asset> assetDiscard;

    void Awake()
    {
        assetDeck = new List<Asset>();
        assetDiscard = new List<Asset>();
        assetDeck.Add(new AxeAsset());
        assetDeck.Add(new ArcaneScholarAsset());
        assetDeck.Add(new LuckyTalismanAsset());
        assetDeck.Add(new PrivateInvestigatorAsset());
        assetDeck.Add(new WhiskeyAsset());
        assetDeck.Add(new PersonalAssistantAsset());
        assetDeck.Add(new BandagesAsset());

        assetPool = new List<Asset>(assetDeck);
    }

    public Asset DrawRandomAsset()
    {
        if (assetPool.Count == 0)
            return null;

        int index = Random.Range(0, assetPool.Count);
        Asset a = assetPool[index];
        assetPool.RemoveAt(index);
        return a;
    }

    // Removes Asset from Pool
    public Asset DrawAssetByName(string name)
    {
        int index = -1;
        for (int i = 0; i < assetPool.Count; i++)
        {
            if (assetPool[i].assetName == name)
            {
                index = i;
            }
        }
        if (index == -1)
            return null;

        Asset a = assetPool[index];
        assetPool.RemoveAt(index);
        return a;
    }

    // Does not Remove Asset from Pool
    public Asset ReferenceAssetByName(string name)
    {
        int index = -1;
        for (int i = 0; i < assetDeck.Count; i++)
        {
            if (assetDeck[i].assetName == name)
            {
                index = i;
            }
        }
        if (index == -1)
            return null;

        Asset a = assetDeck[index];
        return a;
    }

    public void DiscardAsset(Asset a)
    {
        assetDiscard.Add(a);
    }
}
