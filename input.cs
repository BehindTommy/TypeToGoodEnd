using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public partial class input : TextEdit
{
	[Signal]
	public delegate void set_move_speedEventHandler(int newSpeed);
	[Signal]
	public delegate void subtitle_changeEventHandler(string subtitle_text, int subtitle_pin, string effect_type);


	[Export]
	public uint move_speed = 0;

	private string input_text;//the text just inputed by player
	private string target_text;//the line of text need to be completed now
	private XmlParser parser = new();//to read .xml file contains all text
	private List<string> text_contact = new();//to keep all text read from .xml file
	private int text_contact_pin = 0;//to pin where target_text shoud be in text_contact
	private int speed_counter = 0;//how fast character move and map scroll

	private int i, targat_pin = 0;//which character is inputed in target_text

	public override void _Ready()
	{
		GD.Print("start load");
		parser.Open("res://Asset/target_text.xml");
		while (parser.Read() != Error.FileEof)
		{
			switch (parser.GetNodeType())
			{
				case XmlParser.NodeType.Element:
					// GD.Print(parser.GetNodeName());
					if (parser.GetNodeName() == "paragraph")
					{
						// GD.Print(parser.GetAttributeValue(0));
						text_contact.Add(parser.GetAttributeValue(0));
					}
					// GD.Print(parser.GetNodeOffset());
					break;
				case XmlParser.NodeType.Text:
					if (!String.IsNullOrWhiteSpace(parser.GetNodeData()))
					{
						// GD.Print(parser.GetNodeData());
						text_contact.Add(parser.GetNodeData());
					}
					// GD.Print(parser.GetNodeOffset());
					break;
				case XmlParser.NodeType.ElementEnd:
					// GD.Print("\\" + parser.GetNodeName());
					if (parser.GetNodeName() == "paragraph")
					{
						// GD.Print(parser.GetAttributeValue(0));
						text_contact.Add("E.O.P.");
					}
					// GD.Print(parser.GetNodeOffset());
					break;
				default:
					break;
			}
		}
		// GD.Print(text_contact.IndexOf("main02"));
		text_contact_pin = text_contact.IndexOf("main01") + 1;
		// GD.Print(text_contact[text_contact.IndexOf("main01")+1]);
		target_text = text_contact[text_contact_pin];

		//init subtitle
		EmitSignal(SignalName.subtitle_change, target_text, targat_pin, "CLEAR");

		GrabFocus();
		move_speed = 1;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		input_text = GetWordAtPos(Vector2.Zero);
		for (i = 0; i < input_text.Length; i++)
		{
			// GD.Print(input_text[i] + "-" + target_text[i + targat_pin]);
			// GD.Print(targat_pin);
			if (input_text[i] == target_text[i + targat_pin])
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
			if (i == input_text.Length - 1)
			{
				targat_pin += i + 1;
				EmitSignal(SignalName.subtitle_change, target_text, targat_pin, "CORRECT");
				// GD.Print("send correct signal");
				// speed_counter collect all characters
				EmitSignal(SignalName.set_move_speed, speed_counter);
				speed_counter = 0;

				break;
			}
		}
		if (targat_pin == target_text.Length)//this line has completed, change to next line
		{
			targat_pin = 0;
			if(text_contact[++text_contact_pin] == "E.O.P.")//the whole paragraph has completed, change to next paragraph
			{
				text_contact_pin = text_contact.IndexOf("main02") + 1;
			}
			target_text = text_contact[text_contact_pin];
			EmitSignal(SignalName.subtitle_change, target_text, targat_pin, "CLEAR");
			// GD.Print("send CLEAR signal");
			// a CORRECT signal has been sent, so speed_counter not needed now.

		}

		Clear();
	}
}
