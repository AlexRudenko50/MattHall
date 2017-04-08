//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;
//
//public class AugmentedScript : MonoBehaviour
//{
//
//
//	private float originalLatitude;
//	private float originalLongitude;
//	private float currentLongitude;
//	private float currentLatitude;
//    private float MIN_CUBE_DIST = 150;
//
//    private GameObject distanceTextObject;
//	private double distance;
//
//	private bool setOriginalValues = true;
//
//	private Vector3 targetPosition;
//	private Vector3 originalPosition;
//
//	private float speed = .1f;
//
////    public GameObject Camera;
//
//	IEnumerator GetCoordinates()
//	{
//        //while true so this function keeps running once started.
//			// check if user has location service enabled
//			if (!Input.location.isEnabledByUser)
//				yield break;
//
//			// Start service before querying location
//			Input.location.Start (1f,.1f);
//
//			// Wait until service initializes
//			int maxWait = 20;
//			while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
//				yield return new WaitForSeconds (1);
//				maxWait--;
//			}
//
//			// Service didn't initialize in 20 seconds
//			if (maxWait < 1) {
//				print ("Timed out");
//				yield break;
//			}
//
//			// Connection has failed
//			if (Input.location.status == LocationServiceStatus.Failed) {
//				print ("Unable to determine device location");
//				yield break;
//			} else {
//				// Access granted and location value could be retrieved
//				print ("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
//
//				//if original value has not yet been set save coordinates of player on app start
//				if (setOriginalValues) {
//					originalLatitude = Input.location.lastData.latitude;
//					originalLongitude = Input.location.lastData.longitude;
//					setOriginalValues = false;
//				}
//        }
//    }
//
//	void Start(){
//        Input.compass.enabled = true;
//        //get distance text reference
//        distanceTextObject = GameObject.FindGameObjectWithTag ("distanceText");
//		//start GetCoordinate() function 
//		StartCoroutine ("GetCoordinates");
//		//initialize target and original position
//		targetPosition = transform.position;
//		originalPosition = transform.position;
//
//	}
//
//	void Update(){
//
//
//        currentLatitude = Input.location.lastData.latitude;
//        currentLongitude = Input.location.lastData.longitude;
//        //Calc(originalLatitude, originalLongitude, currentLatitude, currentLongitude);
//        //Orient();
//        // Orient an object to point northward.
//        print(currentLatitude + ", " + currentLongitude + ", " + distanceTextObject);
//        if(currentLatitude != null) { 
//            var angle = AngleBetweenCoordinates(90f, 0f, 35.913582f, 14.445180f);
//            RotateAboutCamera(angle);
//        }
//    }
//
//
//
//
//    float SignedAngle(Vector3 a, Vector3 b)
//    {
//        var angle = Mathf.Atan2(a.x * b.y - a.y * b.x, a.x * b.x + a.y * b.y);
//        return angle;
//    }
//
//    private float AngleBetweenCoordinates(float lat1, float long1, float lat2, float long2)
//    {
//        float dLon = (long2 - long1);
//
//        float y = Mathf.Sin(dLon) * Mathf.Cos(lat2);
//        float x = Mathf.Cos(lat1) * Mathf.Sin(lat2) - Mathf.Sin(lat1)
//                * Mathf.Cos(lat2) * Mathf.Cos(dLon);
//
//        float brng = Mathf.Atan2(y, x);
//
//        brng = Mathf.Rad2Deg * (brng);
//        brng = (brng + 360) % 360;
//        brng = 360 - brng; // count degrees counter-clockwise - remove to make clockwise
//        distanceTextObject.GetComponent<Text>().text = "ANGLE: " + brng;
//
//        return brng;
//    }
//
//    public void RotateAboutCamera(float angle)
//    {
//        transform.parent.transform.rotation =  Quaternion.Euler(0, /*angle +*/ Input.compass.trueHeading, 0);
//
//        //gameObject.transform.parent.Rotate(Vector3.up, angle);
//    }
//
//}
////void Orient()
////{
////    float delta = 0; 
////    Vector3 north = Quaternion.Euler(0, Input.compass.trueHeading, 0) * fwd;
////    delta = SignedAngle(fwd, north)  + (360 - gameObject.transform.eulerAngles.y);
//
////    float xx = Calc(0, currentLatitude, 0, currentLongitude), zz = Calc(35.913582f, 0, 14.445180f, 0);
////    Vector3 position = new Vector3(xx, 0, zz); //the position of the target if unityNorth and actual geoNorth were the same
////    position.Normalize();
////    Vector3 unityN = new Vector3(0, 0, 1); // unity north
////    var angoloTrgt = Vector3.Angle(unityN, position);
////    position *= MIN_CUBE_DIST;
////    gameObject.transform.position = new Vector3(0, 0, MIN_CUBE_DIST);
////}
//////calculates distance between two sets of coordinates, taking into account the curvature of the earth.
////public float Calc(float lat1, float lon1, float lat2, float lon2)
////{
////	var R = 6378.137; // Radius of earth in KM
////	var dLat = lat2 * Mathf.PI / 180 - lat1 * Mathf.PI / 180;
////	var dLon = lon2 * Mathf.PI / 180 - lon1 * Mathf.PI / 180;
////	float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
////		Mathf.Cos(lat1 * Mathf.PI / 180) * Mathf.Cos(lat2 * Mathf.PI / 180) *
////		Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);
////	var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
////	distance = R * c;
////	distance = distance * 1000f; // meters
////                                    //set the distance text on the canvas
////       //distanceTextObject.GetComponent<Text>().text = "D[" + distance + ", L1[" + lat1 + "/" + lon1 + "] L2[" + lat2 + "/" + lon2 + "]";
////       //convert distance from double to float
////       float distanceFloat = (float)distance;
////	//set the target position of the ufo, this is where we lerp to in the update function
//////	targetPosition = originalPosition - new Vector3 (0, 0, distanceFloat * 12);
////       //distance was multiplied by 12 so I didn't have to walk that far to get the UFO to show up closer
////       return distanceFloat;
////}
////linearly interpolate from current position to target position
////transform.position = Vector3.Lerp(transform.position, targetPosition, speed);
////rotate by 1 degree about the y axis every frame
////transform.eulerAngles += new Vector3 (0, 1f, 0);
////overwrite current lat and lon everytime