using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class EquationSymbolProbability
{
    public EquationSymbolEnum Symbol;
    public List<RangeProbability> Probabilities;
}


[CreateAssetMenu(fileName = "HandGenerationProfile", menuName = "ScriptableObjects/Hand Generation/Profile")]
public class HandGenerationProfile : ScriptableObject
{
    // Use a list for serialization since Dictionaries can't be serialized by Unity
    public List<EquationSymbolProbability> SymbolProbabilities;

   public Dictionary<EquationSymbolEnum, List<RangeProbability>> ShapeProbabilityMap =>
        SymbolProbabilities.ToDictionary(sp => sp.Symbol, sp => sp.Probabilities);
}

