using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipToeLogic : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject spawnPlatform;
    private int width = 10;
    private int depth = 15;
    private GameObject[,] feld; 
    private Vector3 spawnPositionOrgin;
    public float heightPlatform = 0.5f;
    public float margin = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        feld = new GameObject[width, depth];
        Vector3 scalePlatform = new Vector3(1.0f, heightPlatform, 1.0f);

        //############## Berechnungen für die Plattfromen(Position, Größe und Abstand) ##############
        float xPlatformSize = spawnPlatform.transform.localScale.x / width;
        float yPlatformSize = spawnPlatform.transform.localScale.z / depth;

        Vector3 xVektor = Vector3.left * xPlatformSize / 2;
        Vector3 zVector = Vector3.back * yPlatformSize / 2;

        scalePlatform.x = xPlatformSize - margin;
        scalePlatform.z = yPlatformSize - margin;

        spawnPositionOrgin = spawnPlatform.transform.position + spawnPlatform.transform.localScale/2 + Vector3.down * scalePlatform.y/2;
        for (int i = 0; i < depth; i++) {
            for (int j = 0; j < width; j++)
            {
                Vector3 offsetX = Vector3.left * j * xPlatformSize;
                Vector3 offsetZ = Vector3.back * i * yPlatformSize;
                feld[j, i] = Instantiate(platformPrefab, spawnPositionOrgin + xVektor + zVector + offsetX + offsetZ, Quaternion.identity);
                feld[j, i].name = j + ":" + i;
                feld[j, i].transform.localScale = scalePlatform;
                //TipToePlatform tipToePlatform = feld[j, i].GetComponent<TipToePlatform>();
                //tipToePlatform.isPath = true;
            }
        }
        //############## Algorothmus für den Pfad ##############
        erstellepfad();

    }
    private void erstellepfad()
    {
        int start = Random.Range(0, width);
        TipToePlatform tipToePlatform = feld[start, 0].GetComponent<TipToePlatform>();
        tipToePlatform.isPath = true;

        int x = start;
        int y = 0;

        while (y < depth-1) { 
            // 0 - 2
            int rangeLinks = 0; //links == 0 : Vorne == 1 : Rechts == 2 
            int rangeRechts = 3;
            //############## kann ich nach rechts? ##############
            // noch im raster?
            if (x + 1 >= width)
            {
                rangeRechts--;
            }
            else { 
                // ist das Feld schon gesetzt?
                tipToePlatform = feld[x + 1, y].GetComponent<TipToePlatform>();
                if (tipToePlatform.isPath) rangeRechts--;
            }
            //############## kann ich nach links? ##############
            // noch im raster?
            if (x - 1 < 0)
            {
                rangeLinks++;
            }
            else
            {
                // ist das Feld schon gesetzt?
                tipToePlatform = feld[x - 1, y].GetComponent<TipToePlatform>();
                if (tipToePlatform.isPath) rangeLinks++;
            }
            Debug.Log("L " + rangeLinks + ": R " + rangeRechts);

            start = Random.Range(rangeLinks, rangeRechts);



            if (start == 0) x--;
            if (start == 1) y++;
            if (start == 2) x++;

            tipToePlatform = feld[x, y].GetComponent<TipToePlatform>();
            tipToePlatform.isPath = true;


        }

    }
    private void clearPlatform()
    {
        for (int i = 0; i < depth; i++)
        {
            for (int j = 0; j < width; j++)
            {
                TipToePlatform tipToePlatform = feld[j, i].GetComponent<TipToePlatform>(); 
                tipToePlatform.isPath = false;
            }
        }
    }
    public void resetGame()
    {
        clearPlatform();
        erstellepfad();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
