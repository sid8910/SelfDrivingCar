using UnityEngine;
//using SocketIO;
using System.Collections.Generic;
using System;

public class RearWheelDrive : MonoBehaviour {

//    private SocketIOComponent _socket;
    public Camera FrontFacingCamera;

    private WheelCollider[] wheels;

	public float maxAngle = 30;
	public float maxTorque = 300;
	public GameObject wheelShape;

	// here we find all the WheelColliders down in the hierarchy
	public void Start()
	{

/*        _socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();
        _socket.On("open", OnOpen);
        _socket.On("move", OnMove);
*/
        wheels = GetComponentsInChildren<WheelCollider>();

		for (int i = 0; i < wheels.Length; ++i) 
		{
			var wheel = wheels [i];

			// create wheel shapes only when needed
			if (wheelShape != null)
			{
				var ws = GameObject.Instantiate (wheelShape);
				ws.transform.parent = wheel.transform;

                if(wheel.transform.localPosition.x < 0f)
                {
                    ws.transform.localScale = new Vector3(ws.transform.localScale.x*-1f, ws.transform.localScale.y, ws.transform.localScale.z);
                }
			}
		}
	}


 /*   void OnOpen(SocketIOEvent obj)
    {
        Debug.Log("Connection Open");
        EmitTelemetry(obj);
    }

    void OnMove(SocketIOEvent obj)
    {
        EmitTelemetry(obj);
        JSONObject jsonObject = obj.data;
        string key = jsonObject.GetField("key").str;

        //float stearingAngle = JSONObject.GetField("stearing_angle");
        //float torque = JSONObject.GetField("torque_val");
        //Update(stearingAngle, torque);
    }

    void EmitTelemetry(SocketIOEvent obj)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["move"] = "true";
            data["image"] = Convert.ToBase64String(CameraHelper.CaptureFrame(FrontFacingCamera));
            _socket.Emit("telemetry", new JSONObject(data));
        });
    }
*/
    // this is a really simple approach to updating wheels
    // here we simulate a rear wheel drive car and assume that the car is perfectly symmetric at local zero
    // this helps us to figure our which wheels are front ones and which are rear


    public void Update()
	{
        
		float angle = maxAngle * Input.GetAxis("Horizontal");
		float torque = maxTorque * Input.GetAxis("Vertical");

		foreach (WheelCollider wheel in wheels)
		{
			// a simple car where front wheels steer while rear ones drive
			if (wheel.transform.localPosition.z > 0)
				wheel.steerAngle = angle;

			if (wheel.transform.localPosition.z < 0)
				wheel.motorTorque = torque;

			// update visual wheels if any
			if (wheelShape) 
			{
				Quaternion q;
				Vector3 p;
				wheel.GetWorldPose (out p, out q);

				// assume that the only child of the wheelcollider is the wheel shape
				Transform shapeTransform = wheel.transform.GetChild (0);
				shapeTransform.position = p;
				shapeTransform.rotation = q;
			}

		}
	}
}
