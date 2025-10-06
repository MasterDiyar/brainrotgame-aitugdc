using Godot;
using System;

public partial class Feminad : People
{

	public override void TakeDamage(float amount)
	{
		if (Hp < MaxHp / 2) Speed +=4;
		base.TakeDamage(amount);
	}
}
