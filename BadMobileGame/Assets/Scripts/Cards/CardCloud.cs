using System;
using System.Collections.Generic;
using UnityEngine;

public class CardCloud : MonoBehaviour
{
    private List<EquationCard> deck = new List<EquationCard>();
    public GameObject cardPrefab;
    public HandGenerationProfile profile;

    //scriptable objects for objects

    //need at least one math symbol... 
    void Awake()
    {
        deck = new List<EquationCard>();
    }

    public void GenerateCardBatch()
    {
        //For each probability 
        foreach (EquationSymbolProbability prob in profile.SymbolProbabilities)
        {   
            //Get random num...
            int rand = UnityEngine.Random.Range(1, 101);
            int running = 0;
            //For every range probability
            foreach (RangeProbability rangeProb in prob.Probabilities)
            {
                //if the rand is < the running total, then the probability hit...
                if (rand <= running + rangeProb.ProbabilityPct)
                {
                    //make a random amount within the range of a randomly selected symbol of the flagged enums...
                    int amount = UnityEngine.Random.Range(rangeProb.MaxInclusive, rangeProb.MaxInclusive + 1);
                    List<EquationSymbol> equationSymbols = EquationSymbolFactory.Create(prob.Symbol);
                    for (int i = 0; i < amount; i++)
                    {
                        MakeCard(equationSymbols[UnityEngine.Random.Range(0, equationSymbols.Count)]);
                    }
                }
                running += rangeProb.ProbabilityPct;
            }
        }
    }

    private EquationCard MakeCard(EquationSymbol symbol)
    {
        //Spawning in card and initializing it.
        GameObject spawnedCardObject = Instantiate(cardPrefab, GetNewCardSpawnPos(), Quaternion.identity);
        EquationCard card = spawnedCardObject.GetComponent<EquationCard>();
        card.Initialize(symbol, this);
        deck.Add(card);
        return card;
    }

    private Vector3 GetNewCardSpawnPos()
    {
        //stubbed for now... will put it somewhere floaty within in the cloud in a good spot.
        return transform.position;
    }

    public void RemoveCardFromDeck(EquationCard card)
    {
        if (deck.Contains(card)) { deck.Remove(card); }
    }

    public void DestroyAllCardsInDeck()
    {
        if (deck.Count > 0)
        {
            for(int i = deck.Count - 1; i >= 0; i--)
            {
                deck[i].DestroyCard();
            }

            deck.Clear();
        }
    }

}

//Seems a bit hacky, but works...
public static class EquationSymbolFactory
{
    public static List<EquationSymbol> Create(EquationSymbolEnum symbolEnum)
    {
        List<EquationSymbol> symbols = new List<EquationSymbol>();

        foreach (EquationSymbolEnum enumValue in Enum.GetValues(typeof(EquationSymbolEnum)))
        {
            if (symbolEnum.HasFlag(enumValue))
            {
                switch (enumValue)
                {
                    // Handle each flag accordingly...
                    case EquationSymbolEnum.Circle:
                        symbols.Add(new EquationShapeType(ShapeType.circle));
                        break;
                    case EquationSymbolEnum.Square:
                        symbols.Add(new EquationShapeType(ShapeType.square));
                        break;
                    case EquationSymbolEnum.Triangle:
                        symbols.Add(new EquationShapeType(ShapeType.triangle));
                        break;
                    case EquationSymbolEnum.Multiply:
                        symbols.Add(new MultiplyExpression());
                        break;
                    case EquationSymbolEnum.Add:
                        symbols.Add(new AddExpression());
                        break;
                    case EquationSymbolEnum.Subtract:
                        symbols.Add(new SubtractExpression());
                        break;
                    case EquationSymbolEnum.Red:
                        symbols.Add(new EquationColorType(ShapeColor.red));
                        break;
                    case EquationSymbolEnum.Blue:
                        symbols.Add(new EquationColorType(ShapeColor.blue));
                        break;
                    case EquationSymbolEnum.Green:
                        symbols.Add(new EquationColorType(ShapeColor.green));
                        break;
                    case EquationSymbolEnum.One:
                        symbols.Add(new EquationNumber(1));
                        break;
                    case EquationSymbolEnum.Two:
                        symbols.Add(new EquationNumber(2));
                        break;
                    case EquationSymbolEnum.Three:
                        symbols.Add(new EquationNumber(3));
                        break;
                    case EquationSymbolEnum.Four:
                        symbols.Add(new EquationNumber(4));
                        break;
                    case EquationSymbolEnum.Five:
                        symbols.Add(new EquationNumber(5));
                        break;
                    case EquationSymbolEnum.Six:
                        symbols.Add(new EquationNumber(6));
                        break;
                    case EquationSymbolEnum.Seven:
                        symbols.Add(new EquationNumber(7));
                        break;
                    case EquationSymbolEnum.Eight:
                        symbols.Add(new EquationNumber(8));
                        break;
                    case EquationSymbolEnum.Nine:
                        symbols.Add(new EquationNumber(9));
                        break;
                }
            }
        }

        return symbols;
    }
}

