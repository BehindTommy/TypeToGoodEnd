using Godot;
using System;

public partial class Subtitle_Default : Label
{
	private string target_text = Godot.FileAccess.GetFileAsString("res://Asset/target_text.txt");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Text = target_text;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
