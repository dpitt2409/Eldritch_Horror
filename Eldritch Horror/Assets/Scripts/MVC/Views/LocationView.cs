using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationView : MVC
{
    [SerializeField]
    public GameObject objectOnSpace;

    [SerializeField]
    public GameObject[] locationTiles;

    private GameObject gameMap;

    // Start is called before the first frame update
    void Start()
    {
        gameMap = transform.GetChild(0).GetChild(0).gameObject;

        gameMap.SetActive(false);
    }

    public void EnableMap()
    {
        gameMap.SetActive(true);

    }

    public void DisableMap()
    {
        gameMap.SetActive(false);
    }

    public void LocationUpdated(Location l)
    {
        GameObject tile = locationTiles[App.Model.locationModel.FindLocationIndex(l)];

        Transform objectParent = tile.transform.GetChild(1);
        foreach (Transform child in objectParent)
        {
            Destroy(child.gameObject);
        }

        if (l.activeGate)
        {
            GameObject go = Instantiate(objectOnSpace, objectParent);
            go.GetComponent<Image>().sprite = App.Model.spriteModel.gateSprite;
            go.GetComponent<Image>().color = App.Model.spriteModel.GetGateColor(l.gate);
        }

        foreach (Monster m in l.monstersOnLocation)
        {
            GameObject go = Instantiate(objectOnSpace, objectParent);
            go.GetComponent<Image>().sprite = m.monsterSprite;
        }

        foreach (Expedition e in l.expeditionsOnLocation)
        {
            GameObject go = Instantiate(objectOnSpace, objectParent);
            go.GetComponent<Image>().sprite = App.Model.spriteModel.expeditionSprite;
        }

        foreach (Investigator i in l.investigatorsOnLocation)
        {
            GameObject go = Instantiate(objectOnSpace, objectParent);
            go.GetComponent<Image>().sprite = i.investigatorPortrait;
        }

        for (int i = 0; i < l.cluesOnLocation.Count; i++)
        {
            GameObject go = Instantiate(objectOnSpace, objectParent);
            go.GetComponent<Image>().sprite = App.Model.spriteModel.clueTokenSprite;
        }

        foreach (Investigator i in l.deadInvestigatorsOnLocation)
        {
            GameObject go = Instantiate(objectOnSpace, objectParent);
            go.GetComponent<Image>().sprite = i.investigatorPortrait;
            go.GetComponent<Image>().color = Color.grey;
        }

        foreach (OngoingEffect oe in l.ongoingEffectsOnLocation)
        {
            GameObject go = Instantiate(objectOnSpace, objectParent); // Change to different object with a special tooltip
            go.GetComponent<Image>().sprite = App.Model.spriteModel.rumorSprite;
        }
    }
}
