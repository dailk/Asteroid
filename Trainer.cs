using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Trainer : MonoBehaviour
{
    public GraphData graphdata;
    public bool create;

    int ab = 0;
    AI_Funcation[,] Generations_A = new AI_Funcation[4,100];
    AI_Funcation[,] Generations_B = new AI_Funcation[4,100];
    List<AI_Funcation[,]> gen;
    //buzz
    //apolo
    //astro
    //challenged

    // Start is called before the first frame update
    void Start() {
        gen = new List<AI_Funcation[,]> {
            Generations_A,
            Generations_B
        };

        if (create) {
            //create AIs
            for (int f = 0; f < 2; f++) {
                for (int d = 0; d < 4; d++) {
                    for (int i = 0; i < 100; i++) {
                        AI_Funcation obj = ScriptableObject.CreateInstance<AI_Funcation>();

                        obj.create();
                        obj.id = i;
                        AssetDatabase.CreateAsset(obj, "Assets/" + f + "/F_AI_" + (drill)d + "_" + i + "_" + f + ".asset");
                        EditorUtility.SetDirty(obj);
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();

                        gen[f][d, i] = obj;
                    }
                }
            }
        } else {
            for (int f = 0; f < 2; f++) {
                for (int d = 0; d < 4; d++) {
                    for (int i = 0; i < 100; i++) {
                        AI_Funcation obj = (AI_Funcation)AssetDatabase.LoadAssetAtPath("Assets/" + f + "/F_AI_" + (drill)d + "_" + i + "_" + f + ".asset", typeof(AI_Funcation));
                        gen[f][d, i] = obj;
                    }
                }
            }

        }

        for (int g = 0; g < 1; g++) {
            int[] divider = new int[4];
            //loop through data
            foreach (tuple rowData in graphdata.data) {

                //ignore zeros
                if (rowData.RateOfPen == 0 && rowData.bitDepth == 0 && rowData.HookLoad == 0 && rowData.DeltaPresure == 0 && rowData.TotalWeight == 0) { break; }

                //calc proformence
                float[] inputs = { rowData.bitDepth, rowData.HookLoad, rowData.DeltaPresure, rowData.TotalWeight };
                for (int i = 0; i < 100; i++) {
                    float result = gen[ab][(int)rowData.DrillBit, i].calc(inputs);
                    gen[ab][(int)rowData.DrillBit, i].acc += Mathf.Abs(rowData.RateOfPen - result);
                    divider[(int)rowData.DrillBit]++;
                }
            }

            for (int d = 0; d < 4; d++) {
                for (int i = 0; i < 100; i++) {
                    gen[ab][d, i].acc = gen[ab][d, i].acc / divider[d];
                }
            }


            //rank top 20
            //        int[,] tempList = new int[4,100];
            //for (int c = 0; c < 100; c++) {
            //    tempList[0, c] = c;
            //    tempList[1, c] = c;
            //    tempList[2, c] = c;
            //    tempList[3, c] = c;
            //}
            //Array.Sort(gen[ab]);
            for (int d = 0; d < 4; d++) {

                for (int i = 0; i < 500; i++) {
                    bool swap = false;
                    for (int s = 1; s < 99; s++) {
                        if (gen[ab][d, s-1].acc > gen[ab][d, s].acc) {
                            Debug.Log(gen[ab][d, s-1].acc + " " + gen[ab][d, s].acc);
                            AI_Funcation temp = gen[ab][d, s];
                            gen[ab][d, s] = gen[ab][d, s - 1];
                            gen[ab][d, s - 1] = temp;
                        }
                    }
                    if (!swap) {
                        break;
                    }
                }
                //Debug.Log("Drill " + (drill)d + " " + gen[ab][d, tempList[d,0]].acc + " " + gen[ab][d, tempList[d, 1]].acc + " " + gen[ab][d, tempList[d, 2]].acc);
            }

            for (int i = 0; i < 4; i++) {
                for (int q = 0; q < 10; q++) {
                    //Debug.Log( g + "Generation " + (drill)i + " "+ gen[ab][i, 0].acc);
                    
                    Debug.Log((drill)i + " " + gen[ab][i, q].acc);
                }
            }

            //next generation
            //for (int d=0;d<4 ;d++) {
            //    if (gen[ab][0, 0].acc > gen[ab][d, 0].acc) {
            //        Debug.Log(gen[ab][0, 0].acc + ">" + gen[ab][d, 0].acc);

            //    } else {
            //        Debug.Log(gen[ab][0, tempList[d, 0]].acc + "<" + gen[ab][d, 0].acc);
            //        gen[(ab + 1) % 2][d, 0].copy(gen[ab][d, tempList[0, 0]]);
            //    } 
            //}

            for (int d = 0; d < 4; d++) {
                gen[(ab + 1) % 2][d, 0].copy(gen[ab][d, 0]);
                for (int i = 1; i < 100; i++) {
                    int p1 = UnityEngine.Random.Range(0, 20);
                    int p2 = UnityEngine.Random.Range(0, 20);
                    gen[(ab + 1) % 2][d, i].merge(gen[ab][d, p1], gen[ab][d, p2]);
                }
            }

            //flip arrays
            ab = (ab + 1) % 2;

        }

        for (int f = 0; f < 2; f++) {
            for (int d = 0; d < 4; d++) {
                for (int i = 0; i < 100; i++) {
                    EditorUtility.SetDirty(gen[f][d, i]);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
        }


    }
}
