using HiveMQtt.Client;
using HiveMQtt.Client.Options;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;

public class MQTTPublisher
{
    private HiveMQClientOptions options;
    private HiveMQClient client;
    private IConfiguration configuration;
    private string topic;

    public MQTTPublisher( IConfiguration configuration)
    {
        this.configuration = configuration;
        client = new HiveMQClient(options);
        topic = configuration["MQTT:topic"];
        options = new HiveMQClientOptions
        {
            Host = configuration["MQTT:host"],
            Port = Int32.Parse(configuration["MQTT:port"]),
            UserName = configuration["MQTT:user"],
            Password = configuration["MQTT:pwd"],
            UseTLS = true
        };
    }
    public async Task ConnectToBroker()
    {
        var connectResult = await client.ConnectAsync().ConfigureAwait(false);
        if (connectResult.ReasonCode != HiveMQtt.MQTT5.ReasonCodes.ConnAckReasonCode.Success)
        { 
            throw new Exception("Connection to HiveMQ failed: " + connectResult.ReasonString);
        }
    }

    public async Task<bool> PublishMessage(string message)
    {
        try
        {
            await ConnectToBroker();
            await client.PublishAsync(topic, message).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
        return true;
    }
}