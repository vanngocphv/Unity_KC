using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IF_KitchenObjectParent
{
    //Singleton, only allow set inside this class, do not allow set in public
    public static PlayerController Instance { get; private set;}    //Create a instance, only one for one



    //Event variable
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged; // This event only fire when the some counter has been selected
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounterArgs;        // The public object can allow we refer to check the selected counter
    }

    //Pick up event sound effect
    public event EventHandler OnPickUpKitchenObject;


    //Variable
    [SerializeField] private Transform holdingObjectPoint;           // Location holding object in hand
    [SerializeField] private GameObject playerVisual;                // Player Visual game object
    [SerializeField] private GameInputController gameInputController;// Game Input Controller script 
    [SerializeField] private LayerMask counterLayer;                 // Counter layer
    [SerializeField] private float moveSpeed = 5f;                   // move speed
    [HideInInspector] public bool isWalking;

    private KitchenObject kitchenObject;
    private BaseCounter selectedCounter;           // Handle for developer can keep track what counter is selected
    private Animator anim;                          // Animator controller
    //private Vector3 lastDir;                        // Last Direction from player Viewport;
    private float rotateSpeed = 15f;                // rotate speed


    // When the game start, the scene has been loaded, this Awake() will be called
    private void Awake(){
        // Check if the game only has one player, set Instance
        if (Instance != null){
            Debug.LogError("This game has more player inside");
            return;
        }
        Instance = this;
    }


    // Start is called before the first frame update
    private void Start()
    {
        //isWalkingHash = Animator.StringToHash("isWalking");
        anim = playerVisual.GetComponent<Animator>();
        gameInputController.OnInteractAction += GameInput_OnInteractAction;
        gameInputController.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleLookAt();
    }

    private void HandleMovement(){
        //Create a vector2 for store value
        Vector2 inputValue = gameInputController.InputNormalizedVector();
        //Debug.Log(inputValue);

        //Handle set animation walking
        if (inputValue.x != 0 || inputValue.y != 0){
            isWalking = true;
            //anim.SetBool(isWalkingHash, true);
        }
        else{
            isWalking = false;
            //anim.SetBool(isWalkingHash, false);
        }
        //Handle this Vector2 -> direction vector in normalize
        Vector3 inputDir = new Vector3(inputValue.x, 0f, inputValue.y);
        
        //Create a raycast for check if has collider in the front of player
        float playerHeight = 2f;
        float playerSize = 0.7f;
        float moveSpeedDistance = Time.deltaTime * moveSpeed;
        //Create a capsule cast in the front of player
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerSize, inputDir, moveSpeedDistance );

        //Transform position in world space in every frame
        if (!canMove && (inputDir.x != 0  && inputDir.z != 0)){
            //Attemp check if can move to X
            Vector3 inputDirX = new Vector3(inputDir.x, 0, 0).normalized;
            canMove = (inputDir.x < -0.5f || inputDir.x > 0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerSize, inputDirX, moveSpeedDistance);
            if (canMove){
                //set move direction to move x direction
                inputDir = inputDirX;
            }
            else {
                //Attemp check if can move to Z
                Vector3 inputDirZ = new Vector3(0, 0, inputDir.z).normalized;
                canMove = (inputDir.z < -0.5f || inputDir.z > 0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerSize, inputDirZ, moveSpeedDistance);
                if (canMove){
                    //set move direction to move Z direction
                    inputDir = inputDirZ;
                }
            }
        }
        //Rotate gameobject
        if (inputDir != Vector3.zero)
            transform.forward = Vector3.Slerp( transform.forward, inputDir, rotateSpeed * Time.deltaTime);
        
        //Set move when can move
        if (canMove)
            transform.position += inputDir * moveSpeedDistance;

    }
    //Handle where user looking at
    private void HandleLookAt()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        float interactDistance = 2f;
        //Create a vector2 for store value
        //Vector2 inputValue = gameInputController.InputNormalizedVector();
        //Handle this Vector2 -> direction vector in normalize
        //Vector3 inputDir = new Vector3(inputValue.x, 0f, inputValue.y);
        
        //if (inputDir != Vector3.zero){
        //    lastDir = inputDir;
        //}

        //Cast a raycast for check if can interact
        bool canInteract = Physics.Raycast(transform.position, transform.forward, out RaycastHit rayHitInfo, interactDistance, counterLayer);
        //Debug.DrawLine(transform.position, transform.position + transform.forward.normalized, Color.red, interactDistance);
        if (canInteract){
            // Try get component script if when rayHit hit any counter in the front of line sight
            if (rayHitInfo.transform.TryGetComponent(out BaseCounter counter))
            {
                if (selectedCounter != counter){
                    //A counter in view, set the clear counter from try get component for selected
                    SetSelectedCounter(counter);
                }
            }
            else{
                //clear counter, check another;
                SetSelectedCounter(null);
            }
        }
        else {
            //Not hit anything, clear counter
            SetSelectedCounter(null);
        }
    }

    

    private void GameInput_OnInteractAction(object sender, System.EventArgs e){
        //If the game state <> Playing
        if (!MainGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null){
            selectedCounter.Interact(this);
        }
    }

    private void GameInput_OnInteractAlternateAction(object sender, System.EventArgs e){
        //If the game has state <> Playing
        if (!MainGameManager.Instance.IsGamePlaying()) return;

        if (selectedCounter != null){
            selectedCounter.InteractAlternate(this);
        }
    }

    private void SetSelectedCounter(BaseCounter _counter){
        //Set for global selected counter = clearounter, refer to declare clear counter in global variable declare
        this.selectedCounter = _counter;


        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs{
            selectedCounterArgs = this.selectedCounter
        });
    }

    //Get Current Top Point of this current KitchenParent
    public Transform GetCurrentTopPoint(){
        return holdingObjectPoint;
    }
    //Set, get, clear kitchent object
    public KitchenObject GetCurrentKitchenObject(){
        return kitchenObject;
    }
    public void SetKitchenObject(KitchenObject _kitchenObject){
        kitchenObject = _kitchenObject;
        //fire event sound effect if not null
        if (kitchenObject != null){
            OnPickUpKitchenObject?.Invoke(this, EventArgs.Empty);
        }
    }
    public void ClearCurrentKitchenObject(){
        kitchenObject = null;
    }

    //Check if this KitchenParent has object inside
    public bool CheckKitchenObject(){
        return kitchenObject != null;
    }
}
