using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Vanilla.Geocodes
{
    public class MojitenMapPoint : MonoBehaviour
    {

        public RectTransform _inputPin;
        public RectTransform _cellPin;

        public Text hashText;

        private Canvas        _canvas;
//        private RectTransform _canvasRect;
        
        [SerializeField]
        private Vector2 _inputLatLong = Vector2.zero;
        public Vector2 InputLatLong
        {
            get => _inputLatLong;
            set
            {
                if (Mathf.Abs(_inputLatLong.x - value.x) < Mathf.Epsilon &&
                    Mathf.Abs(_inputLatLong.y - value.y) < Mathf.Epsilon) return;

                _inputLatLong = value;

                HandleLatLongChange();
            }
        }

        public float latCellSize;
        public float longCellSize;

        public float latCellHalf;
        public float longCellHalf;
        
        public int    outputRow;
        public int    outputColumn;
        public string outputHash;
        
        public int precision = 4;

        public Vector2 outputLatLong;
        
        public  Vector2 cellBoundsMin;
        public  Vector2 cellBoundsMax;

        public float cellPixelWidth;
        public float cellPixelHeight;
        
        public float cellPixelWidthDebug;
        public float cellPixelHeightDebug;

        public Vector2 cheatSheet;

        
        void Awake()
        {
            _canvas     = _inputPin.GetComponentInParent<Canvas>();
//            _canvasRect = (RectTransform) _canvas.transform;

            var latTotal  = Mojiten2.LATITUDE_MAX  - Mojiten2.LATITUDE_MIN;
            var longTotal = Mojiten2.LONGITUDE_MAX - Mojiten2.LONGITUDE_MIN;

            Debug.Log(latTotal);
            Debug.Log(longTotal);
            


//            cellPixelWidth  = _canvas.renderingDisplaySize.x / Mojiten2.GridSizeLon;
//            cellPixelHeight = _canvas.renderingDisplaySize.y / Mojiten2.GridSizeLat;

            latCellSize  = (float) (latTotal  / Mojiten2.Latitude_Rows);
            longCellSize = (float) (longTotal / Mojiten2.Longitude_Columns);

            latCellHalf = latCellSize / 2;
            longCellHalf = longCellSize / 2;

            HandleLatLongChange();
        }


        void Update()
        {
            if (Input.GetMouseButton(0)) HandleMouseInput();
        }


        private void HandleMouseInput()
        {
            var worldMousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 relativeMousePoint = _canvas.transform.InverseTransformPoint(worldMousePoint);
            
//            Vector2 mouseInput = _canvas.transform.InverseTransformPoint(Input.mousePosition);

//            Debug.Log(relativeMousePoint);
            
//            mouseInput.x -= _canvasRect.sizeDelta.x * 0.5f;
//            mouseInput.y -= _canvasRect.sizeDelta.y * 0.5f;
            
            InputLatLong = relativeMousePoint.MapPointToLatLong(_canvas);
        }


        void HandleLatLongChange()
        {
//            Debug.Log("HandleLatLongChange");
            
            var (row, column, geocode) = Mojiten2.Encode(InputLatLong.y,
                                                         InputLatLong.x,
                                                         precision);

            outputRow    = row;
            outputColumn = column;
            outputHash   = geocode;

            var (latitude, longitude) = Mojiten2.Decode(outputHash);

            outputLatLong = new Vector2((float) longitude,
                                        (float) latitude);

            cellBoundsMin = new Vector2(outputLatLong.x - longCellHalf,
                                        outputLatLong.y - latCellHalf);
            
            cellBoundsMax = new Vector2(outputLatLong.x + longCellHalf,
                                        outputLatLong.y + latCellHalf);

            Debug.Log($"Did that work? Longitude Cell Size check!\n[{cellBoundsMax.x - cellBoundsMin.x}] should be the same as [{longCellSize}]");
            Debug.Log($"Did that work? Latitude Cell Size check!\n[{cellBoundsMax.y  - cellBoundsMin.y}] should be the same as [{latCellSize}]");

            _inputPin.anchoredPosition = InputLatLong.LatLongToMapPoint(_canvas);
            _cellPin.anchoredPosition  = outputLatLong.LatLongToMapPoint(_canvas);

            cellPixelWidth  = _canvas.renderingDisplaySize.x / Mojiten2.Longitude_Columns;
            cellPixelHeight = _canvas.renderingDisplaySize.y / Mojiten2.Latitude_Rows;

//            cellPixelWidthDebug  = _canvas.renderingDisplaySize.x / Mojiten2.Longitude_Columns;
//            cellPixelHeightDebug = _canvas.renderingDisplaySize.y / Mojiten2.Latitude_Rows;

//            cellPixelWidth  = _canvas.renderingDisplaySize.x / (Mojiten2.Longitude_Columns ^ precision);
//            cellPixelHeight = _canvas.renderingDisplaySize.y / (Mojiten2.Latitude_Rows     ^ precision);
            
            cellPixelWidthDebug  = _canvas.renderingDisplaySize.x;
            cellPixelHeightDebug = _canvas.renderingDisplaySize.y;

            for (var i = 0;
                 i < precision;
                 i++)
            {
                cellPixelWidthDebug  /= Mojiten2.Longitude_Columns;
                cellPixelHeightDebug /= Mojiten2.Latitude_Rows;
            }
//
////            cellPixelWidthPrecise  = cellPixelWidth  / precision;
////            cellPixelHeightPrecise = cellPixelHeight / precision;
//            
            _cellPin.sizeDelta = new Vector2(cellPixelWidthDebug,
                                             cellPixelHeightDebug);

            hashText.text = geocode;
        }

    }
}
