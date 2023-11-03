using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class Client
{
    private TcpClient client;
    private NetworkStream stream;

    public event Action<string> MessageReceived;

    public async Task ConnectAsync(string serverIp, int serverPort)
    {
        client = new TcpClient();
        await client.ConnectAsync(serverIp, serverPort);
        stream = client.GetStream();
        StartListening();
    }

    public async Task SendAsync(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        await stream.WriteAsync(data, 0, data.Length);
    }

    private async void StartListening()
    {
        byte[] buffer = new byte[1024];
        int bytesRead;

        while (true)
        {
            bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            if (bytesRead <= 0)
            {
                
                break;
            }

            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            MessageReceived?.Invoke(receivedMessage);
        }
    }
}
