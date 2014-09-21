
// Require a character controller to be attached to the same game object
@script RequireComponent(CharacterController)

public var idleAnimation : AnimationClip;
public var walkAnimation : AnimationClip;
public var runAnimation : AnimationClip;
public var jumpPoseAnimation : AnimationClip;
public var fallPoseAnimation : AnimationClip;
public var attackPoseAnimation : AnimationClip;

public var walkMaxAnimationSpeed : float = 0.75;
public var trotMaxAnimationSpeed : float = 1.0;
public var runMaxAnimationSpeed : float = 1.0;
public var jumpAnimationSpeed : float = 1.15;
public var fallAnimationSpeed : float = 1.0;
public var attackAnimationSpeed : float = 1.0;
public var anim : Animator;

public var isHost :boolean;
public var isGrounded: boolean = false;
enum CharacterState
{
	Idle = 0,
	Walking = 1,
	Trotting = 2,
	Running = 3,
	Jumping = 4,
}

public var _characterState : CharacterState;
public var buik : Transform;
// The speed when walking
var walkSpeed = 2.0;
// after trotAfterSeconds of walking we trot with trotSpeed
var trotSpeed = 4.0;
// when pressing "Fire3" button (cmd) we start running
var runSpeed = 6.0;

var inAirControlAcceleration = 3.0;

// How high do we jump when pressing jump and letting go immediately
var jumpHeight = 1;

//counter for loop
var counter = 0;

// The gravity for the character
var gravity = 1.0;
// The gravity in controlled descent mode
var speedSmoothing = 10.0;
var rotateSpeed = 500.0;
var trotAfterSeconds = 3.0;

var canJump = true;

private var jumpRepeatTime = 0.05;
private var jumpTimeout = 0.15;
private var groundedTimeout = 0.25;

// The camera doesnt start following the target immediately but waits for a split second to avoid too much waving around.
private var lockCameraTimer = 0.0;

// The current move direction in x-z
private var moveDirection = Vector2.zero;
// The current vertical speed
private var verticalSpeed = 0.0;
// The current x-z move speed
private var moveSpeed = 0.0;

// The last collision flags returned from controller.Move
private var collisionFlags : CollisionFlags; 

// Are we jumping? (Initiated with jump button and not grounded yet)
private var jumping = false;
private var jumpingReachedApex = false;

// Are we moving backwards (This locks the camera to not do a 180 degree spin)
private var movingBack = false;
// Is the user pressing any keys?
private var isMoving = false;
// When did the user start walking (Used for going into trot after a while)
private var walkTimeStart = 0.0;
// Last time the jump button was clicked down
private var lastJumpButtonTime = -10.0;
// Last time we performed a jump
private var lastJumpTime = -1.0;

// the height we jumped from (Used to determine for how long to apply extra jump power after jumping.)
private var lastJumpStartHeight = 0.0;


private var inAirVelocity = Vector3.zero;

private var lastGroundedTime = 0.0;


public var isControllable = false;

function Awake ()
{
	//THIGNS
	anim = GetComponent(Animator);
}

function start()
{
	Debug.Log("test3");
	if(isHost){
		Debug.Log("test");
	} else {
		Debug.Log("test1");
	}
}

function ApplyJumping ()
{
	// Prevent jumping too fast after each other
	if (lastJumpTime + jumpRepeatTime > Time.time)
		return;

	if (IsGrounded()) {
		// Jump
		// - Only when pressing the button down
		// - With a timeout so you can press the button slightly before landing		
		if (canJump && Time.time < lastJumpButtonTime + jumpTimeout) {
			//verticalSpeed = CalculateJumpVerticalSpeed (jumpHeight);
			anim.SetTrigger("Jump");
			SendMessage("DidJump", SendMessageOptions.DontRequireReceiver);
		}
	}
}


function ApplyGravity ()
{
	if (isControllable)	// don't move player at all if not controllable.
	{
		// Apply gravity
		var jumpButton = Input.GetButton("Jump");
		
		
		// When we reach the apex of the jump we send out a message
		if (jumping && !jumpingReachedApex && verticalSpeed <= 0.0)
		{
			jumpingReachedApex = true;
			SendMessage("DidJumpReachApex", SendMessageOptions.DontRequireReceiver);
		}
	
		if (IsGrounded ())
			verticalSpeed = 0.0;
		else
			verticalSpeed -= gravity * Time.deltaTime;
	}
}

function CalculateJumpVerticalSpeed (targetJumpHeight : float)
{
	// From the jump height and gravity we deduce the upwards speed 
	// for the character to reach at the apex.
	return Mathf.Sqrt(2 * targetJumpHeight * gravity);
}

function DidJump ()
{
	jumping = true;
	jumpingReachedApex = false;
	lastJumpTime = Time.time;
	lastJumpStartHeight = transform.position.y;
	lastJumpButtonTime = -10;
	
	_characterState = CharacterState.Jumping;
}

