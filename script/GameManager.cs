using Godot;
using System;

public partial class GameManager : Node2D
{
	public float Money;
	public Label MoneyLabel;
	public override void _Ready()
	{
		Money = 50;
		MoneyLabel = GetNode<Label>("PurseCounter");
	}
	public override void _Process(double delta)
	{
		Money += (float)delta;
		MoneyLabel.Text = "Prompt: "+ ((int)Money).ToString();
	}

	public void leaveGame()
	{
		var looser = GD.Load<PackedScene>("res://scene/loose.tscn").Instantiate();
		looser.GetNode<Label>("Label").Text = GetNode<Mobspawner>("mobspawner").waveLevel.ToString()+" level alived! Molodec";
		GetParent().AddChild(looser);
		
		GetParent().RemoveChild(this);
	}
}
