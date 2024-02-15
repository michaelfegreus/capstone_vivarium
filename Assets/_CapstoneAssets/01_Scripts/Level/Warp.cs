using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {

	[Header("Set either an object to warp to, or direct coordinates.")]
	[Tooltip("Set an object for the Player to warp to. This script will prefer warping to the object over coordinates.")]
	public Transform destinationObject;
    [Tooltip("Offset the warp position by the destination's local forward direction. DOES NOT affect direct Destination Coordinates warps -- only Object warps.")]
    public float warpOffset = 3;
    [Tooltip("Set direct coordinates for the Player to warp to. Coordinates will get overwritten if an object is set above.")]
	public Vector2 destinationCoordinates;
    

	void Start(){
		// Overwrite destinationCoordinates if there is a destinationObject in the editor.
		if(destinationObject != null){
            Vector3 destinationObjectCoordinatesAndOffset = destinationObject.transform.position + destinationObject.transform.up * warpOffset;
            destinationCoordinates = new Vector2 (destinationObjectCoordinatesAndOffset.x, destinationObjectCoordinatesAndOffset.y);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
        if (gameObject.activeInHierarchy)
        {
            if (col.tag == ("Player"))
            {
                StartCoroutine(WarpAtEndOfFrame(col.gameObject));

                

                /* OLD
                thePlayer.GetComponent<scr_player_manager> ().Warp (destinationWarp.position);
                myCam.enabled = false;
                newCam.enabled = true; */
            }
        }
	}

    IEnumerator WarpAtEndOfFrame(GameObject collidedPlayer)
    {
        yield return new WaitForEndOfFrame();

        if (destinationObject != null)
        {
            // Warps player to a destination object transform
            collidedPlayer.transform.position = destinationObject.transform.position + destinationObject.transform.up * warpOffset;
        }
        else if (destinationCoordinates != null)
        {
            collidedPlayer.transform.position = new Vector3(destinationCoordinates.x, destinationCoordinates.y, 0f); ;
        }
    }
}
