using System.Net;
using System.Net.Sockets;
using System.Text;

int port = 5000;
int x = 0;
int y = 0;

using Socket socket = new Socket(
    AddressFamily.InterNetwork,
    SocketType.Stream,
    ProtocolType.Tcp    
    );

socket.Bind(new IPEndPoint(IPAddress.Loopback, port));
socket.Listen(10);

using var clientSocket=socket.Accept();

using var stream = new NetworkStream(clientSocket); 
using var reader = new StreamReader(stream, Encoding.UTF8);
using var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

string? line;
while ((line = reader.ReadLine()) != null)
{
    Console.WriteLine($"Получено: {line}");
    if (line == "EXIT")
    {
        break;
    }
    switch (line)
    {
        case "UP": y++; break;
        case "DOWN": y--; break;
        case "RIGHT": x++; break;
        case "LEFT": x--; break;
        case "POSITION": break;
        default:
            writer.WriteLine("ERROR: Unknown command");
            continue;
    }
    writer.WriteLine($"Позиция: ({x}, {y}) ");
}