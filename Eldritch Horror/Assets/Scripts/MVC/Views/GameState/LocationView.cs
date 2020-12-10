using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationView : MVC
{
    [SerializeField]
    private Color preFlipBackgroundColor;
    [SerializeField]
    private Color postFlipBackgroundColor;

    [SerializeField]
    public GameObject objectOnSpace;
    [SerializeField]
    private GameObject monsterOnSpace;
    [SerializeField]
    private GameObject ongoingEffectOnSpace;

    [SerializeField]
    public GameObject[] locationTiles;

    private GameObject gameMap;
    private GameObject gameBackground;

    // Start is called before the first frame update
    void Start()
    {
        gameMap = transform.GetChild(0).GetChild(0).gameObject;
        gameBackground = transform.GetChild(0).GetChild(1).gameObject;

        gameMap.SetActive(false);
        gameBackground.GetComponent<Image>().color = preFlipBackgroundColor;
    }

    public void EnableMap()
    {
        gameMap.SetActive(true);
    }

    public void DisableMap()
    {
        gameMap.SetActive(false);
    }

    public void AncientOneFlipped()
    {
        gameBackground.GetComponent<Image>().color = postFlipBackgroundColor;
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
            go.GetComponent<Image>().sprite = App.Model.gameSpritesModel.gateSprite;
            go.GetComponent<Image>().color = App.Model.mythosSpritesModel.GetGateColor(l.gate);
        }

        foreach (Monster m in l.monstersOnLocation)
        {
            GameObject go = Instantiate(monsterOnSpace, objectParent);
            go.GetComponent<Image>().sprite = m.monsterSprite; // Portrait
            go.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = m.monsterName; // Name
            if (m.damageTaken == 0) // No Taken Damage
            {
                go.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            }
            else // Some Taken Damage
            {
                go.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                go.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "" + m.damageTaken;
            }
            go.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "" + m.toughness; // Toughness
            if (m.tests[0] == TestStat.None) // No Test 1
            {
                go.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Image>().sprite = App.Model.gameSpritesModel.GetTestStatSprite(TestStat.None);
                go.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Text>().text = "";
                go.transform.GetChild(0).GetChild(3).GetChild(2).GetComponent<Text>().text = "";
                go.transform.GetChild(0).GetChild(3).GetChild(3).GetComponent<Image>().sprite = App.Model.gameSpritesModel.GetTestStatSprite(TestStat.None);
            }
            else // Test 1
            {
                go.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<Image>().sprite = App.Model.gameSpritesModel.GetTestStatSprite(m.tests[0]);
                if (m.testMods[0] == 0)
                    go.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Text>().text = "";
                else
                    go.transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<Text>().text = "" + m.testMods[0];
                go.transform.GetChild(0).GetChild(3).GetChild(2).GetComponent<Text>().text = "" + m.horror;
                go.transform.GetChild(0).GetChild(3).GetChild(3).GetComponent<Image>().sprite = App.Model.gameSpritesModel.sanitySprite;
            }
            if (m.tests[1] == TestStat.None) // No Test 2
            {
                go.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Image>().sprite = App.Model.gameSpritesModel.GetTestStatSprite(TestStat.None);
                go.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<Text>().text = "";
                go.transform.GetChild(0).GetChild(4).GetChild(2).GetComponent<Text>().text = "";
                go.transform.GetChild(0).GetChild(4).GetChild(3).GetComponent<Image>().sprite = App.Model.gameSpritesModel.GetTestStatSprite(TestStat.None);
            }
            else // Test 2
            {
                go.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Image>().sprite = App.Model.gameSpritesModel.GetTestStatSprite(m.tests[1]);
                if (m.testMods[1] == 0)
                    go.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<Text>().text = "";
                else
                    go.transform.GetChild(0).GetChild(4).GetChild(1).GetComponent<Text>().text = "" + m.testMods[1];
                go.transform.GetChild(0).GetChild(4).GetChild(2).GetComponent<Text>().text = "" + m.damage;
                go.transform.GetChild(0).GetChild(4).GetChild(3).GetComponent<Image>().sprite = App.Model.gameSpritesModel.healthSprite;
            }
            go.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = m.monsterText; // Monster Text
            if (m.reckoningText == "") // No Reckoning
            {
                go.transform.GetChild(0).GetChild(6).gameObject.SetActive(false);
            }
            else // Reckoning
            {
                go.transform.GetChild(0).GetChild(6).gameObject.SetActive(true);
                go.transform.GetChild(0).GetChild(6).GetChild(0).GetComponent<Text>().text = m.reckoningText;
            }
        }

        foreach (Expedition e in l.expeditionsOnLocation)
        {
            GameObject go = Instantiate(objectOnSpace, objectParent);
            go.GetComponent<Image>().sprite = App.Model.gameSpritesModel.expeditionSprite;
        }

        foreach (Investigator i in l.investigatorsOnLocation)
        {
            GameObject go = Instantiate(objectOnSpace, objectParent);
            go.GetComponent<Image>().sprite = i.investigatorPortrait;

            if (i.delayed)
            {
                go.transform.eulerAngles = new Vector3(go.transform.eulerAngles.x, go.transform.eulerAngles.y, go.transform.eulerAngles.z + 270);
            }
        }

        for (int i = 0; i < l.cluesOnLocation.Count; i++)
        {
            GameObject go = Instantiate(objectOnSpace, objectParent);
            go.GetComponent<Image>().sprite = App.Model.gameSpritesModel.clueTokenSprite;
        }

        foreach (Investigator i in l.deadInvestigatorsOnLocation)
        {
            GameObject go = Instantiate(objectOnSpace, objectParent);
            go.GetComponent<Image>().sprite = i.investigatorPortrait;
            go.GetComponent<Image>().color = Color.grey;
        }

        foreach (OngoingEffect oe in l.ongoingEffectsOnLocation)
        {
            GameObject go = Instantiate(ongoingEffectOnSpace, objectParent);
            go.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = oe.effectTitle;

            if (oe.eldritchTokens == 0)
            {
                go.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                go.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                go.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "" + oe.eldritchTokens;
            }

            go.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = oe.location;
            go.transform.GetChild(0).GetChild(3).GetComponent<Text>().text = oe.effectText;

            if (oe.reckoningText == "")
            {
                go.transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
            }
            else
            {
                go.transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
                go.transform.GetChild(0).GetChild(4).GetChild(0).GetComponent<Text>().text = oe.reckoningText;
            }
        }

        foreach (EldritchToken et in l.eldritchTokensOnLocation)
        {
            GameObject go = Instantiate(objectOnSpace, objectParent);
            go.GetComponent<Image>().sprite = App.Model.gameSpritesModel.eldritchTokenSprite;
        }
    }
}
