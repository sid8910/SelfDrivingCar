using UnityEngine;
using SocketIO;
using System.Collections.Generic;
using System;

public class CarController : MonoBehaviour {

    private SocketIOComponent _socket;
    public Camera FrontFacingCamera;

    public Rigidbody rb;
    public float force = 2000f;

    void Start()
    {
        _socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();
        _socket.On("open", OnOpen);
        _socket.On("move", OnMove);
    }

    void OnOpen(SocketIOEvent obj)
    {
        Debug.Log("Connection Open");
        EmitTelemetry(obj);
    }

    void OnMove(SocketIOEvent obj)
    {
        EmitTelemetry(obj);
        JSONObject jsonObject = obj.data;
        string key = jsonObject.GetField("key").str;
        Move(key);
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

    void FixedUpdate () {
        rb.AddForce(0, 0, 100 * Time.deltaTime);
        Move("0");

    }

    void Move(string key)
    {
        if (key == "d" || Input.GetKey("d"))
        {
            rb.AddForce(force * Time.deltaTime, 0, 0);
        }
        if (key == "a" || Input.GetKey("a"))
        {
            rb.AddForce(-force * Time.deltaTime, 0, 0);
        }
        if (key == "w" || Input.GetKey("w"))
        {
            rb.AddForce(0, 0, force * Time.deltaTime);
        }
        if (key == "s" || Input.GetKey("s"))
        {
            rb.AddForce(0, 0, -force * Time.deltaTime);
        }
    }
}
