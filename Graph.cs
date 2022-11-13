using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class Graph : MonoBehaviour {

    [SerializeField] private Sprite circle;
    [SerializeField] private RectTransform graphContainer;

    private void Start() {
        //GraphData obj = ScriptableObject.CreateInstance<GraphData>();

        //obj.ReadFiles();

        //AssetDatabase.CreateAsset(obj, "Assets/FullData.asset");
        //EditorUtility.SetDirty(obj);
        //AssetDatabase.SaveAssets();
        //AssetDatabase.Refresh();

        //EditorUtility.FocusProjectWindow();
        //Selection.activeObject = obj;


        GraphData obj = (GraphData)AssetDatabase.LoadAssetAtPath("Assets/FullData.asset", typeof(GraphData));
        //obj.setUp();
        //EditorUtility.SetDirty(obj);
        //AssetDatabase.SaveAssets();
        //AssetDatabase.Refresh();

        foreach (tuple t in obj.data) {
        //    placePoint(new Vector2((t.bitDepth - obj.Min.bitDepth) / (obj.Max.bitDepth - obj.Min.bitDepth), (t.RateOfPen - obj.Min.RateOfPen) / (obj.Max.RateOfPen - obj.Min.RateOfPen)));
        } 
    }

    private void placePoint(Vector2 pos) {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circle;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(pos.x * graphContainer.sizeDelta.x, pos.y * graphContainer.sizeDelta.y);
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.zero;
    }
}
