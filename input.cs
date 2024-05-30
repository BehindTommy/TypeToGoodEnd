using Godot;
using System;
using System.IO;

public partial class input : TextEdit
{
	[Signal]
	public delegate void set_move_speedEventHandler(uint newSpeed);

	[Export]
	public uint move_speed;

	// private uint move_speed = 0;
	private string input_text;
	private string target_text = "事实上";
	private int i, targat_pin=0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GrabFocus();
		move_speed = 1;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		input_text = GetWordAtPos(Vector2.Zero);
		// if(input_text.Substring(1,1) == target_text.Substring(1,1))
		for(i = 0; i < input_text.Length; i++)
		{
			GD.Print(input_text[i] + "-" + target_text[i + targat_pin]);
			GD.Print(targat_pin);
			if(input_text[i] == target_text[i + targat_pin])
			{
				GD.Print("correct");
			}
			else
			{
				GD.Print("wrong");
				targat_pin += i;
				input_text.Remove(0);
				break;
			}
			if(i == input_text.Length-1)
			{
				targat_pin += i+1;
				break;
			}
		}
		if(targat_pin == target_text.Length)
		{
			GD.Print("win");
			targat_pin = 0;
			//win the game
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
