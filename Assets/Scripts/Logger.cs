using System;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

public class Logger : MonoBehaviour
{

    public static Logger loggerInstance;

    public float TimeWithM4 = 0;
    public float TimeWithGlock = 0;
    public float TimeWithShotgun = 0;
    public float TimeWithSMG = 0;
    public float TimeWithSniper = 0;
    public float TimeWithFlamethrower = 0;
    public float TimeWithBazooka = 0;
    public int Deaths = 0;
    public int Kills = 0;

    private float _testTimer = 0;

    void Awake()
    {
        if (loggerInstance == null)
        {
            Debug.Log("Instance started");
            loggerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Noninstance destroyed");
            Destroy(this);
        }
    }

    private string BuildSendString()
    {
        return "<timeplayed:" + Convert.ToInt32(Time.realtimeSinceStartup) + "@deaths:" + Deaths + "@kills:" + Kills;
    }

    private string BuildContinousSendString()
    {
        return "<m4time:" + Convert.ToInt32(TimeWithM4) + "@glocktime:" + Convert.ToInt32(TimeWithGlock) +
               "@shotguntime:" + Convert.ToInt32(TimeWithShotgun) + "@snipertime:" + Convert.ToInt32(TimeWithSniper) +
               "@flametime:" + Convert.ToInt32(TimeWithFlamethrower) + "@bazookatime:" + Convert.ToInt32(TimeWithBazooka) +
               "@smgtime:" + Convert.ToInt32(TimeWithSMG);
    }

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _testTimer += Time.deltaTime;
	    if (_testTimer > 150)
	    {
	        if (SendMessage())
	        {
	            Debug.Log("Message sent successfully");
	        }
	        _testTimer = 0;
	    }
	}

    public void ExitSendMessage()
    {
        try
        {
            var client = new TcpClient();
            IAsyncResult result = client.BeginConnect("10.22.3.99", 11000, null, null);

            bool success = result.AsyncWaitHandle.WaitOne(5000, true);

            var sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
            var sMessage = BuildSendString();

            sWriter.WriteLine(sMessage);
            sWriter.Flush();

            Debug.Log("Sent successfully");
        }
        catch (Exception ex)
        {
            Debug.Log("Unsuccessfull");
        }
    }

    public bool SendMessage()
    {
        try
        {
            var client = new TcpClient();
            IAsyncResult result = client.BeginConnect("10.22.3.99", 11000, null, null);

            bool success = result.AsyncWaitHandle.WaitOne(5000, true);

            var sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
            var sMessage = BuildContinousSendString();

            sWriter.WriteLine(sMessage);
            sWriter.Flush();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
