using Godot;
using System;

public partial class enemy : CharacterBody2D
{
	public float Speed = 150f;

	Vector2 playerPosition;
	Vector2 mobPosition = Vector2.Zero;
	Vector2 targetPosition = Vector2.Zero;
	
	public AnimationPlayer animationPlayer;
	public Timer timer;
	
	bool player_in_att_zone = false; 
	public bool en_attack_cooldown = true;
	public int enHealth = 10;

	public override void _PhysicsProcess(double delta)
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		
		Vector2 velocity = Velocity;
		mobPosition = this.Position;
		playerPosition = GetNode<CharacterBody2D>($"../Player").Position;
		targetPosition = (playerPosition - mobPosition).Normalized();

		velocity = Vector2.Zero;

		if (mobPosition.DistanceTo(playerPosition) < 50000 && Speed > 0) {
			velocity = targetPosition;
			if(velocity.X < 0) {
				animationPlayer.Play("Run");
				GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = false;
			} else if (velocity.X > 0) {
				animationPlayer.Play("Run");
				GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = true;
			}
		}

		velocity = velocity * Speed;
		Velocity = velocity;
		
		MoveAndSlide();
	}
	
	private void _on_enemy_hit_box_body_entered(Node2D body)
	{
		if(body.Name == "Player") {
			player_in_att_zone = true;
		}
	}
	
	private void _on_enemy_hit_box_area_entered(Area2D area)
	{
		player Player = GetTree().Root.GetNode("World").GetNode<player>("Player");
		
		if(area.Name == "SwordArea2D") {
			if(enHealth - Player.swordDamage > 0) {
				enHealth -= Player.swordDamage;
				GD.Print(Player.swordDamage);
			} else {
				OnEnemyDeath();
			}
		}
		
		if(area.Name == "TiroAttack") {
			if(enHealth - Player.tiroDamage > 0) {
				enHealth -= Player.tiroDamage;
			} else {
				OnEnemyDeath();
			}
		}
	}

	private void _on_enemy_hit_box_body_exited(Node2D body)
	{
		if(body.Name == "Player") {
			player_in_att_zone = false;
		}
	}

	private void SpawnCoin()
	{
		var scene = GD.Load<PackedScene>("res://coin.tscn");
		var coin = scene.Instantiate<Area2D>();
		AddSibling(coin);
		coin.Position = Position;
	}
	
	public async void OnEnemyDeath()
	{
		player Player = GetTree().Root.GetNode("World").GetNode<player>("Player");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		enHealth = 0;
		Player.en_range = false;
		animationPlayer.Play("Death");
		Speed = 0;
		await ToSignal(GetTree().CreateTimer(0.6f), SceneTreeTimer.SignalName.Timeout);
		SpawnCoin();
		QueueFree();
	}
}
