using Godot;
using System;

public partial class GameManager : Node2D
{
	public float Money = 50;
	public Label MoneyLabel;
	public override void _Ready()
	{
		MoneyLabel = GetNode<Label>("PurseCounter");
	}
	public override void _Process(double delta)
	{
		MoneyLabel.Text = "Purse: "+Money.ToString();
	}
}
