using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationModel : MVC
{
    public List<Location> locations;

    // Start is called before the first frame update
    void Awake()
    {
        GenerateLocationList();
    }

    public Location FindLocationByName(string name)
    {
        foreach (Location l in locations)
        {
            if (l.locationName == name)
            {
                return l;
            }
        }

        return null;
    }

    public int FindLocationIndex(Location l)
    {
        return locations.IndexOf(l);
    }

    private void GenerateLocationList()
    {
        locations = new List<Location>();

        // Space 1
        Location s1 = new Location("Space 1");
        s1.type = LocationType.City;
        s1.majorLocation = false;
        s1.gate = GateColor.None;
        locations.Add(s1);

        // Space 2
        Location s2 = new Location("Space 2");
        s2.type = LocationType.Sea;
        s2.majorLocation = false;
        s2.gate = GateColor.None;
        locations.Add(s2);

        // Space 3
        Location s3 = new Location("Space 3");
        s3.type = LocationType.Sea;
        s3.majorLocation = false;
        s3.gate = GateColor.None;
        locations.Add(s3);

        // San Francisco
        Location sf = new Location("San Francisco");
        sf.type = LocationType.City;
        sf.majorLocation = true;
        sf.gate = GateColor.Green;
        locations.Add(sf);

        // Space 4
        Location s4 = new Location("Space 4");
        s4.type = LocationType.Wilderness;
        s4.majorLocation = false;
        s4.gate = GateColor.None;
        locations.Add(s4);

        // Space 5
        Location s5 = new Location("Space 5");
        s5.type = LocationType.City;
        s5.majorLocation = false;
        s5.gate = GateColor.None;
        locations.Add(s5);

        // Space 6
        Location s6 = new Location("Space 6");
        s6.type = LocationType.City;
        s6.majorLocation = false;
        s6.gate = GateColor.None;
        locations.Add(s6);

        // Space 7
        Location s7 = new Location("Space 7");
        s7.type = LocationType.City;
        s7.majorLocation = false;
        s7.gate = GateColor.None;
        locations.Add(s7);

        // Arkam
        Location ark = new Location("Arkham");
        ark.type = LocationType.City;
        ark.majorLocation = true;
        ark.gate = GateColor.Red;
        locations.Add(ark);

        // Space 8
        Location s8 = new Location("Space 8");
        s8.type = LocationType.Sea;
        s8.majorLocation = false;
        s8.gate = GateColor.None;
        locations.Add(s8);

        // The Amazon
        Location amazon = new Location("The Amazon");
        amazon.type = LocationType.Wilderness;
        amazon.majorLocation = false;
        amazon.gate = GateColor.None;
        locations.Add(amazon);

        // Buneos Aires
        Location ba = new Location("Buenos Aires");
        ba.type = LocationType.City;
        ba.majorLocation = true;
        ba.gate = GateColor.Blue;
        locations.Add(ba);

        // Space 9
        Location s9 = new Location("Space 9");
        s9.type = LocationType.Wilderness;
        s9.majorLocation = false;
        s9.gate = GateColor.None;
        locations.Add(s9);

        // London
        Location lon = new Location("London");
        lon.type = LocationType.City;
        lon.majorLocation = true;
        lon.gate = GateColor.Blue;
        locations.Add(lon);

        // Rome
        Location rome = new Location("Rome");
        rome.type = LocationType.City;
        rome.majorLocation = true;
        rome.gate = GateColor.Red;
        locations.Add(rome);

        // Space 10
        Location s10 = new Location("Space 10");
        s10.type = LocationType.Wilderness;
        s10.majorLocation = false;
        s10.gate = GateColor.None;
        locations.Add(s10);

        // Space 11
        Location s11 = new Location("Space 11");
        s11.type = LocationType.Sea;
        s11.majorLocation = false;
        s11.gate = GateColor.None;
        locations.Add(s11);

        // Space 12
        Location s12 = new Location("Space 12");
        s12.type = LocationType.Sea;
        s12.majorLocation = false;
        s12.gate = GateColor.None;
        locations.Add(s12);

        // Space 13
        Location s13 = new Location("Space 13");
        s13.type = LocationType.Sea;
        s13.majorLocation = false;
        s13.gate = GateColor.None;
        locations.Add(s13);

        // Space 14
        Location s14 = new Location("Space 14");
        s14.type = LocationType.City;
        s14.majorLocation = false;
        s14.gate = GateColor.None;
        locations.Add(s14);

        // Istanbul
        Location ist = new Location("Istanbul");
        ist.type = LocationType.City;
        ist.majorLocation = true;
        ist.gate = GateColor.Green;
        locations.Add(ist);

        // The Pyramids
        Location pyr = new Location("The Pyramids");
        pyr.type = LocationType.Wilderness;
        pyr.majorLocation = false;
        pyr.gate = GateColor.None;
        locations.Add(pyr);

        // The Heart of Africa
        Location hoa = new Location("The Heart of Africa");
        hoa.type = LocationType.Wilderness;
        hoa.majorLocation = false;
        hoa.gate = GateColor.Red;
        locations.Add(hoa);

        // Space 15
        Location s15 = new Location("Space 15");
        s15.type = LocationType.City;
        s15.majorLocation = false;
        s15.gate = GateColor.None;
        locations.Add(s15);

        // Antarctica
        Location ant = new Location("Antarctica");
        ant.type = LocationType.Sea;
        ant.majorLocation = false;
        ant.gate = GateColor.Blue;
        locations.Add(ant);

        // Space 16
        Location s16 = new Location("Space 16");
        s16.type = LocationType.City;
        s16.majorLocation = false;
        s16.gate = GateColor.None;
        locations.Add(s16);

        // Tunguska
        Location tun = new Location("Tunguska");
        tun.type = LocationType.Wilderness;
        tun.majorLocation = false;
        tun.gate = GateColor.None;
        locations.Add(tun);

        // The Himalayas
        Location him = new Location("The Himalayas");
        him.type = LocationType.Wilderness;
        him.majorLocation = false;
        him.gate = GateColor.Red;
        locations.Add(him);

        // Space 17
        Location s17 = new Location("Space 17");
        s17.type = LocationType.City;
        s17.majorLocation = false;
        s17.gate = GateColor.None;
        locations.Add(s17);

        // Space 18
        Location s18 = new Location("Space 18");
        s18.type = LocationType.Sea;
        s18.majorLocation = false;
        s18.gate = GateColor.None;
        locations.Add(s18);

        // Space 19
        Location s19 = new Location("Space 19");
        s19.type = LocationType.Wilderness;
        s19.majorLocation = false;
        s19.gate = GateColor.None;
        locations.Add(s19);

        // Tokyo
        Location tok = new Location("Tokyo");
        tok.type = LocationType.City;
        tok.majorLocation = true;
        tok.gate = GateColor.Blue;
        locations.Add(tok);

        // Shanghai
        Location sha = new Location("Shanghai");
        sha.type = LocationType.City;
        sha.majorLocation = true;
        sha.gate = GateColor.Red;
        locations.Add(sha);

        // Space 20
        Location s20 = new Location("Space 20");
        s20.type = LocationType.City;
        s20.majorLocation = false;
        s20.gate = GateColor.None;
        locations.Add(s20);

        // Space 21
        Location s21 = new Location("Space 21");
        s21.type = LocationType.Wilderness;
        s21.majorLocation = false;
        s21.gate = GateColor.None;
        locations.Add(s21);

        // Sydney
        Location syd = new Location("Sydney");
        syd.type = LocationType.City;
        syd.majorLocation = true;
        syd.gate = GateColor.Red;
        locations.Add(syd);

        //  Generate Connections

        // Space 1
        s1.connections = new Connection[3];
        s1.connections[0] = new Connection(s19, ConnectionType.Ship);
        s1.connections[1] = new Connection(s4, ConnectionType.Uncharted);
        s1.connections[2] = new Connection(sf, ConnectionType.Ship);

        // Space 2
        s2.connections = new Connection[2];
        s2.connections[0] = new Connection(tok, ConnectionType.Ship);
        s2.connections[1] = new Connection(sf, ConnectionType.Ship);

        // Space 3
        s3.connections = new Connection[2];
        s3.connections[0] = new Connection(syd, ConnectionType.Ship);
        s3.connections[1] = new Connection(ba, ConnectionType.Ship);


        // San Francisco
        sf.connections = new Connection[5];
        sf.connections[0] = new Connection(s2, ConnectionType.Ship);
        sf.connections[1] = new Connection(s1, ConnectionType.Ship);
        sf.connections[2] = new Connection(s5, ConnectionType.Train);
        sf.connections[3] = new Connection(s6, ConnectionType.Train);
        sf.connections[4] = new Connection(s7, ConnectionType.Ship);

        // Space 4
        s4.connections = new Connection[2];
        s4.connections[0] = new Connection(s1, ConnectionType.Uncharted);
        s4.connections[1] = new Connection(s5, ConnectionType.Uncharted);

        // Space 5
        s5.connections = new Connection[3];
        s5.connections[0] = new Connection(sf, ConnectionType.Train);
        s5.connections[1] = new Connection(s4, ConnectionType.Uncharted);
        s5.connections[2] = new Connection(ark, ConnectionType.Train);

        // Space 6
        s6.connections = new Connection[3];
        s6.connections[0] = new Connection(sf, ConnectionType.Train);
        s6.connections[1] = new Connection(ark, ConnectionType.Train);
        s6.connections[2] = new Connection(s7, ConnectionType.Train);

        // Space 7
        s7.connections = new Connection[5];
        s7.connections[0] = new Connection(sf, ConnectionType.Ship);
        s7.connections[1] = new Connection(s6, ConnectionType.Train);
        s7.connections[2] = new Connection(s8, ConnectionType.Ship);
        s7.connections[3] = new Connection(amazon, ConnectionType.Uncharted);
        s7.connections[4] = new Connection(ba, ConnectionType.Ship);

        // Arkham
        ark.connections = new Connection[5];
        ark.connections[0] = new Connection(s5, ConnectionType.Train);
        ark.connections[1] = new Connection(s6, ConnectionType.Train);
        ark.connections[2] = new Connection(s9, ConnectionType.Ship);
        ark.connections[3] = new Connection(lon, ConnectionType.Ship);
        ark.connections[4] = new Connection(s8, ConnectionType.Ship);

        // Space 8
        s8.connections = new Connection[4];
        s8.connections[0] = new Connection(ark, ConnectionType.Ship);
        s8.connections[1] = new Connection(s10, ConnectionType.Ship);
        s8.connections[2] = new Connection(ba, ConnectionType.Ship);
        s8.connections[3] = new Connection(s7, ConnectionType.Ship);

        // The Amazon
        amazon.connections = new Connection[2];
        amazon.connections[0] = new Connection(s7, ConnectionType.Uncharted);
        amazon.connections[1] = new Connection(ba, ConnectionType.Uncharted);

        // Bunos Aires
        ba.connections = new Connection[6];
        ba.connections[0] = new Connection(s3, ConnectionType.Ship);
        ba.connections[1] = new Connection(s7, ConnectionType.Ship);
        ba.connections[2] = new Connection(amazon, ConnectionType.Uncharted);
        ba.connections[3] = new Connection(s8, ConnectionType.Ship);
        ba.connections[4] = new Connection(s11, ConnectionType.Ship);
        ba.connections[5] = new Connection(s12, ConnectionType.Ship);

        // Space 9
        s9.connections = new Connection[1];
        s9.connections[0] = new Connection(ark, ConnectionType.Ship);

        // London
        lon.connections = new Connection[3];
        lon.connections[0] = new Connection(ark, ConnectionType.Ship);
        lon.connections[1] = new Connection(s13, ConnectionType.Ship);
        lon.connections[2] = new Connection(rome, ConnectionType.Ship);

        // Rome
        rome.connections = new Connection[5];
        rome.connections[0] = new Connection(lon, ConnectionType.Ship);
        rome.connections[1] = new Connection(s14, ConnectionType.Train);
        rome.connections[2] = new Connection(ist, ConnectionType.Train);
        rome.connections[3] = new Connection(pyr, ConnectionType.Ship);
        rome.connections[4] = new Connection(s10, ConnectionType.Ship);

        // Space 10
        s10.connections = new Connection[4];
        s10.connections[0] = new Connection(rome, ConnectionType.Ship);
        s10.connections[1] = new Connection(pyr, ConnectionType.Uncharted);
        s10.connections[2] = new Connection(s15, ConnectionType.Ship);
        s10.connections[3] = new Connection(s8, ConnectionType.Ship);

        // Space 11
        s11.connections = new Connection[2];
        s11.connections[0] = new Connection(ba, ConnectionType.Ship);
        s11.connections[1] = new Connection(s15, ConnectionType.Ship);

        // Space 12
        s12.connections = new Connection[2];
        s12.connections[0] = new Connection(ba, ConnectionType.Ship);
        s12.connections[1] = new Connection(ant, ConnectionType.Ship);

        // Space 13
        s13.connections = new Connection[1];
        s13.connections[0] = new Connection(lon, ConnectionType.Ship);

        // Space 14
        s14.connections = new Connection[2];
        s14.connections[0] = new Connection(rome, ConnectionType.Train);
        s14.connections[1] = new Connection(s16, ConnectionType.Train);

        // Istanbul
        ist.connections = new Connection[4];
        ist.connections[0] = new Connection(s16, ConnectionType.Train);
        ist.connections[1] = new Connection(s17, ConnectionType.Train);
        ist.connections[2] = new Connection(pyr, ConnectionType.Train);
        ist.connections[3] = new Connection(rome, ConnectionType.Train);

        // The Pyramids
        pyr.connections = new Connection[4];
        pyr.connections[0] = new Connection(rome, ConnectionType.Ship);
        pyr.connections[1] = new Connection(ist, ConnectionType.Train);
        pyr.connections[2] = new Connection(hoa, ConnectionType.Uncharted);
        pyr.connections[3] = new Connection(s10, ConnectionType.Uncharted);

        // The Heart of Africa
        hoa.connections = new Connection[2];
        hoa.connections[0] = new Connection(pyr, ConnectionType.Uncharted);
        hoa.connections[1] = new Connection(s15, ConnectionType.Uncharted);

        // Space 15
        s15.connections = new Connection[5];
        s15.connections[0] = new Connection(s11, ConnectionType.Ship);
        s15.connections[1] = new Connection(s10, ConnectionType.Ship);
        s15.connections[2] = new Connection(hoa, ConnectionType.Uncharted);
        s15.connections[3] = new Connection(s17, ConnectionType.Ship);
        s15.connections[4] = new Connection(s18, ConnectionType.Ship);

        // Antarctica
        ant.connections = new Connection[2];
        ant.connections[0] = new Connection(s12, ConnectionType.Ship);
        ant.connections[1] = new Connection(syd, ConnectionType.Ship);

        // Space 16
        s16.connections = new Connection[3];
        s16.connections[0] = new Connection(s14, ConnectionType.Train);
        s16.connections[1] = new Connection(tun, ConnectionType.Train);
        s16.connections[2] = new Connection(ist, ConnectionType.Train);

        // Tunguska
        tun.connections = new Connection[2];
        tun.connections[0] = new Connection(s16, ConnectionType.Train);
        tun.connections[1] = new Connection(s19, ConnectionType.Train);

        // The Himalayas
        him.connections = new Connection[2];
        him.connections[0] = new Connection(s17, ConnectionType.Uncharted);
        him.connections[1] = new Connection(sha, ConnectionType.Uncharted);

        // Space 17
        s17.connections = new Connection[5];
        s17.connections[0] = new Connection(ist, ConnectionType.Train);
        s17.connections[1] = new Connection(him, ConnectionType.Uncharted);
        s17.connections[2] = new Connection(sha, ConnectionType.Train);
        s17.connections[3] = new Connection(s20, ConnectionType.Ship);
        s17.connections[4] = new Connection(s15, ConnectionType.Ship);

        // Space 18
        s18.connections = new Connection[2];
        s18.connections[0] = new Connection(s15, ConnectionType.Ship);
        s18.connections[1] = new Connection(syd, ConnectionType.Ship);

        // Space 19
        s19.connections = new Connection[4];
        s19.connections[0] = new Connection(tun, ConnectionType.Train);
        s19.connections[1] = new Connection(s1, ConnectionType.Ship);
        s19.connections[2] = new Connection(tok, ConnectionType.Ship);
        s19.connections[3] = new Connection(sha, ConnectionType.Train);

        // Tokyo
        tok.connections = new Connection[4];
        tok.connections[0] = new Connection(s19, ConnectionType.Ship);
        tok.connections[1] = new Connection(s2, ConnectionType.Ship);
        tok.connections[2] = new Connection(s20, ConnectionType.Ship);
        tok.connections[3] = new Connection(sha, ConnectionType.Ship);

        // Shanghai
        sha.connections = new Connection[5];
        sha.connections[0] = new Connection(s19, ConnectionType.Train);
        sha.connections[1] = new Connection(tok, ConnectionType.Ship);
        sha.connections[2] = new Connection(s20, ConnectionType.Ship);
        sha.connections[3] = new Connection(s17, ConnectionType.Train);
        sha.connections[4] = new Connection(him, ConnectionType.Uncharted);

        // Space 20
        s20.connections = new Connection[4];
        s20.connections[0] = new Connection(sha, ConnectionType.Ship);
        s20.connections[1] = new Connection(tok, ConnectionType.Ship);
        s20.connections[2] = new Connection(syd, ConnectionType.Ship);
        s20.connections[3] = new Connection(s17, ConnectionType.Ship);

        // Space 21
        s21.connections = new Connection[1];
        s21.connections[0] = new Connection(syd, ConnectionType.Uncharted);

        // Sydney
        syd.connections = new Connection[4];
        syd.connections[0] = new Connection(s21, ConnectionType.Uncharted);
        syd.connections[1] = new Connection(s20, ConnectionType.Ship);
        syd.connections[2] = new Connection(s3, ConnectionType.Ship);
        syd.connections[3] = new Connection(ant, ConnectionType.Ship);
    }
}
