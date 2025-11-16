using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

using System.Threading.Tasks;

public partial class UnoGame : Node
{
	public System.Collections.Generic.Dictionary<string, Color> ColourCodes =
        new()
        {
            {"RED", new Color(255, 0, 0)},
			{"GREEN", new Color(0, 255, 0)},
			{"YELLOW", new Color(255, 255, 0)},
			{"BLUE", new Color(0, 0, 255)},
        };
	
	const int HANDSIZE = 7;
	public List<Card> discard;
	public List<Card> deck;
	public List<Card> hand;
	public static UnoGame Instance;
    public override async void _Ready()
    {
		
        StartIfNotStarted();
    }

	private void StartIfNotStarted()
    {
        if(Instance == null){
			Instance = this;

            if (Networking.Instance.IsHost)
            {
				hand = new List<Card>();
				discard = new List<Card>();
				CreateDeck();
				StartDiscard();
            }
		}
		return;
    }

	public void DrawCard(bool forClient=false)
    {
        if(!Networking.Instance.IsHost)
			return;

		hand.Add(deck[0]);
		deck.RemoveAt(0);

		RefreshHand();
	}
	public Card GetTopMostCardFromList(List<Card> cards)
    {
        return cards[cards.Count - 1];
    }
	public void RefreshHand()
    {
		var options = ((OptionButton)GetNode("%Hand_Options"));
		options.Clear();

		foreach(var card in hand)
        {
			var colour_str = card.colourName;

			options.AddItem(colour_str + " " + card.number);
        }
    }
	public void StartDiscard()
    {
		Discard(0);
    }
	public void Discard(int index)
    {
        discard.Add(deck[index]);
		discard.RemoveAt(index);

		var latestDiscardCard = GetTopMostCardFromList(discard);
		GetNode("%Colour").Set("Color", latestDiscardCard.colour);
		GetNode("%Number").Set("Text", latestDiscardCard.number);
    }
	public void CreateDeck()
    {
		var generator = new RandomNumberGenerator();

		var tempCards = new List<Card>();
		// 9 colors
		// 8 (2 for each color)
		var max = 9 * 8;
		
		var keys = ColourCodes.Keys.ToList();
		for(int i = 0; i < max; i++)
        {
			ColourCodes.TryGetValue(keys[i % 8 / 2], out var color);
			var new_card = new Card(color, keys[i% 8 / 2], (Math.Floor(i/8.0)+ 1).ToString());
			tempCards.Add(new_card);
        }

		deck = new List<Card>();
		for(int i = 0; i < tempCards.Count; i++)
        {
			var index = generator.RandiRange(0, tempCards.Count - 1);
			var card = tempCards[index];
			tempCards.RemoveAt(index);
			deck.Add(card);      
        }

		for(int i = 0; i < HANDSIZE * 2; i++)
        {
			DrawCard(i % 2 == 0 ? true: false);
        }
    }
}