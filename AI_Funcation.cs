using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Funcation : ScriptableObject, IComparer<AI_Funcation>
{
    public float[,] primaryLayer = new float[4,16];
    public float[,] secondaryLayer = new float[16,4];
    public float[,] finalLayer = new float[4,4];

    public float acc=0;
    public int id = 0;

    public float calc(float[] inputs) {


        float[] secondInput = new float[16];
        float[] thirdInput = new float[4];
        float output = 0;

        for (int i = 0; i < 4; i++) {
            for (int w = 0; w < 16; w++) {
                secondInput[w] += inputs[i] * primaryLayer[i,w];
            }
        }

        for (int i = 0; i < 16; i++) {
            for (int w = 0; w < 4; w++) {
                thirdInput[w] += secondInput[i] * secondaryLayer[i, w];
            }
        }

        for (int i = 0; i < 4; i++) {
            for (int w = 0; w < 4; w++) {
                output += thirdInput[i] * finalLayer[i, w];
            }
        }

        return output;
    }

    public void create() {
        acc = 0;
        for (int r = 0; r < 4; r++) {
            for (int c = 0; c < 16; c++) {
                primaryLayer[r, c] = Random.Range(-10f, 10f);
            }
        }

        for (int r = 0; r < 16; r++) {
            for (int c = 0; c < 4; c++) {
                secondaryLayer[r, c] = Random.Range(-10f, 10f);
            }
        }

        for (int r = 0; r < 4; r++) {
            for (int c = 0; c < 4; c++) {
                finalLayer[r, c] = Random.Range(-10f, 10f);
            }
        }
    }

    public void copy(AI_Funcation p1) {
        acc = 0;

        for (int r = 0; r < 4; r++) {
            for (int c = 0; c < 16; c++) {
                primaryLayer[r, c] = Random.Range(-10f, 10f);
            }
        }

        for (int r = 0; r < 16; r++) {
            for (int c = 0; c < 4; c++) {
                secondaryLayer[r, c] = Random.Range(-10f, 10f);
            }
        }

        for (int r = 0; r < 4; r++) {
            for (int c = 0; c < 4; c++) {
                finalLayer[r, c] = Random.Range(-10f, 10f);
            }
        }
    }

    public void merge(AI_Funcation p1, AI_Funcation p2) {
        acc = 0;

        for (int r = 0; r < 4; r++) {
            for (int c = 0; c < 16; c++) {
                if (Random.Range(0, 100) == 0) {
                    primaryLayer[r, c] = Random.Range(-10f,10f);
                } else { 
                    primaryLayer[r, c] = (p1.primaryLayer[r, c] + p2.primaryLayer[r, c]) / 2;
                }
            }
        }

        for (int r = 0; r < 16; r++) {
            for (int c = 0; c < 4; c++) {
                if (Random.Range(0, 100) == 0) {
                    secondaryLayer[r, c] = Random.Range(-10f, 10f);
                } else {
                    secondaryLayer[r, c] = (p1.secondaryLayer[r, c] + p2.secondaryLayer[r, c]) / 2;
                }
            }
        }

        for (int r = 0; r < 4; r++) {
            for (int c = 0; c < 4; c++) {
                if (Random.Range(0, 100) == 0) {
                    finalLayer[r, c] = Random.Range(-10f, 10f);
                } else {
                    finalLayer[r, c] = (p1.finalLayer[r, c] + p2.finalLayer[r, c]) / 2;
                }
            }
        }
    }

    public int Compare(AI_Funcation x, AI_Funcation y) {
        return (int)(x.acc - y.acc);
    }
}
