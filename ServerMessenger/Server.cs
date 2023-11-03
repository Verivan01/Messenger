using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class Server
{
    private TcpListener listener;
    private List<ClientHandler> clients = new List<ClientHandler>();

    public event Action<string, ClientHandler> MessageReceived;

    public Server(int port)
    {
        listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
    }

    public async Task StartAsync()
    {
        while (true)
        {
            TcpClient client = await listener.AcceptTcpClientAsync();
            ClientHandler clientHandler = new ClientHandler(client, this);
            clients.Add(clientHandler);

            Task.Run(() => clientHandler.HandleClientAsync());
        }
    }

    public void BroadcastMessage(string message, ClientHandler sender)
    {
        MessageReceived?.Invoke(message, sender);
        foreach (ClientHandler client in clients)
        {
            if (client != sender)
            {
                client.SendMessage(message);
            }
        }
    }
}

public class ClientHandler
{
    private TcpClient client;
    private NetworkStream stream;
    private Server server;

    public ClientHandler(TcpClient client, Server server)
    {
        this.client = client;
        stream = client.GetStream();
        this.server = server;
    }

    public async Task HandleClientAsync()
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
            server.BroadcastMessage(receivedMessage, this);
        }
    }

    public async Task SendMessage(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        await stream.WriteAsync(data, 0, data.Length);
    }
}
