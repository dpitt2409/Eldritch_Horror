using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactModel : MVC
{
    public List<Asset> artifactDeck;
    public List<Asset> artifactPool;

    void Start()
    {
        artifactDeck = new List<Asset>();
        artifactDeck.Add(new TwinSceptersArtifact());

        artifactPool = new List<Asset>(artifactDeck);
    }

    public Asset DrawRandomArtifact()
    {
        if (artifactPool.Count == 0)
            return null;

        int index = Random.Range(0, artifactPool.Count);
        Asset a = artifactPool[index];
        artifactPool.RemoveAt(index);
        return a;
    }

    // Removes Artifact from Pool
    public Asset DrawArtifactByName(string name)
    {
        int index = -1;
        for (int i = 0; i < artifactPool.Count; i++)
        {
            if (artifactPool[i].assetName == name)
            {
                index = i;
            }
        }
        if (index == -1)
            return null;

        Asset a = artifactPool[index];
        artifactPool.RemoveAt(index);
        return a;
    }

    // Does not Remove Artifact from Pool
    public Asset ReferenceArtifactByName(string name)
    {
        int index = -1;
        for (int i = 0; i < artifactDeck.Count; i++)
        {
            if (artifactDeck[i].assetName == name)
            {
                index = i;
            }
        }
        if (index == -1)
            return null;

        Asset a = artifactDeck[index];
        return a;
    }
}