function Update() {
	if(isHost){
		renderer.material.SetColor("_Color", Color.red);
	} else {
		this.gameObject.renderer.material.color = Color.blue;
	}
	if( isControllable){
		var controller : CharacterController = GetComponent(CharacterController);
		moveSpeed = 0.00;
		if (Input.GetKey (KeyCode.LeftArrow)) {
			moveSpeed = walkSpeed * Time.deltaTime * (-1);
			anim.SetFloat("direction",-1);
			//make the person rotate to the left.
			counter = 0;
		}
		 else if (Input.GetKey (KeyCode.RightArrow)) {
			moveSpeed = walkSpeed * Time.deltaTime * (1);
			//make the person turn to the right.
			anim.SetFloat("direction",1);
			counter = 0;
		} else {
			counter++;
			if(counter > 100){
				//front and center	
			}
		}
		anim.SetFloat("speed",moveSpeed);
		if (canJump == true && isControllable){
			if (Input.GetKey (KeyCode.UpArrow)) {
				anim.SetTrigger('Jump');
				verticalSpeed = 0.3;
				_characterState = CharacterState.Jumping;
				canJump = false;
				isGrounded = false;
				counter = 0;
			}
		} else {
			if(isGrounded){
				canJump = true;
				verticalSpeed = 0.0;
				_characterState = CharacterState.Idle;
			} else {
				counter ++;
				verticalSpeed -= gravity * Time.deltaTime;
				if(counter > 200){
					counter = 0;
					isGrounded = true;
					_characterState = CharacterState.Idle;
					canJump = true;
					verticalSpeed = 0.0;
				}
			}
		}
		verticalSpeed -= gravity * Time.deltaTime;
		var collisionFlags = controller.Move(Vector3(moveSpeed,verticalSpeed,0));
		if(controller.collisionFlags & CollisionFlags.Below){
			isGrounded = true;
			verticalSpeed =0;
		}
	} else {
		if(_characterState == CharacterState.Jumping){
			anim.SetTrigger('Jump');
			_characterState = _characterState.Idle;
		}
	}
	
	//UpdateSmoothedMovementDirection();
	
	//ApplyGravity ();

	// Apply jumping logic
	//ApplyJumping ();
	
	// Calculate actual motion
	//var movement = moveDirection * moveSpeed + Vector3 (0, verticalSpeed, 0) + inAirVelocity;
	//movement *= Time.deltaTime;
	//
	// Move the controller
	//var controller : CharacterController = GetComponent(CharacterController);
	//collisionFlags = controller.Move(movement);
	
	// ANIMATION sector
	/*if(_animation) {
		if(_characterState == CharacterState.Jumping) 
		{
			if(!jumpingReachedApex) {
				_animation[jumpPoseAnimation.name].speed = jumpAnimationSpeed;
				_animation[jumpPoseAnimation.name].wrapMode = WrapMode.ClampForever;
				_animation.CrossFade(jumpPoseAnimation.name);
			} else {
				_animation[jumpPoseAnimation.name].speed = -fallAnimationSpeed;
				_animation[fallPoseAnimation.name].speed = fallAnimationSpeed;
				_animation.CrossFade(fallPoseAnimation.name, 0.1);		
			}
		} 
		else 
		{
			if(this.isControllable && controller.velocity.sqrMagnitude < 0.5) {
				_animation.CrossFade(idleAnimation.name);
                this._characterState = CharacterState.Idle;
			}
			else 
			{
				if(_characterState == CharacterState.Running) {
					_animation[runAnimation.name].speed = runMaxAnimationSpeed;
					if(isControllable) {_animation[runAnimation.name].speed = Mathf.Clamp(controller.velocity.magnitude, 0.0, runMaxAnimationSpeed);}
					_animation.CrossFade(runAnimation.name);	
				}
				else if(_characterState == CharacterState.Trotting) {
                    _animation[runAnimation.name].speed = trotMaxAnimationSpeed;
					if(isControllable) {_animation[walkAnimation.name].speed = Mathf.Clamp(controller.velocity.magnitude, 0.0, trotMaxAnimationSpeed);}
					_animation.CrossFade(walkAnimation.name);	
				}
				else if(_characterState == CharacterState.Walking) {
					_animation[runAnimation.name].speed = walkMaxAnimationSpeed;
					if(isControllable) {_animation[walkAnimation.name].speed = Mathf.Clamp(controller.velocity.magnitude, 0.0, walkMaxAnimationSpeed);}
					_animation.CrossFade(walkAnimation.name);	
				}
				
			}
		}*/
		/*if (isControllable && Input.GetButton("Fire1"))
		{
			animation[attackPoseAnimation.name].AddMixingTransform(buik);
			animation.CrossFade(attackPoseAnimation.name, 0.2);
			animation.CrossFadeQueued(idleAnimation.name, 1.0);
		}
	}*/
	// ANIMATION sector
	
	// Set rotation to the move direction
	/*if (IsGrounded())
	{
		
		transform.rotation = Quaternion.LookRotation(moveDirection);
			
	}	
	else
	{
		var xzMove = movement;
		xzMove.y = 0;
		xzMove.x = 0;
		Debug.Log("test");
		if (xzMove.sqrMagnitude > 0.001)
		{
			transform.rotation = Quaternion.LookRotation(xzMove);
		}
	}	
	
	// We are in jump mode but just became grounded
	if (IsGrounded())
	{
		lastGroundedTime = Time.time;
		inAirVelocity = Vector3.zero;
		if (jumping)
		{
			jumping = false;
			SendMessage("DidLand", SendMessageOptions.DontRequireReceiver);
		}
	}*/
}

function OnControllerColliderHit (hit : ControllerColliderHit )
{
//	Debug.DrawRay(hit.point, hit.normal);
	if (hit.moveDirection.y > 0.01) 
		return;
}

function GetSpeed () {
	return moveSpeed;
}

function IsJumping () {
	return jumping;
}

function IsGrounded () {
	return isGrounded ;
}

function GetDirection () {
	return moveDirection;
}

function IsMovingBackwards () {
	return movingBack;
}

function GetLockCameraTimer () 
{
	return lockCameraTimer;
}

function IsMoving ()  : boolean
{
	return Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5;
}

function HasJumpReachedApex ()
{
	return jumpingReachedApex;
}

function IsGroundedWithTimeout ()
{
	return lastGroundedTime + groundedTimeout > Time.time;
}

function Reset ()
{
	gameObject.tag = "Player";
}

