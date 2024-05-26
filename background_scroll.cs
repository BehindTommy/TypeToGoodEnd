using Godot;
using System;

public partial class background_scroll : Node2D
{
	[Export]
	private uint scroll_speed = 100;

	private Sprite2D layer_2, layer_1;
	private Sprite2D layer_2_D, layer_1_D;

	private Vector2 origin_position;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		layer_1 = GetNode<Sprite2D>("BackgroundLayer1");
		layer_2 = GetNode<Sprite2D>("Background");
		layer_1_D = GetNode<Sprite2D>("BackgroundLayer1_Duplicated");
		layer_2_D = GetNode<Sprite2D>("Background_Duplicated");

		origin_position = layer_1.Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		layer_1.Position += scroll_speed * 2 * (float)delta * Vector2.Left;
		layer_2.Position += scroll_speed * (float)delta * Vector2.Left;
		layer_1_D.Position += scroll_speed * 2 * (float)delta * Vector2.Left;
		layer_2_D.Position += scroll_speed * (float)delta * Vector2.Left;

		if(layer_1.Position.X < (Vector2.Left * origin_position).X)
		{
			layer_1.Position += 2 * origin_position * Vector2.Right;
			layer_1_D.Position += 2 * origin_position * Vector2.Right;
		}
		if(layer_2.Position.X < (Vector2.Left * origin_position).X)
		{
			layer_2.Position += 2 * origin_position * Vector2.Right;
			layer_2_D.Position += 2 * origin_position * Vector2.Right;
		}
	}
}
