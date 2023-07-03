using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEditor;

using UnityEngine;

namespace Vanilla.Geocodes
{

    public class MojitenMapSubdivider : MonoBehaviour
    {

        public RectTransform _canvas;

        public GameObject cellPrefab;

        public int Longitude_Columns = 16; // Number of divisions horizontally
        public int Latitude_Rows     = 9;  // Number of divisions vertically

        public int defaultPrecision = 1;


//        void Awake() => SpawnGrid(transform,
//                                  defaultPrecision);


//        void Start() => DivideCell(transform as RectTransform,
//                                   defaultPrecision); // Start recursive division with precision 2


//        void Awake() => SpawnGrid(_canvas,
//                                  defaultPrecision);


        async Task Start()
        {
            await SpawnGrid(_canvas,
                      defaultPrecision);
            
            await DivideCell(_canvas,
                       defaultPrecision,
                       0,
                       -1.0f,
                       1.0f,
                       -1.0f,
                       1.0f);
        }


        //    void Start()
//    {
//        SpawnGrid(transform, 2); // Start recursive spawning with precision 2
//        DivideCell(transform as RectTransform, 2, 0); // Start recursive division with precision 2
//    }


        async Task SpawnGrid(Transform parent,
                       int precision)
        {
            if (precision <= 0 || !Application.isPlaying) return;

            for (int i = 0;
                 i < Longitude_Columns * Latitude_Rows;
                 i++)
            {
                Transform newCell = Instantiate(cellPrefab,
                                                parent).transform;

                await Task.Yield();
                
                SpawnGrid(newCell,
                          precision - 1);
            }
        }


    async Task DivideCell(RectTransform cell, int precision, int depth, float globalWidthMin, float globalWidthMax, float globalHeightMin, float globalHeightMax)
    {
        if (!Application.isPlaying) return;
        
        if (precision <= 0)
        {
            // No need for further subdivision.
            for (var i = 0; i < cell.childCount; i++)
            {
                cell.GetChild(i).gameObject.SetActive(false);
            }
            return;
        }

        float cellPixelWidth = cell.rect.width / Longitude_Columns;
        float cellPixelHeight = cell.rect.height / Latitude_Rows;
        int childIndex = 0;

        for (int row = Latitude_Rows - 1; row >= 0; row--) 
        {
            for (int column = 0; column < Longitude_Columns; column++) 
            {
                if (childIndex >= cell.childCount)
                {
                    Debug.LogError("Not enough children in " + cell.name);
                    return;
                }
                
                RectTransform subCell = cell.GetChild(childIndex++) as RectTransform;
                MojitenMapCell mojitenMapCell = subCell.GetComponent<MojitenMapCell>();

                mojitenMapCell.depth = depth;
                mojitenMapCell.column = column;
                mojitenMapCell.row = row;

                var widthRange = globalWidthMax - globalWidthMin;
                var heightRange = globalHeightMax - globalHeightMin;

                mojitenMapCell.WidthMin  = globalWidthMin  + widthRange  * column       / Longitude_Columns;
                mojitenMapCell.WidthMax  = globalWidthMin  + widthRange  * (column + 1) / Longitude_Columns;
                mojitenMapCell.HeightMin = globalHeightMin + heightRange * row          / Latitude_Rows;
                mojitenMapCell.HeightMax = globalHeightMin + heightRange * (row + 1)    / Latitude_Rows;
                
                mojitenMapCell.LatitudeMin = (float) (mojitenMapCell.HeightMin * Mojiten2.LATITUDE_MAX);
                mojitenMapCell.LatitudeMax = (float) (mojitenMapCell.HeightMax * Mojiten2.LATITUDE_MAX);
                
                mojitenMapCell.LongitudeMin = (float) (mojitenMapCell.WidthMin * Mojiten2.LONGITUDE_MAX);
                mojitenMapCell.LongitudeMax = (float) (mojitenMapCell.WidthMax * Mojiten2.LONGITUDE_MAX);

                subCell.gameObject.SetActive(true);
                subCell.anchorMin = new Vector2(0.5f, 0.5f);
                subCell.anchorMax = new Vector2(0.5f, 0.5f);
                subCell.sizeDelta = new Vector2(cellPixelWidth, cellPixelHeight);
                subCell.anchoredPosition = new Vector2(cellPixelWidth * (column - Longitude_Columns * 0.5f) + cellPixelWidth * 0.5f,
                                                       cellPixelHeight * (row - Latitude_Rows * 0.5f) + cellPixelHeight * 0.5f);

                await Task.Yield();
                
                // Divide the sub-cell
                DivideCell(subCell, precision - 1, depth + 1, mojitenMapCell.WidthMin, mojitenMapCell.WidthMax, mojitenMapCell.HeightMin, mojitenMapCell.HeightMax);
            }
        }

        // Set remaining children inactive
        while (childIndex < cell.childCount)
        {
            cell.GetChild(childIndex++).gameObject.SetActive(false);
        }
    }


    }

}