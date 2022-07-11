//Follow Tracker By Niklas Meisch 16.06.2022
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FollowTracker : MonoBehaviour
{

    [Header("Tracker Settings")]
    public bool useRotation = true;
    public bool usePosition = true;
    [Tooltip("use GameObject.find to get the Tracker reference")]
    [SerializeField] bool _getTrackerDuringRuntime;
    [Tooltip("Tracker name in scenen (is case sensetive)")]
    [TextArea]
    [SerializeField] string _TrackerName;
    [Tooltip("The Tracker with the Pose Driver has to be in the scene with both Rotation and Position enabled " +
     "it is needed for calibration.")]
    [SerializeField] private Transform _tracker;
    [SerializeField] private Transform _leftController;
    [SerializeField] private Transform _rightController;

    [Header("Camera Settings")]
    [Tooltip("if Instanciate Camera Rig is on the script will create a new one from the prefab" +
        "if it is disabled it will use the object you dragged in.")]
    [SerializeField] private bool _instantianteCameraRig;
    [SerializeField] private GameObject _cameraRigPrefab;

    [Header("Cave Settings")]
    [SerializeField] private Vector3 _caveDimentions = new Vector3(2, 2, 2);
    [Header("tracker Visualization")]
    [SerializeField] private GameObject _trackerVisuals;

    [Header("Calibration Settings")]
    [Tooltip("time Till Calibration is the time it waits before setting the origin location and rotation.")]
    [SerializeField] private float _timeTillCalibration = 3;
    [Tooltip("if this is true it will calibrate automatically, if it isnt you can call SetTrackerOrigin(); form another script.")]
    [SerializeField] private bool _setOnStart = true;
    [Tooltip("this allows you to call SetTrackerOrigin in the editor by just checking the box.")]
    [SerializeField] private bool _debugStart = false;
    private Transform _handParent;
    private Transform _headObject;
    private Transform _trackerOrigin;
    private Transform _followerOrigin;
    private Transform _followerTracker;

    private bool _isCalibrated = false;

    private void Awake()
    {
        CreateOrigin();
    }
    private void Start()
    {
        if (_setOnStart) {
            StartCoroutine(SetOriginAfterTime());
        }
    }
    private void Update()
    {
        if (_debugStart) {
            SetTrackerOrigin();
            _debugStart = false;
        }
        UpdateTrackerPosition();
    }
    #region Gizmos
    private void OnDrawGizmos()
    {

        //facing direction cave
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position + this.transform.forward, this.transform.position + this.transform.forward * 2);
        //facing direction head
        if (_followerTracker != null) {
            Gizmos.DrawLine(_followerTracker.position, _followerTracker.position + _followerTracker.up);
        }
        //Cave dimentions
        Gizmos.matrix = this.transform.localToWorldMatrix;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(0, _caveDimentions.y / 2, 0), _caveDimentions);
        //Cave dimentions
        if (_followerTracker != null) {
            Gizmos.matrix = _followerTracker.localToWorldMatrix;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(Vector3.zero, 0.10f);
        }

    }
    #endregion
    private void UpdateTrackerPosition()
    {
        //based on input set by bools 
        if (_isCalibrated) {
            //set tracker rotation 
            if (useRotation) {
                _followerTracker.localRotation = _tracker.rotation;
            }

            //Get the distance vector for the _trackerOrigin and the Tracker  and then add it form the new origin.
            if (usePosition) {
                _followerTracker.localPosition = (_tracker.position - _trackerOrigin.position);
            }

            //debug Feedback
            if (!usePosition && !useRotation) {
                Debug.LogWarning("No Rotation Selected. Please Select at least One", this);
            }

            //move and rotate head object
            if (_headObject != null) {
                _headObject.transform.localPosition = (_tracker.position - _trackerOrigin.position);
                _headObject.transform.localEulerAngles = _tracker.rotation.eulerAngles;
            }
        }
    }
    private void CreateOrigin()
    {
        // find Tracker in the scene
        if (_tracker == null) {
            _tracker = GameObject.Find("Tracker").transform;
        }
        // Create new Origin for Follower
        _followerOrigin = new GameObject("followerOrigin").transform;
        _followerOrigin.SetParent(this.transform);
        _followerOrigin.localPosition = Vector3.zero;
        _followerOrigin.localRotation = Quaternion.identity;

        // Create new Tracker for Follower
        _followerTracker = new GameObject("followerTracker").transform;
        _headObject = new GameObject("TrackerVisuals").transform;

        // Create reference origin for original tracker
        _trackerOrigin = new GameObject("trackerOrigin").transform;
        _trackerOrigin.position = Vector3.zero;
        _trackerOrigin.rotation = Quaternion.identity;
        //Create HeadParent
        if (_leftController != null || _rightController != null)
        {
            _handParent = new GameObject("handParent").transform;
        }

    }
    public void SetTrackerOrigin()
    {

        //set Virtual origin
        _trackerOrigin.position = _tracker.position;
        _trackerOrigin.rotation = _tracker.rotation;

        //Orient Cave / follower origin 
        _followerTracker.SetParent(_followerOrigin.transform);
        _followerTracker.localPosition = Vector3.zero;
        _followerTracker.localRotation = _trackerOrigin.rotation;

        // set headobject pos and parent
        _headObject.SetParent(_followerOrigin.transform);
        _headObject.localPosition = Vector3.zero;
        _headObject.localRotation = _trackerOrigin.rotation;

        // set visuals as cild of head Object
        if (_trackerVisuals != null) {
            _trackerVisuals.transform.SetParent(_headObject.transform);
            _trackerVisuals.transform.localPosition = Vector3.zero;
            _trackerVisuals.transform.forward = _headObject.up;
        }

        //turn origin to fit to cave
        //create helper object at the looks direction positon 
        GameObject lookAtMe = new GameObject("lookAtMe");
        lookAtMe.transform.position = _followerOrigin.position + _followerTracker.up;

        //create helper object at origin pos and have it look at lookAtMe helper object
        GameObject rotationChecker = new GameObject("RotationChecker");
        rotationChecker.transform.position = _followerOrigin.position;
        rotationChecker.transform.LookAt(lookAtMe.transform);

        //get angle between the two vectors 
        float angle = Vector3.SignedAngle(_followerOrigin.forward, rotationChecker.transform.forward, Vector3.up);

        //set rotation of the origin based on angel
        _followerOrigin.localRotation = Quaternion.AngleAxis(angle * -1, Vector3.up);

        // Delete Helper Ojects
        Destroy(lookAtMe);
        Destroy(rotationChecker);

        // Create Camera Rig 
        if (_instantianteCameraRig) {
            _cameraRigPrefab = Instantiate(_cameraRigPrefab);
        }
        //move rotate and child the camera rig followertracker
        _cameraRigPrefab.transform.position = _followerTracker.position;
        _cameraRigPrefab.transform.rotation = this.transform.rotation;
        _cameraRigPrefab.transform.forward = this.transform.forward;
        _cameraRigPrefab.transform.SetParent(_followerTracker);


        //set HeadParent
        if (_headObject != null)
        {
            _handParent.position = _followerOrigin.position;
            _handParent.rotation = _followerOrigin.rotation;
            _handParent.SetParent(_followerOrigin);
            _handParent.localPosition = -_trackerOrigin.position;
        }
        //set origin for controllers
        if (_rightController != null) {
            _rightController.SetParent(_followerOrigin);
        }
        if (_leftController != null) {
            _leftController.SetParent(_followerOrigin);
        }

        //give feedback!!
        _isCalibrated = true;
        Debug.Log("Calibrated");
    }
   
    public IEnumerator SetOriginAfterTime()
    {
        yield return new WaitForSecondsRealtime(_timeTillCalibration);
        SetTrackerOrigin();
        yield return null;
    }
}
