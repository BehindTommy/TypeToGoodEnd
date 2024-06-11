using Godot;
using System;

public partial class Subtitle_Default : Label
{
	Label Complete_effect;

	int complete_pin = 0;
	double wrong_flag = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Complete_effect = GetNode<Label>("Subtitle_Completed");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(wrong_flag > 0)
		{
			Complete_effect.Text = Text[..(complete_pin+1)];
			wrong_flag -= delta;
		}
		else
		{
			Complete_effect.Text = Text[..complete_pin];
			wrong_flag = 0;
		}
	}

/*
effect_type:
CORRECT - when the game input get a correct input
WRONG   - when the game get a wrong input
CLEAR   - when this line of text is over and should display next line
*/
	private void _on_input_subtitle_change(string subtitle_text, int subtitle_pin, string effect_type)
	{
		// GD.Print(target_text);
		Text = subtitle_text;
		GD.Print(subtitle_text);

		complete_pin = subtitle_pin;

		switch(effect_type)
		{
			case "CORRECT":
				// Complete_effect.Text = Text[..complete_pin];
				break;
			case "WRONG":
				wrong_flag = 0.1;
				// Complete_effect.Text = Text[..(complete_pin+1)];
				break;
			case "CLEAR":
				break;
			default:
				GD.PrintErr("receiving invalid effect_type");
				break;
		}
	}
}
