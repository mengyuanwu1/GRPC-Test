using System;
using Cysharp.Net.Http;
using Grpc.Core;
using Grpc.Net.Client;
using Helloworld;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Calls : MonoBehaviour
{
    public Button Button;
    public TMP_Text Text;
    
    // private GrpcChannel channel;
    // private YetAnotherHttpHandler handler;
    // private Greeter.GreeterClient client;

    public string host = "http://localhost:50051";

    // Start is called before the first frame update
    void Start()
    {
        Button.onClick.AddListener(OnCallRPC);
        
        // var options = new GrpcChannelOptions();
        // // var handler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
        // handler = new YetAnotherHttpHandler();
        // options.HttpHandler = handler;
        // options.Credentials = ChannelCredentials.Insecure;
        // channel = GrpcChannel.ForAddress(host, options);
        // // client = new RPCTest.RPCTestClient(channel);
        // client = new Greeter.GreeterClient(channel);

        Debug.Log("Client started on channel " + host);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnCallRPC()
    {
        // TestRPCOneArgOneReturnRequest request = new TestRPCOneArgOneReturnRequest { Input0 = "Hello from Unity" };
        // var reply = client.TestRPCOneArgOneReturn(request);

        // var reply = client.SayHello(new HelloRequest { Name = "GreeterClient" });

        Debug.Log("Calling RPC");
        using var handler = new YetAnotherHttpHandler();
        using var channel = GrpcChannel.ForAddress(host, new GrpcChannelOptions() { HttpHandler = handler, Credentials = ChannelCredentials.Insecure});
        var greeter = new Greeter.GreeterClient(channel);
        HelloRequest request = new HelloRequest { Name = "Unity" };
        var reply = greeter.SayHello(request);
        
        Debug.Log(reply.ToString());
        Text.text = $"[{DateTime.Now}] {reply}";
    }
}
