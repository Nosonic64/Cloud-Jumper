using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScoreData 
{
    // Class that holds the list of scores
    static public List<Score> scores; 
    public ScoreData()
    {
            scores = new List<Score>();
    }
  
}
