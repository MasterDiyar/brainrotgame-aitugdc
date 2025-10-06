using Godot;
using System;

public partial class Feminad : People
{

	public override void TakeDamage(float amount)
	{
		if (Hp < MaxHp / 2) Speed *= 1.4f;
		base.TakeDamage(amount);
	}
}
