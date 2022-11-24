using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static Movement instance;

    public float speed, speedMultiplier;
    [HideInInspector] public Vector2 dir;

    float p;

    [SerializeField] Rigidbody2D body;
    [SerializeField] AnimationCurve speedCurve;

    public bool control;

    public IInteractable Interactable { get; set; }

    void Awake()
    {
        if(instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (control)
        {
            #region MOVEMENT
            // Handles the movement includinsg sprinting
            //Checks if the user can still move
            float x = Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0f && Mathf.Abs(Input.GetAxisRaw("Vertical")) == 0 ? Input.GetAxisRaw("Horizontal") : 0f;
            float y = Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0f && Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 0 ? Input.GetAxisRaw("Vertical") : 0f;

            dir = new Vector2(x, y).normalized * speed;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                p += Time.deltaTime;
                float g = Mathf.Lerp(1, speedMultiplier, speedCurve.Evaluate(p));
                body.MovePosition(body.position + (dir * g) * Time.deltaTime);
            }
            else if (!Input.GetKey(KeyCode.LeftShift))
            {
                p = 0;
                body.MovePosition(body.position + dir * Time.deltaTime);
            }
            #endregion

            if (Input.GetKeyDown(KeyCode.E))
            {
                Interactable?.Interact(this);
            }
        }

    }
}
