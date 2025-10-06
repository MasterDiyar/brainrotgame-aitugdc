using Godot;
using System;

public partial class Koleskak : People
{
	[Export] private PackedScene rebenak;
	Random rnd = new Random();
	private void Spawn()
	{
		
		GetNode<Timer>("Timer").WaitTime = rnd.NextSingle()*8;
		var kat = rebenak.Instantiate<Rebenak>();
		kat.GlobalPosition = GlobalPosition+Vector2.Left*48;
		GetParent().AddChild(kat);
	}
}
