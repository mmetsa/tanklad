using System.Collections;
using System.Collections.Generic;

namespace DTO.App
{
    public class Message
    {
        public Message()
        {
            
        }

        public Message(params string[] messages)
        {
            Messages = messages;
        }
        public IList<string> Messages { get; set; } = new List<string>();
    }
}