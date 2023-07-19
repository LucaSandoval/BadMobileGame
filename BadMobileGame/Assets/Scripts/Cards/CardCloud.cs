using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCloud : MonoBehaviour
{
    List<EquationCard> deck = new List<EquationCard>();
    public GameObject cardPrefab;


    void Start()
    {
        //GenerateCardBatch();
        MakeCard(new EquationNumber(7));
        MakeCard(new MultiplyExpression());
        MakeCard(new AddExpression());
        MakeCard(new EquationColorType(ShapeColor.blue));
        MakeCard(new EquationShapeType(ShapeType.triangle));
    }

    void GenerateCardBatch(int numberOfCards) {
        for (int i = 0; i < numberOfCards; i++) {
            deck.Add(GenerateRandomCard());
        }
    }

    //stubbed... 
    private EquationCard GenerateRandomCard() {
        EquationSymbol symbol;
        //Testing for now...
        symbol = new EquationNumber(7);

        return MakeCard(symbol);
    }

    private EquationCard MakeCard(EquationSymbol symbol) {
        //Spawning in card and initializing it.
        GameObject spawnedCardObject = Instantiate(cardPrefab, GetNewCardSpawnPos(), Quaternion.identity);
        EquationCard card = spawnedCardObject.GetComponent<EquationCard>();
        card.Initialize(symbol, this);
        return card;
    }

    private Vector3 GetNewCardSpawnPos() {
        //stubbed for now... will put it somewhere floaty within in the cloud in a good spot.
        return transform.position;
    }


}
