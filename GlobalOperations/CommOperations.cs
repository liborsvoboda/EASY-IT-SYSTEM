using System;

namespace EasyITSystemCenter.GlobalOperations {

    interface IMessage {
        Type Type { get; }
        object Data { get; }
    }

    class Message : IMessage {
        public Type Type { get; set; }
        public object Data { get; set; }
    }



    //TODO 
    //Udelat agendu websocket zpráv server i třeba s uživatelem a bude to chat
    //rozsirit tento message a udelat jej centralni a z neho pak plugin





    /// <summary>
    /// Centralized Communication Operations as Console, Web Socket 
    /// Plugins and Extensions communications
    /// </summary>
     class CommOperations {

        public void SendToConsole(Message message) { Console.WriteLine($"Send: Type={message.Type.Name}, Data={message.Data}"); }
        public void SendToConsole<T>(Message message) { Console.WriteLine($"Send: Type={typeof(T)}, Data={message.Data}"); }




    }
}