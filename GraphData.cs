using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using System.Linq;
using System;

[Serializable]
public struct tuple {
    public float bitDepth;
    public float RateOfPen;
    public float HookLoad;
    public float DeltaPresure;
    public float TotalWeight;
    public drill DrillBit;
}

public enum drill { Buzz=0, Astro=1, Apollo=2, ChallengDriller=3};

public class GraphData : ScriptableObject {
    public tuple[] data = new tuple[300258];

    public tuple Max;
    public tuple Min;

    public void setUp() {
        foreach (tuple row in data) {
            if (row.bitDepth > Max.bitDepth) {
                Max.bitDepth = row.bitDepth;
            } else if (row.bitDepth < Min.bitDepth) { 
                Min.bitDepth = row.bitDepth;
            }

            if (row.RateOfPen > Max.RateOfPen) {
                Max.RateOfPen = row.RateOfPen;
            } else if (row.RateOfPen < Min.RateOfPen) {
                Min.RateOfPen = row.RateOfPen;
            }

            if (row.HookLoad > Max.HookLoad) {
                Max.HookLoad = row.HookLoad;
            } else if (row.HookLoad < Min.HookLoad) {
                Min.HookLoad = row.HookLoad;
            }

            if (row.DeltaPresure > Max.DeltaPresure) {
                Max.DeltaPresure = row.DeltaPresure;
            } else if (row.DeltaPresure < Min.DeltaPresure) {
                Min.DeltaPresure = row.DeltaPresure;
            }

            if (row.TotalWeight > Max.TotalWeight) {
                Max.TotalWeight = row.TotalWeight;
            } else if (row.TotalWeight < Min.TotalWeight) {
                Min.TotalWeight = row.TotalWeight;
            }
        }
    }

