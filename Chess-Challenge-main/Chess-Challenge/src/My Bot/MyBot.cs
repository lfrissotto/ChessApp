using ChessChallenge.API;
using System;
using SocketIOClient;

public class MyBot : IChessBot
{
    private string connectionId;
    private string mensagem;


    public static SocketIO client = new SocketIO("http://localhost:3000/");

    public static async void Connection(string connectionId)
    {
        // Setting up the server IP address and port number
        // var client = new SocketIO("http://localhost:3000/");
        //var client = new SocketIO("http://localhost:3000/");

        client.On("message", response =>
        {
            // You can print the returned data first to decide what to do next.
            // output: ["hi client"]
            Console.WriteLine(response);

            connectionId = response.GetValue<string>();
            // The socket.io server code looks like this:
            // socket.emit('hi', 'hi client');
        });

        client.On("connected", response =>
        {
            // You can print the returned data first to decide what to do next.
            // output: ["ok",{"id":1,"name":"tom"}]
            Console.WriteLine(response);
            
            // Get the first data in the response
            connectionId = response.GetValue<string>();
            // Get the second data in the response
            //var dto = response.GetValue<TestDTO>(1);

            // The socket.io server code looks like this:
            // socket.emit('hi', 'ok', { id: 1, name: 'tom'});
        });

        client.OnConnected += async (sender, e) =>
        {
            // Emit a string

            await client.EmitAsync("hi", "socket.io");

            // Emit a string and an object
            //var dto = new TestDTO { Id = 123, Name = "bob" };
            await client.EmitAsync("register", "source");
        };
        await client.ConnectAsync();
    }

    
    public Move Think(Board board, Timer timer, bool flag)
    {
        Move[] moves = board.GetLegalMoves();
        // const connected = new Connection();
        Console.WriteLine("Bot Move: " + moves[0]);
        Console.WriteLine("ID: " + connectionId);
        return moves[0];
    }


    public void ConnectionHandler()
    {
        Connection(connectionId);
        Console.WriteLine("Aqui: "+connectionId);
        //joinRoom()
    }
}