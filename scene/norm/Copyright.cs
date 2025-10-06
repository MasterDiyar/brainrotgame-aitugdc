using Godot;
using System;

public partial class Copyright : People
{
	public override void _Process(double delta)
	{
		base._Process(delta);
		Hp += (float)delta;
	}

	public override void TakeDamage(float damage)
	{
		base.TakeDamage(damage-1);
	}
}
