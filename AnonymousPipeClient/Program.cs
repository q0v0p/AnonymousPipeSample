﻿using System.IO.Pipes;

if (args.Length <= 0)
    return;

using var pipeClient = new AnonymousPipeClientStream(PipeDirection.In, args[0]);

Console.WriteLine("[CLIENT] Current TransmissionMode: {0}.",
   pipeClient.TransmissionMode);

using var sr = new StreamReader(pipeClient);
// Display the read text to the console
string temp;

// Wait for 'sync message' from the server.
do
{
    Console.WriteLine("[CLIENT] Wait for sync...");
    temp = sr.ReadLine();
}
while (!temp.StartsWith("SYNC"));

// Read the server data and echo to the console.
while ((temp = sr.ReadLine()) != null)
{
    Console.WriteLine("[CLIENT] Echo: " + temp);
}

Console.Write("[CLIENT] Press Enter to continue...");
Console.ReadLine();