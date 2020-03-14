# Publisher-Subscriber-Coding-Challenge
A Publisher Subcriber Coding Challenge I did a while back. There's a bug....Can you find it? XD

>Publisher.cs and Client.cs included in the zip as well as the VS 	project, built against .NET 4.5.2

>Publisher.exe will immediately attempt to start the server 
	(on port 5001) when executed. 

>Can be built either using Visual Studio or running the .cs 	through a C# compiler.

>Contains 4 main classes and 2 events:

	Publisher: opens connections with, handles 			messages from, and updates clients as well as maintains 	subscriber lists.

	Message: contains messages and interpretes data being sent 	between the publisher and client.

	Connection: listens for messages from clients and fires an 	event when a message is succesfully read.

	ConnectionData: holds relevant connection information and 	objects (i.e. the stream, buffer).

	MessageRecievedEvents: these are fired when a message is 	succesfully recieved by a connection.

	OBLITERATEEvents: these are fired when there is an issue 	either interpreting a message or writing to a particular 	connection they initiate the closing of a Connection.
