using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class MovementController : MonoBehaviour {
    // Velocity related components
    private Vector2 moveDir = Vector2.zero;
    [SerializeField] private float playerSpeed = 5f;

    // Common Functions
    private CommonFunctions commonFunctions;

    private void Awake() {
        commonFunctions = GameObject.Find("Common Functions Container").GetComponent<CommonFunctions>();
    }

    private void Update() {
        if (!commonFunctions.editMode) {
            PlayerMovement();
        }
    }

    // Functions for movement of the Player -
    private void PlayerMovement() {

        Vector2 moveDir = MovementInputNormalized();
        float moveDistance = playerSpeed * Time.deltaTime;

        moveDir = ChangeMovementIfWall(moveDir);
        transform.position += new Vector3(moveDir.x, moveDir.y, 0) * moveDistance;
    }

    public Vector2 MovementInputNormalized() {
        moveDir = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W)) {
            moveDir.y += 1;
        }

        if (Input.GetKey(KeyCode.S)) {
            moveDir.y -= 1;
        }

        if (Input.GetKey(KeyCode.D)) {
            moveDir.x += 1;
        }

        if (Input.GetKey(KeyCode.A)) {
            moveDir.x -= 1;
        }

        return moveDir.normalized;
    }

    private bool IsCollidingWithWall(RaycastHit2D[] hits2D) {
        bool isColliding = false;

        foreach(RaycastHit2D hit2D in hits2D) {
            if (hit2D.collider.CompareTag("Wall")) {
                isColliding = true;
                break; 
            }
        }

        return isColliding;
    }

    private Vector2 ChangeMovementIfWall(Vector2 moveDir) {
        Vector3 raycastPosition = transform.position;
        Vector3 raycastScale = transform.localScale;

        Vector2 moveDirX = new Vector2(moveDir.x, 0);
        float raycastAngleX = 0;

        Vector2 moveDirY = new Vector2(0, moveDir.y);
        float raycastAngleY = 90;

        float moveDistance = Time.deltaTime * playerSpeed;

        RaycastHit2D[] characterHitsX = Physics2D.BoxCastAll(raycastPosition, raycastScale, raycastAngleX, moveDirX, moveDistance);
        RaycastHit2D[] characterHitsY = Physics2D.BoxCastAll(raycastPosition, raycastScale, raycastAngleY, moveDirY, moveDistance);

        bool wallAtX = IsCollidingWithWall(characterHitsX);
        bool wallAtY = IsCollidingWithWall(characterHitsY);

        if (wallAtX && wallAtY) {
            return Vector2.zero;
        } else if (wallAtX && !wallAtY) {
            return moveDirY.normalized;
        } else if (!wallAtX && wallAtY) {
            return moveDirX.normalized;
        } else {
            return moveDir;
        }
    }

    // A function to find a GameObject with some tag in children of given parent
    public GameObject FindWithTag (GameObject parent, string tag) {
        GameObject requiredChild = null;

        foreach (Transform transform in parent.transform) {
            if (transform.CompareTag(tag)) {
                requiredChild = transform.gameObject;
                break;
            }
        }

        return requiredChild;
    }
}