    public void ReadFiles() {

        string[] names = { "C:\\Users\\diego\\Downloads\\TheInterstellarAsteroidRush-main\\TheInterstellarAsteroidRush-main\\Asteroids\\Asteroid 1.csv",
                           "C:\\Users\\diego\\Downloads\\TheInterstellarAsteroidRush-main\\TheInterstellarAsteroidRush-main\\Asteroids\\Asteroid 2.csv",
                           "C:\\Users\\diego\\Downloads\\TheInterstellarAsteroidRush-main\\TheInterstellarAsteroidRush-main\\Asteroids\\Asteroid 3.csv",
                           "C:\\Users\\diego\\Downloads\\TheInterstellarAsteroidRush-main\\TheInterstellarAsteroidRush-main\\Asteroids\\Asteroid 4.csv",
                           "C:\\Users\\diego\\Downloads\\TheInterstellarAsteroidRush-main\\TheInterstellarAsteroidRush-main\\Asteroids\\Asteroid 5.csv",
                           "C:\\Users\\diego\\Downloads\\TheInterstellarAsteroidRush-main\\TheInterstellarAsteroidRush-main\\Asteroids\\Asteroid 6.csv",
                           "C:\\Users\\diego\\Downloads\\TheInterstellarAsteroidRush-main\\TheInterstellarAsteroidRush-main\\Asteroids\\Asteroid 7.csv",
                           "C:\\Users\\diego\\Downloads\\TheInterstellarAsteroidRush-main\\TheInterstellarAsteroidRush-main\\Asteroids\\Asteroid 8.csv",
                           "C:\\Users\\diego\\Downloads\\TheInterstellarAsteroidRush-main\\TheInterstellarAsteroidRush-main\\Asteroids\\Asteroid 9.csv",
                           "C:\\Users\\diego\\Downloads\\TheInterstellarAsteroidRush-main\\TheInterstellarAsteroidRush-main\\Asteroids\\Asteroid 10.csv",
                           "C:\\Users\\diego\\Downloads\\TheInterstellarAsteroidRush-main\\TheInterstellarAsteroidRush-main\\Asteroids\\Asteroid 11.csv",
                           "C:\\Users\\diego\\Downloads\\TheInterstellarAsteroidRush-main\\TheInterstellarAsteroidRush-main\\Asteroids\\Asteroid 12.csv",
                           "C:\\Users\\diego\\Downloads\\TheInterstellarAsteroidRush-main\\TheInterstellarAsteroidRush-main\\Asteroids\\Asteroid 13.csv",
                           "C:\\Users\\diego\\Downloads\\TheInterstellarAsteroidRush-main\\TheInterstellarAsteroidRush-main\\Asteroids\\Asteroid 14.csv",
                           "C:\\Users\\diego\\Downloads\\TheInterstellarAsteroidRush-main\\TheInterstellarAsteroidRush-main\\Asteroids\\Asteroid 15.csv",
                           "C:\\Users\\diego\\Downloads\\TheInterstellarAsteroidRush-main\\TheInterstellarAsteroidRush-main\\Asteroids\\Asteroid 16.csv",
                           "C:\\Users\\diego\\Downloads\\TheInterstellarAsteroidRush-main\\TheInterstellarAsteroidRush-main\\Asteroids\\Asteroid 17.csv",
                           "C:\\Users\\diego\\Downloads\\TheInterstellarAsteroidRush-main\\TheInterstellarAsteroidRush-main\\Asteroids\\Asteroid 18.csv",
                           "C:\\Users\\diego\\Downloads\\TheInterstellarAsteroidRush-main\\TheInterstellarAsteroidRush-main\\Asteroids\\Asteroid 19.csv",
                           "C:\\Users\\diego\\Downloads\\TheInterstellarAsteroidRush-main\\TheInterstellarAsteroidRush-main\\Asteroids\\Asteroid 20.csv"};

        using (StreamWriter writer = new StreamWriter("C:\\Users\\diego\\OneDrive\\Desktop\\DataEdited.txt")) {
            foreach (string name in names) {

                StreamReader inp_stm = new StreamReader(name);

                while (!inp_stm.EndOfStream) {
                    // Do Something with the input.
                    string line = inp_stm.ReadLine();


                    bool skip = false;
                    for (int i = 0; i < line.Length - 1; i++) {
                        if (line[i].Equals(',') && line[i] == line[i + 1]) {
                            skip = true;
                            break;
                        }
                    }
                    if (skip) { continue; }

                    writer.Write(line.Replace(",", "\n") + "\n");
                }

                inp_stm.Close();
            }
            writer.Close();
        }

        StreamReader inp_stm2 = new StreamReader("C:\\Users\\diego\\OneDrive\\Desktop\\DataEdited.txt");

        float c;
        int index = 0;
        while (!inp_stm2.EndOfStream) {
            string check = inp_stm2.ReadLine();
            if (!float.TryParse(check, out c)) {
                inp_stm2.ReadLine();
                inp_stm2.ReadLine();
                inp_stm2.ReadLine();
                inp_stm2.ReadLine();
                inp_stm2.ReadLine();
                inp_stm2.ReadLine();
            } else {
                data[index].bitDepth = float.Parse(check);
                data[index].RateOfPen = float.Parse(inp_stm2.ReadLine());
                data[index].HookLoad = float.Parse(inp_stm2.ReadLine());
                data[index].DeltaPresure = float.Parse(inp_stm2.ReadLine());
                data[index].TotalWeight = float.Parse(inp_stm2.ReadLine());
                inp_stm2.ReadLine();

                string type = inp_stm2.ReadLine();
                if (type.Equals("Buzz Drilldrin")) {
                    data[index].DrillBit = drill.Buzz;
                } else if (type.Equals("Apollo")) {
                    data[index].DrillBit = drill.Apollo;
                } else if (type.Equals("AstroBit")) {
                    data[index].DrillBit = drill.Astro;
                } else {
                    data[index].DrillBit = drill.ChallengDriller;
                }
                index++;
            }
        }
    }
}
