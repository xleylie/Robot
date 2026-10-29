using System.Net;
using System.Net.Sockets;
using System.Text;

int port = 5000;

using Socket clientSocket = new Socket(
    AddressFamily.InterNetwork,
    SocketType.Stream,
    ProtocolType.Tcp
    );

try
{
    clientSocket.Connect(new IPEndPoint(IPAddress.Loopback, port));
}
catch (SocketException ex)
{
    Console.WriteLine($"Не удалось подключиться к серверу");
    return;
}

using var stream = new NetworkStream(clientSocket);
using var reader = new StreamReader(stream, Encoding.UTF8);
using var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

while (true)
{
    Console.WriteLine("1.UP 2.DOWN 3.LEFT 4.RIGHT 5.POSITION 6.EXIT");
    if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 6)
    {
        Console.WriteLine("Неверный выбор");
        continue;
    }

    string command = choice switch
    {
        1 => "UP",
        2 => "DOWN",
        3 => "LEFT",
        4 => "RIGHT",
        5 => "POSITION",
        _ => "EXIT"
    };

    writer.WriteLine(command);
    if (choice == 6)
        break;

    string? response = reader.ReadLine();
    if (response == null)
    {
        Console.WriteLine("Сервер закрыл соединение");
        break;
    }
    Console.WriteLine(response);
}