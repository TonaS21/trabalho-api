using Godot;
using System;

public partial class player : CharacterBody2D
{
	public float speed = 300f; //original 200
	
	public AnimationPlayer animationPlayer;
	public Timer timer;
	public ProgressBar Health;
	
	public bool en_range = false;
	public bool en_attack_cooldown = true;
	public bool playerAlive = true;
	public PackedScene SwordSlash = GD.Load<PackedScene>("res://tiro_attack.tscn");

	public int health = 100;
	public int damage;

	public Vector2 mousePos;
	public Vector2 pos;
	public Vector2 direction;
	
	public bool pl_tiroattack_cooldown = true;
	public bool attackfromboss = true;
	public int swordDamage = 10;
	public int tiroDamage = 5;

	public void Movement() {
		Vector2 inputDirection = Input.GetVector("left", "right", "up", "down");
		Velocity = inputDirection * speed;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		Velocity = velocity;
		
		Health = GetNode<ProgressBar>("Health");
		Health.Value = health;
		
		Movement();
		Animations();
		EnemyAttack();
		TiroAttack();
		MoveAndSlide();
	}
	
	public void Animations() {
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		
		//Estar parado
		if (!Input.IsAnythingPressed() && animationPlayer.CurrentAnimation != "Attack1") {
			animationPlayer.Play("Idle");
		}
		
		//Correr
		if (Input.IsActionPressed("right") && animationPlayer.CurrentAnimation != "Attack1") {
			animationPlayer.Play("Run");
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = false;
		} else if(Input.IsActionPressed("left") && animationPlayer.CurrentAnimation != "Attack1") {
			animationPlayer.Play("Run");
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = true;
		} else if(Input.IsActionPressed("up") && animationPlayer.CurrentAnimation != "Attack1") {
			animationPlayer.Play("Run");
		} else if(Input.IsActionPressed("down") && animationPlayer.CurrentAnimation != "Attack1") {
			animationPlayer.Play("Run");
		} else if(Input.IsActionPressed("attack")) {
			animationPlayer.Play("Attack1");
		}
	}

	public async void TiroAttack() {
		if(Input.IsActionJustPressed("tiroattack") && pl_tiroattack_cooldown == true) {
			pl_tiroattack_cooldown = false;
			var tiro = SwordSlash.Instantiate<tiro_attack>();
			mousePos = GetGlobalMousePosition();
			tiro.Position = Position + Position.DirectionTo(mousePos);
			tiro.direction = Position.DirectionTo(mousePos);
			tiro.velocity = 10;
			AddSibling(tiro);
			tiro.Visible = true;
			await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);
			pl_tiroattack_cooldown = true;
		}
	}

	public async void EnemyAttack() {
		if(en_range && en_attack_cooldown == true) {
			timer = GetNode<Timer>("Cooldown");
			if(health - damage > 0) {
				health = health - damage;
				en_attack_cooldown = false;
				timer.Start();
			} else {
				health = 0;
				speed = 0;
				playerAlive = false;
				animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
				animationPlayer.Play("Death");
				await ToSignal(GetTree().CreateTimer(0.6f), SceneTreeTimer.SignalName.Timeout);
				GetTree().ChangeSceneToFile("res://game_over.tscn");
			}
		}
	}

	private void _on_player_hit_box_body_entered(CharacterBody2D body)
	{
		if(body is enemy) {
			damage = 10;
			en_range = true;
		} else if (body is enemy_2) {
			damage = 20;
			en_range = true;
		} 
	}
	
	private void _on_player_hit_box_area_entered(Area2D area)
	{
		if (area is coin) {
			if (health <= 95) {
				health = health + 5;
			} else {
				health = 100;
			}
		} else if (area is diamond) {	
			health = 100;
		} else if (area is boss_attack) {	
			en_range = true;
			damage = 10;
		}
	}	
	
	private void _on_player_hit_box_body_exited(CharacterBody2D body)
	{
		if(body is enemy || body is enemy_2) {
			en_range = false;
		}
	}
	
	private void _on_player_hit_box_area_exited(Area2D area)
	{
		if (area is boss_attack) {	
			en_range = false;
		}
	}
	
	private void _on_cooldown_timeout()
	{
		en_attack_cooldown = true;
	}
	
	public override void _Ready() { }
}


