using Microsoft.Maps.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinManager : MonoBehaviour
{
    public MapRenderer mapRenderer;

    public GameObject pinPrefab;

    public PinDataCollection collection;

    public GameObject ScrollViewObject;
    public GameObject EntryPrefab;

    List<Entry> entries = new List<Entry>();
    List<PinComponent> mapPins = new List<PinComponent>();

    public static PinManager instance;

    public int highestID = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        LoadAllPinData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeletePin(int pinID)
    {
        foreach(PinData pinData in collection.mapPins)
        {
            if (pinData.pinID == pinID)
            {
                collection.mapPins.Remove(pinData);
                break;
            }
        }
        foreach(PinComponent mapPin in mapPins)
        {
            if (mapPin.pinData.pinID == pinID)
            {
                mapPins.Remove(mapPin);
                Destroy(mapPin.gameObject);
                break;
            }
        }
        foreach(Entry entry in entries)
        {
            if (entry.pinID == pinID)
            {
                entries.Remove(entry);
                Destroy(entry.gameObject);
                break;
            }
        }
    }

    private void OnApplicationQuit()
    {
        SaveAllPinData();
    }

    public void CreatePinAtCenter(string name, string date, string desc)
    {
        double latitude = mapRenderer.Center.LatitudeInDegrees;
        double longitude = mapRenderer.Center.LongitudeInDegrees;

        highestID++;
        PinData pinData = new PinData(latitude, longitude, name, date, desc, highestID);
        collection.mapPins.Add(pinData);

        CreateNewPin(latitude, longitude);
        CreateNewEntry(pinData);
    }
    public void CreateNewPin(double lat, double lon)
    {
        GameObject pinObject = Instantiate(pinPrefab);
        MapPin mapPin = pinObject.GetComponent<MapPin>();
        pinObject.transform.parent = mapRenderer.transform;

        mapPin.Location = new Microsoft.Geospatial.LatLon(lat, lon);
        PinComponent pinComponent = pinObject.GetComponent<PinComponent>();
        if (pinComponent != null)
        {
            mapPins.Add(pinComponent);
        }
    }

    public void SaveAllPinData()
    {
        string pinDataJson = JsonUtility.ToJson(collection);
        //Debug.LogError(pinDataJson);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PinDataList.json", pinDataJson);
    }

    public void LoadAllPinData()
    {
        if (!System.IO.File.Exists(Application.persistentDataPath + "/PinDataList.json")) return;
        //Debug.Log(Application.persistentDataPath);
        string jsonFile = System.IO.File.ReadAllText(Application.persistentDataPath + "/PinDataList.json");
        collection = JsonUtility.FromJson<PinDataCollection>(jsonFile);
        foreach(PinData pinData in collection.mapPins)
        {
            highestID = Mathf.Max(pinData.pinID, highestID);
            CreateNewPin(pinData.latitude, pinData.longitude);
            CreateNewEntry(pinData);
        }
    }
    public void CreateNewEntry(PinData pinData)
    {
        GameObject entryObject = Instantiate(EntryPrefab);
        if (entryObject.TryGetComponent(out Entry entry))
        {
            entryObject.transform.SetParent(ScrollViewObject.transform);
            entry.SetText(pinData.name, pinData.date, pinData.description);
            entry.SetPinID(pinData.pinID);
            entries.Add(entry);
        }
    }
}

[Serializable]
public class PinDataCollection
{
    public List<PinData> mapPins = new List<PinData>();
}

[Serializable]
public class PinData
{
    public double latitude = 0.0;
    public double longitude = 0.0;
    public string name = "";
    public string date = "";
    public string description = "";
    public int pinID = -1;

    public PinData(double lat, double lon, string name, string date, string desc, int pinID)
    {
        this.latitude = lat;
        this.longitude = lon;
        this.name = name;
        this.date = date;
        this.description = desc;
        this.pinID = pinID;
    }
}
