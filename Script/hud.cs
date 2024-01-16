using Godot;
using System;

public partial class hud : Node2D
{
	public int seconds = 0;
	public int min = 0;
	public int Dseconds = 00;
	public int Dmin = 5;

	coin Coin = new coin();
	
	public override void _Process(double delta)
	{
		var Label1 = GetNode<Label>("Moedas");
		Label1.Text = Coin.coinsCollected.ToString() + " Coins";
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
		if (seconds < 10) {
			Label.Text = min.ToString() + ":" + "0" + seconds.ToString();
		} else {
			Label.Text = min.ToString() + ":" + seconds.ToString();
		}
		
	}
	
	public void TimerReset() {
		seconds = Dseconds;
		min = Dmin;
	}
}
