using System.Net;
using System.Net.Sockets;
using System.Text;


//Client code

//info about localhost 
IPHostEntry iPEntry = await Dns.GetHostEntryAsync(Dns.GetHostName());


//localhost ip address
IPAddress ip = iPEntry.AddressList[0];


IPEndPoint ipEndPoint = new(ip, 1234);

Console.WriteLine(ipEndPoint.ToString());

//client socket
using Socket client = new(

    ipEndPoint.AddressFamily,
    SocketType.Stream,
    ProtocolType.Tcp);


await client.ConnectAsync(ipEndPoint);


while (true)
{
    Console.WriteLine("Send A Message: ");
    var message = Console.ReadLine();

    //convert the string message to bytes message
    var messageBytes = Encoding.UTF8.GetBytes(message);

    //send the message to the server
    await client.SendAsync(messageBytes, SocketFlags.None);

    var buffer = new byte[1_024];


    //we recevied the message in the form of bytes
    var received = await client.ReceiveAsync(buffer, SocketFlags.None);

    var messageString = Encoding.UTF8.GetString(buffer, 0, received);

    Console.WriteLine(messageString);
}



