using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Godot;
using Godot.Collections;


public partial class Card : Control
{
    public Color colour;
	public string colourName;
	public string number;

	public Card(Color colour, string colourName, string number)
    {
        this.colour = colour;
		this.colourName = colourName;
		this.number = number;
    }
}


// func _on_card_submit() -> void:
// 	var submitted_card = globals.hand[get_node("%Hand_Options").selected]
// 	#print(globals.discard[globals.discard.size() - 1].colour)
// 	#print(submitted_card.colour)
// 	if (submitted_card.colour == globals.discard[globals.discard.size() - 1].colour):
// 		discard_card(get_node("%Hand_Options").selected);
// 		%Notice.set_text("");
// 	elif (submitted_card.number == globals.discard[globals.discard.size() - 1].number):
// 		discard_card(get_node("%Hand_Options").selected);
// 		%Notice.set_text("");
// 	else:
// 		%Notice.set_text("fuck no");
// 	post_turn();
	

// func setup_hand():
// 	get_node("%Hand_Options").clear();
// 	#print(globals.hand);
// 	for i in globals.hand:
// 		# turn colour to string
// 		var colour_str = ""
// 		match i.colour:
// 			Color("RED"):
// 				colour_str = "Red"
// 			Color("GREEN"):
// 				colour_str = "Green"
// 			Color("BLUE"):
// 				colour_str = "Blue"
// 			Color("YELLOW"):
// 				colour_str = "Yellow"
// 		get_node("%Hand_Options").add_item(colour_str + " " + i.number);
		

// func discard_card(id):
// 	globals.discard.append(globals.hand[id]);
// 	globals.hand.erase(globals.hand[id]);

// func post_turn():
// 	setup_hand()
// 	show_discard()


// func _on_draw_card() -> void:
// 	globals.hand.append(globals.deck[0])
// 	globals.deck.erase (globals.deck[0])
// 	setup_hand();
