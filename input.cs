using Godot;
using System;
using System.IO;
using System.Xml;

public partial class input : TextEdit
{
	[Signal]
	public delegate void set_move_speedEventHandler(int newSpeed);
	[Signal]
	public delegate void subtitle_changeEventHandler(string subtitle_text, int subtitle_pin, string effect_type);


	[Export]
	public uint move_speed=0;

	private string input_text;
	private string target_text;
	// private string target_text = Godot.FileAccess.GetFileAsString("res://Asset/target_text.txt");
	private XmlParser parser = new XmlParser();

	private int speed_counter=0;
    
	private int i, targat_pin=0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		parser.Open("res://Asset/target_text.xml");
		while (parser.Read() != Error.FileEof)
		{
			switch(parser.GetNodeType())
			{
				case XmlParser.NodeType.Element:
					GD.Print(parser.GetNodeName());
				break;
				case XmlParser.NodeType.Text:
					GD.Print(parser.GetNodeData());
				break;
				case XmlParser.NodeType.ElementEnd:
					GD.Print("\\" + parser.GetNodeName());
				break;
				default:
				break;
			}
		}

		target_text = "text";
		// // target_text = parser.GetNodeName();

		GrabFocus();
		move_speed = 1;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		input_text = GetWordAtPos(Vector2.Zero);
		for(i = 0; i < input_text.Length; i++)
		{
			// GD.Print(input_text[i] + "-" + target_text[i + targat_pin]);
			// GD.Print(targat_pin);
			if(input_text[i] == target_text[i + targat_pin])
			{
				// GD.Print("correct");
				// this character is right, check the next charracter.
				speed_counter++;
			}
			else
			{
				targat_pin += i;
				EmitSignal(SignalName.subtitle_change, target_text, targat_pin, "WRONG");
				// GD.Print("send WRONG signal");
				// speed_counter only collect the correct characters
				EmitSignal(SignalName.set_move_speed, speed_counter);
				speed_counter = 0;

				input_text.Remove(0);//clear text box
				break;
			}
			if(i == input_text.Length-1)
			{
				targat_pin += i+1;
				EmitSignal(SignalName.subtitle_change, target_text, targat_pin, "CORRECT");
				// GD.Print("send correct signal");
				// speed_counter collect all characters
				EmitSignal(SignalName.set_move_speed, speed_counter);
				speed_counter = 0;

				break;
			}
		}
		if(targat_pin == target_text.Length)
		{
			targat_pin = 0;
			EmitSignal(SignalName.subtitle_change, target_text, targat_pin, "CLEAR");
			// GD.Print("send CLEAR signal");
			// a CORRECT signal has been sent, so speed_counter not needed now.

			// win the game (only this line)
		}

		Clear();
	}
	//get_word_at_pos
	
	// private void  _on_button_pressed()
	// {
	// 	EmitSignal(SignalName.set_move_speed, 2);
	// 	move_speed = 2;
	// 	GD.Print(move_speed);
	// }
}
