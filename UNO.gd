extends Control
var colour_codes = [Color("RED"),Color("GREEN"),Color("YELLOW"),Color("BLUE"), "!"]
const HANDSIZE = 7


# Called when the node enters the scene tree for the first time.

class uno_card:
	var colour;
	var number;


func _ready() -> void:
	create_deck();
	globals.deck.shuffle();
	deal_cards();
	start_discard();
	show_discard();
	setup_hand();
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	
	pass

func create_deck():
	for i in range(9*8+0):
		# gay
		# deck.append(str(floor(i/8)+ 1) + colour_codes[i % 8 / 2]);
		var new_card = uno_card.new()
		new_card.colour = colour_codes[i % 8 / 2];
		new_card.number = str(floor(i/8)+ 1);
		globals.deck.append(new_card);
	# print(globals.deck);

func deal_cards():
	for i in range(HANDSIZE*2):
		match i % 2:
			0:
				globals.hand.append(globals.deck[0]);
				globals.deck.erase(globals.deck[0]);
			1:
				globals.opp_hand.append(globals.deck[0]);
				globals.deck.erase(globals.deck[0]);

func start_discard():
	globals.discard.append(globals.deck[0]);
	globals.deck.erase(0);

func show_discard():
	get_node("%Colour").set_color(globals.discard[globals.discard.size() - 1].colour);
	get_node("%Number").set_text(globals.discard[globals.discard.size() - 1].number);
	pass
	# split the discard pile's top card code
	# get_node("%Colour").Color = 


func _on_card_submit() -> void:
	var submitted_card = globals.hand[get_node("%Hand_Options").selected]
	if (submitted_card.colour == globals.discard[globals.discard.size() - 1].colour):
		pass
	elif (submitted_card.number == globals.discard[globals.discard.size() - 1].number):
		pass
	

func setup_hand():
	print(globals.hand);
	for i in globals.hand:
		# turn colour to string
		var colour_str = ""
		match i.colour:
			Color("RED"):
				colour_str = "Red"
			Color("GREEN"):
				colour_str = "Green"
			Color("BLUE"):
				colour_str = "Blue"
			Color("YELLOW"):
				colour_str = "Yellow"
		get_node("%Hand_Options").add_item(colour_str + " " + i.number);
		
