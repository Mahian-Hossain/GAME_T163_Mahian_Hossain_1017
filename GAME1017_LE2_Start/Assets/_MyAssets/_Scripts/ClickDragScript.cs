using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;



[XmlRoot("GameData")]
public class GameData
{
    public int killCount;

    [XmlArray("TurretPositions")]
    [XmlArrayItem("Position")]
    public List<Vector3> turretPositions;

    public GameData()
    {
        killCount = 0;
        turretPositions = new List<Vector3>();
    }
}

public class ClickDragScript : MonoBehaviour
{
    [SerializeField] private GameObject turretPrefab;
    [SerializeField] private Transform spawnArea;
    
    private bool isDragging = false;
    private Vector2 offset;
    private Rigidbody2D currentlyDraggedObject;
    private List<GameObject> turrets;
    private GameData gameData;

    private void Start()
    {
        turrets = new List<GameObject>();
        gameData = DeserializeFromXml();
    
        SpawnTurrets(gameData.turretPositions);
    }

    void SerializeToXml(GameData data)
    {
        data.killCount++; // Increment kill count
        data.turretPositions.Clear(); // Clear existing turret positions
           
        foreach (GameObject turret in turrets)
        {
            data.turretPositions.Add(turret.transform.position); // Add turret positions to the list
        }


        XmlSerializer serializer = new XmlSerializer(typeof(GameData));
        using (StreamWriter stream = new StreamWriter("gameData.xml"))
        {
            serializer.Serialize(stream, data); // Serialize data to XML
        }
    }

    private GameData DeserializeFromXml()
    {
        if (File.Exists("gameData.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameData));
            using (StreamReader stream = new StreamReader("gameData.xml"))
            {
                return (GameData)serializer.Deserialize(stream); // Deserialize data from XML
            }
        }
        else
        {
            return new GameData(); // Return new GameData if XML file doesn't exist
        }
    }

    private void SpawnTurrets(List<Vector3> turretPositions)
    {
        foreach (Vector3 position in turretPositions)
        {
            GameObject newTurret = Instantiate(turretPrefab, position, Quaternion.identity);
            turrets.Add(newTurret);
        }
    }

    void Update()
    {
        // Input for spawning a new turret
        if (Input.GetKeyDown(KeyCode.T))
        {
            Vector3 spawnPosition = spawnArea.position; // Change this to wherever you want to spawn the turret
            GameObject newTurret = Instantiate(turretPrefab, spawnPosition, Quaternion.identity);
            turrets.Add(newTurret);
        }

        // Input for clearing turrets
        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach (GameObject turret in turrets)
            {
                Destroy(turret);
            }
            turrets.Clear();
            DeleteFile();
        }

        // Input for dragging turrets
        if (Input.GetMouseButtonDown(0) && !isDragging)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.transform.gameObject.tag == "Turret")
            {
                Rigidbody2D rb2d = hit.collider.GetComponent<Rigidbody2D>();
                if (rb2d != null)
                {
                    isDragging = true;
                    currentlyDraggedObject = rb2d;
                    offset = rb2d.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            currentlyDraggedObject = null;
        }

        if (isDragging && currentlyDraggedObject != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentlyDraggedObject.MovePosition(mousePosition + offset);
        }
       
    }

    private void DeleteFile()
    {
        if (File.Exists("gameData.xml"))
        {
            File.Delete("gameData.xml");
            Debug.Log("XML file deleted.");
        }
    }

    private void OnApplicationQuit()
    {
        SerializeToXml(gameData);
    }
}
