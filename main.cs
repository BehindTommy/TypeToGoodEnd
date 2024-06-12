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
	private double game_progress = 0;
	private string next_map = "default";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MC_sprite = GetNode<AnimatedSprite2D>("main_character");
		MC_sprite.Play("idle");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		game_progress += move_speed/100.0;
		move_speed -= delta/5.0;
		if (move_speed <= 0)
		{
			MC_sprite.Play("idle");
			EmitSignal(SignalName.set_scroll, 0, next_map);
		}
		else if (move_speed < 1)
		{
			MC_sprite.Play("walk");
			EmitSignal(SignalName.set_scroll, 100, next_map);
		}
		else if (move_speed <= 3)
		{
			MC_sprite.Play("run");
			EmitSignal(SignalName.set_scroll, 200, next_map);
		}
		else if(move_speed > 3)
		{
			move_speed = 3;
			MC_sprite.Play("run");
			EmitSignal(SignalName.set_scroll, 200, next_map);
		}
		else
		{
			GD.PrintErr("invalied value of *" + nameof(move_speed) + "*");
			move_speed = 0;
		}

		if(game_progress > 20)
		{
			next_map = "default";
		}
		else if(game_progress > 10)
		{
			next_map = "GrassField";
		}
	}

/* character_count is the number of characters inputed this time
*/
	private void _on_input_set_move_speed(int character_count)
	{
		move_speed += character_count;
		// GD.Print(character_count);
	}
}
