using iNKORE.UI.WPF.Modern.Controls;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Receiving;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTT_For_WPF.Cs
{
    internal class MQTT_Launcher
    {
        public static IMqttServer? mqttServer;
        public static void starting(){
            Task.Run(() => Start());
            Log.WriteLine("服务端目前充当消息转发媒介，" +
                              "可以向指定订阅的客户端发送消息，\n" +
                              "但不建议直接从服务端面板发送，" +
                              "推荐使用跨平台的客户端进行操控交互");
            /*while (true)
            {
                //Log.WriteLine("==========================================");
                *//*var cmmd = Log.ReadLine();

                switch (cmmd)
                {
                    case "qt":
                        stop();
                        break;
                    default:
                        Log.WriteLine("主题数据名字:");
                        var cmd = Log.ReadLine();
                        Log.WriteLine("内容:");
                        var cmdr = Log.ReadLine();
                        if (mqttServer != null)
                        {
                            Sendmesage(cmd, cmdr);
                        }
                        break;
                }*//*


            }*/
        }

        public async static void Start()
        {
            // 创建MQTT服务器选项
            IMqttServerOptions? mqttServerOptions = new MqttServerOptionsBuilder()
                .WithDefaultEndpointPort(1883) // 设置MQTT服务器端口
                .Build();

            // 创建MQTT服务器
            mqttServer = new MqttFactory().CreateMqttServer();

            // 处理客户端连接事件
            mqttServer.ClientConnectedHandler = new MqttServerClientConnectedHandlerDelegate(e =>
            {
                Log.WriteLine($"客户端连接: {e.ClientId}");
            });

            // 处理客户端断开连接事件
            mqttServer.ClientDisconnectedHandler = new MqttServerClientDisconnectedHandlerDelegate(e =>
            {
                Log.WriteLine($"客户端断开连接: {e.ClientId}");
            });

            // 处理消息接收事件
            mqttServer.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(e =>
            {
                var message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                GL.MQTT_New_Message = e.ApplicationMessage.Topic+"|"+message;
                Log.WriteLine($"接收到消息: 主题={e.ApplicationMessage.Topic}, 消息={message}");
            });

            Debug.WriteLine(mqttServerOptions.ToString());
            // 启动MQTT服务器
            await mqttServer.StartAsync(mqttServerOptions);
            Log.WriteLine($"MQTT服务器已启动 {mqttServerOptions.DefaultEndpointOptions.BoundInterNetworkAddress}:{mqttServerOptions.DefaultEndpointOptions.Port}");
        }
        public async static void Sendmesage(string topic, string messg)
        {
            // 创建MQTT消息
            var mqttMessage = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(messg)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .Build();
            // 发布消息
            await mqttServer.PublishAsync(mqttMessage);
            Log.WriteLine("消息已发送");
        }
        public async static void stop()
        {
            if (mqttServer != null)
            {
                await mqttServer.StopAsync();
                Log.WriteLine("MQTT服务器已停止");
            }
        }
    }
}
