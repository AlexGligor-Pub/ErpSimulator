using HiveMQtt.Client;
using HiveMQtt.Client.Options;
using System;
using System.Configuration;

public class MQTTPublisher
{
    private HiveMQClientOptions options;
    private HiveMQClient client;

    public MQTTPublisher(HiveMQClientOptions options)
    {
        this.options = options;
    }

    public MQTTPublisher()
    {
        //get options from config file
        options = new HiveMQClientOptions
        {
            Host = ConfigurationManager.AppSettings["host"],
            Port = Int32.Parse(ConfigurationManager.AppSettings["port"]),
            UserName = ConfigurationManager.AppSettings["user"],
            Password = ConfigurationManager.AppSettings["pwd"],
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

    public async Task PublishMessage(string topic, string message)
    {
        await client.PublishAsync(topic, message).ConfigureAwait(false);
    }
}