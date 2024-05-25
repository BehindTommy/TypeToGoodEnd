using Godot;
using System;

public partial class main : Node2D
{
	private AnimatedSprite2D MC_sprite;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MC_sprite = GetNode<AnimatedSprite2D>("main_character");
		MC_sprite.Play("run");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(delta > 1)
		{
			MC_sprite.Play("run");
		}
	}
}
