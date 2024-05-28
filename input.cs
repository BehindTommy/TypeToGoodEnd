using Godot;
using System;

public partial class input : Control
{
	[Signal]
	public delegate void set_move_speedEventHandler(uint newSpeed);

	[Export]
	public uint move_speed;

    // private uint move_speed = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GrabFocus();
		move_speed = 1;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GD.Print(get_word_at_pos(Vector2.Zero));
	}

	
	// private void  _on_button_pressed()
	// {
	// 	EmitSignal(SignalName.set_move_speed, 2);
	// 	move_speed = 2;
	// 	GD.Print(move_speed);
	// }
}
