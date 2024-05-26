using Godot;
using System;

// [Signal]


public partial class main : Node2D
{
	private AnimatedSprite2D MC_sprite;


	[Export]
	private uint move_speed { get; set; } = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MC_sprite = GetNode<AnimatedSprite2D>("main_character");
		MC_sprite.Play("idle");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (move_speed == 0)
		{
			MC_sprite.Play("idle");
		}
		else if (move_speed == 1)
		{
			MC_sprite.Play("walk");
		}
		else if (move_speed == 2)
		{
			MC_sprite.Play("run");
		}
		else
		{
			GD.Print("invalied value of *" + nameof(move_speed) + "*");
			move_speed = 0;
		}
	}

	private void _on_input_set_move_speed(uint newSpeed)
	{
		move_speed = newSpeed;
	}
	// 	private void  _on_button_pressed()
	// 	{
	// 		// EmitSignal(SignalName.set_move_speed, 2);
	// 		move_speed = 2;
	// 	}
}
