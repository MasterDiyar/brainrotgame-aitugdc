using Godot;
using System;

public partial class TunTunTun : BrainRoted
{
	private AnimatedSprite2D _icon;
	[Export] public override float Cost { get; set; }= 170;
	[Export] public float AttackRate = 0.33f;
	public override void _Ready()
	{
		base._Ready();
		
		_icon = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		
		_attackTimer.Timeout += DealDamage;
		_attackTimer.WaitTime = AttackRate;
	}

	protected override void TimerCheck()
	{
		if (_targets.Count > 0) {
			_attackTimer.Start();
			_icon.Play();
		} else {
			_icon.Stop();
			_attackTimer.Stop();
		}
	}

	public void DealDamage()
	{
		var sharp = GD.Load<PackedScene>("res://scene/br/ljump.tscn").Instantiate<Ljump>();
		sharp.Position = _icon.GlobalPosition+Vector2.Down*25+Vector2.Right*220;
		sharp.Rotation = Mathf.Pi/2;
		sharp.AliveTime = 0.33f;
		sharp.Scale = new Vector2(1, 2);
		sharp.damage = 7;
		GetParent().AddChild(sharp);
		TimerCheck();
	}
}
