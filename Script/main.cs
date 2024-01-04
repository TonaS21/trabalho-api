using Godot;
using System;

public partial class main : Node2D
{
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}
	
	private void _on_play_pressed()
	{
		GetTree().ChangeSceneToFile("res://world.tscn");
	}

	private void _on_quit_pressed()
	{
		GetTree().Quit();
	}
}






