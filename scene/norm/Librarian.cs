using Godot;
using System;

public partial class Librarian : People
{
	public bool takingDamage = false;
	public override void Entered(Area2D area)
	{
		takingDamage = !takingDamage;
		if (area.IsInGroup("Unit") && takingDamage && area.GetParent() is BrainRoted roted)
		{
			roted.TakeDamage(Damage);  
			Position += new Vector2(5, 0);
		}
        
	}
}
