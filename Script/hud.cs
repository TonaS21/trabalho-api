using Godot;
using System;

public partial class hud : Node2D
{
	public int seconds = 0;
	public int min = 0;
	public int Dseconds = 00;
	public int Dmin = 5;
	
	public override void _Process(double delta)
	{
	}
	
	public override void _Ready()
	{
		TimerReset();
	}
	
	private void _on_game_timer_timeout()
	{
		if(seconds == 0) {
			if(min > 0) {
				min -= 1;
				seconds = 60;
			}
		}
		seconds -= 1;
		var Label = GetNode<Label>("Time");
		Label.Text = min.ToString() + ":" + seconds.ToString();
	}
	
	public void TimerReset() {
		seconds = Dseconds;
		min = Dmin;
	}
}
