using Godot;
using System;

public partial class Balerina : BrainRoted
{
	private AnimatedSprite2D _icon;
	[Export] public override float Cost { get; set; }= 100;
	[Export] public float CreateRate = 6.5f;
	public override void _Ready()
	{
		base._Ready();
		
		_icon = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_icon.Play();
		
		_attackTimer.Start();
		_attackTimer.Timeout += DealDamage;
		_attackTimer.WaitTime = CreateRate;
	}

	protected override void TimerCheck()
	{
		
	}

	public void DealDamage()
	{
		var man = GetParent().GetParent<GameManager>();
		man.Money += 40;
	}
}
