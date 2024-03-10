using Godot; 
using System;

public partial class PlayerCharacterMove : CharacterBody2D
{
	AnimationTree animationTree;
	bool isMoving;
	Vector2 facingDirection;
	Vector2 velocity = Vector2.Zero; // Current velocity
	float speed = 50f; // Target speed
	float accel = 50f; // Acceleration rate
	float decel = 5; // Deceleration rate, adjust as needed

	public override void _Ready(){
		animationTree = GetNode<AnimationTree>("AnimationTree");
	}

	public override void _Process(double delta){
		ProcessAnimation();
	}

	void ProcessAnimation(){
		animationTree.Set("parameters/conditions/isIdle", !isMoving);
		animationTree.Set("parameters/conditions/isMove", isMoving);
		animationTree.Set("parameters/Idle/blend_position", facingDirection);
		animationTree.Set("parameters/Move/blend_position", facingDirection);
	}

	public override void _PhysicsProcess(double delta){
	Vector2 directionInput = Input.GetVector("left", "right", "up", "down");
	bool inputIsMoving = directionInput != Vector2.Zero;
	
	// Dynamically adjust acceleration based on the current velocity.
	float currentSpeed = velocity.Length();
	float speedRatio = currentSpeed / speed;
	// Adjust acceleration based on speed ratio. This can be tuned for different acceleration curves.
	float dynamicAccel = accel + (0.5f - speedRatio) * (accel * 1f); // Increase accel as the character speeds up

	if (inputIsMoving) {
		velocity = velocity.Lerp(directionInput * speed, dynamicAccel * (float)delta);
		facingDirection = directionInput.Normalized();
	} else {
		// If not actively moving but still has velocity, decelerate
		velocity = velocity.Lerp(Vector2.Zero, decel * (float)delta);
	}
	
	// Determine if character is "moving" based on significant velocity
	isMoving = velocity.Length() > 1.0; // Adjust threshold as needed

	Velocity = velocity;

	MoveAndSlide();

	// Update animation parameters
	ProcessAnimation();
}
}
