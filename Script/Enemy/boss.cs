using Godot;
using System;

public partial class boss : CharacterBody2D
{
	public float Speed = 125f;

	Vector2 playerPosition;
	Vector2 mobPosition = Vector2.Zero;
	Vector2 targetPosition = Vector2.Zero;
	
	public AnimationPlayer animationPlayer;
	public Timer timer;
	public ProgressBar Health;
	
	public Vector2 direction;
	
	bool player_in_att_zone = false; 
	public bool en_attack_cooldown = true;
	public bool boss_attack_cooldown = true;
	public int enHealth = 350;
	
	public PackedScene BossTiro = GD.Load<PackedScene>("res://boss_attack.tscn");
	
	public override void _PhysicsProcess(double delta)
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		
		Vector2 velocity = Velocity;
		mobPosition = this.Position;
		playerPosition = GetNode<CharacterBody2D>($"../Player").Position;
		targetPosition = (playerPosition - mobPosition).Normalized();

		velocity = Vector2.Zero;
		
		Health = GetNode<ProgressBar>("Health");
		Health.Value = enHealth;

		if (mobPosition.DistanceTo(playerPosition) < 150000 && Speed > 0) {
			velocity = targetPosition;
			animationPlayer.Play("Run");
			BossAttack();
		} else if(mobPosition.DistanceTo(playerPosition) > 150000 && Speed > 0) {
			animationPlayer.Play("Run");
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
		if(body is player) {
			player_in_att_zone = true;
		}
	}
	
	private void _on_boss_hit_box_area_entered(Area2D area)
	{
		player Player = GetTree().Root.GetNode("World").GetNode<player>("Player");
		if(area.Name == "SwordArea2D") {
			if(enHealth - Player.swordDamage > 0) {
				enHealth -= Player.swordDamage;
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

	private void _on_boss_hit_box_area_exited(Node2D body)
	{
		if(body is player) {
			player_in_att_zone = false;
		}
	}

	private void SpawnDiamond()
	{
		var scene = GD.Load<PackedScene>("res://diamond.tscn");
		var diamond = scene.Instantiate<Area2D>();
		AddSibling(diamond);
		diamond.Position = Position;
	}
	
	public async void BossAttack() {
		if(boss_attack_cooldown == true) {
			boss_attack_cooldown = false;
			var Bosss = BossTiro.Instantiate<boss_attack>();
			playerPosition = GetNode<CharacterBody2D>($"../Player").Position;
			Bosss.Position = Position + Position.DirectionTo(playerPosition);
			Bosss.direction = Position.DirectionTo(playerPosition);
			Bosss.Rotation = Bosss.direction.Angle();
			Bosss.velocity = 5;
			AddSibling(Bosss);
			Bosss.Visible = true;
			await ToSignal(GetTree().CreateTimer(0.4f), SceneTreeTimer.SignalName.Timeout);
			boss_attack_cooldown = true;
		}
	}
	
	public async void OnEnemyDeath()
	{
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		enHealth = 0;
		animationPlayer.Play("Death");
		Speed = 0;
		await ToSignal(GetTree().CreateTimer(0.35f), SceneTreeTimer.SignalName.Timeout);
		SpawnDiamond();
		QueueFree();
	}
}
