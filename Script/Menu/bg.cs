using Godot;
using System;

public partial class bg : ParallaxBackground
{
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	   float scrollOffsetDelta = 80;
	   ScrollOffset = new Vector2(ScrollOffset.X - scrollOffsetDelta * (float)delta, 0);
	}
}
