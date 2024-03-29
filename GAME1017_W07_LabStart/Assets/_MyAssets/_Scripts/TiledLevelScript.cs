using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TiledLevelScript : MonoBehaviour
{
    [SerializeField] private Tilemap[] tileMaps;
    [SerializeField] private TileBase[] tileBases; 
    [SerializeField] private char[] tileKeys;
    [SerializeField] private char[] tileObstacles;

    private int rows; 
    private int cols; 

    // Added fields for the color-changing background functionality.
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Color[] backgroundColors;
    private int currentColorIndex = 0;
    private Coroutine colorChangeCoroutine;

    void Start()
    {
        mainCamera = Camera.main;
        LoadAndSortTileBases();
        LoadLevel();

        // Start the color change coroutine.
        colorChangeCoroutine = StartCoroutine(ChangeBackgroundColor());
    }

    private void LoadLevel()
    {
        try
        {
            GetRowsAndColumns(); // Get rows and columns from the level file.
            
            using (StreamReader reader = new StreamReader("Assets/Level1.txt"))
            {
                string line;
                for (int row = 1; row < rows + 1; row++)
                {
                    line = reader.ReadLine();
                    for (int col = 0; col < cols; col++)
                    {
                        char c = line[col];
                        if (c == '*') continue; // Skip if sky tile.

                        int charIndex = Array.IndexOf(tileKeys, c);
                        if (charIndex == -1) throw new Exception("Index not found.");
                        // Check if tile is obstacle or normal.
                        if (Array.IndexOf(tileObstacles, c) > -1) // Tile is obstacle.
                        {
                            SetTile(0, charIndex, col, row);
                        }
                        else // Tile is normal.
                        {
                            SetTile(1, charIndex, col, row);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    private void SetTile(int tileMapIndex, int charIndex, int col, int row)
    {
        foreach (Tilemap tilemap in tileMaps)
        {
            if (tilemap.HasTile(new Vector3Int(col, -row, 0))) return;
        }
        tileMaps[tileMapIndex].SetTile(new Vector3Int(col, -row, 0), tileBases[charIndex]);
    }

    private void GetRowsAndColumns()
    {
        string[] lines = File.ReadAllLines("Assets/Level1.txt");
        rows = lines.Length;
        cols = lines[0].Length;
    }

    private void LoadAndSortTileBases()
    {
        // Load tile bases dynamically based on the characters in the tileKeys array.
        tileBases = new TileBase[tileKeys.Length];
        for (int i = 0; i < tileKeys.Length; i++)
        {
            string path = "Tiles/" + tileKeys[i]; // Assuming tiles are located in a "Tiles" folder.
            tileBases[i] = Resources.Load<TileBase>(path);
            if (tileBases[i] == null)
            {
                Debug.LogError("Tile " + tileKeys[i] + " not found at path: " + path);
            }
        }
    }

    private int ExtractNumber(string s)
    {
        // Extracts the number from a string, assuming the number is at the end.
        string numberString = "";
        for (int i = s.Length - 1; i >= 0; i--)
        {
            if (char.IsDigit(s[i]))
            {
                numberString = s[i] + numberString;
            }
            else
            {
                break;
            }
        }
        return int.Parse(numberString);
    }

    private IEnumerator ChangeBackgroundColor()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f); // Change color every 5 seconds.
            currentColorIndex = (currentColorIndex + 1) % backgroundColors.Length;
            mainCamera.backgroundColor = backgroundColors[currentColorIndex];
        }
    }
}
