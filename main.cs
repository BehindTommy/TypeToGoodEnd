using Godot;
using System;

// [Signal]


public partial class main : Node2D
{
	private AnimatedSprite2D MC_sprite;
	// private int journey_length = 1000;

	[Signal]
	public delegate void set_scrollEventHandler(uint newScrollSpeed, string nextMap);

	[Export]
	private double move_speed = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MC_sprite = GetNode<AnimatedSprite2D>("main_character");
		MC_sprite.Play("idle");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		move_speed -= delta/5.0;
		if (move_speed <= 0)
		{
			MC_sprite.Play("idle");
			EmitSignal(SignalName.set_scroll, 0, "a");
		}
		else if (move_speed < 1)
		{
			MC_sprite.Play("walk");
			EmitSignal(SignalName.set_scroll, 100, "b");
		}
		else if (move_speed <= 3)
		{
			MC_sprite.Play("run");
			EmitSignal(SignalName.set_scroll, 200, "c");
		}
		else if(move_speed > 3)
		{
			move_speed = 3;
			MC_sprite.Play("run");
			EmitSignal(SignalName.set_scroll, 200, "c");
		}
		else
		{
			GD.PrintErr("invalied value of *" + nameof(move_speed) + "*");
			move_speed = 0;
		}
	}

/* character_count is the number of characters inputed this time
*/
	private void _on_input_set_move_speed(int character_count)
	{
		move_speed += character_count;
		GD.Print(character_count);
	}
}
