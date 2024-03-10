using Godot;
using System;

public partial class InteractableManager : Node2D
{

[Export] ShapeCast2D shapeCast;

// Called when the node enters the scene tree for the first time
public override void _Ready(){
	
}

//Called every time. 'delta'is the elapse time since the
public override void _Process(double delta){
	if (Input.IsActionJustPressed("interact")){
		for (int i = 0; i < shapeCast.GetCollisionCount(); i++){
			Node2D collider = (Node2D)shapeCast.GetCollider(i);
			GD.Print(collider.Name);
		}
	}
	
}


}
