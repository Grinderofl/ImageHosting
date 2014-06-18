using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenPop.Mime;
using OpenPop.Pop3;

namespace ImageHosting.Core
{
    public class EmailRetriever
    {
        private EmailParser _parser;
        public EmailRetriever(EmailParser parser)
        {
            _parser = parser;
        }

        public void Retrieve()
        {
            var messages = new List<Message>();
            using (var client = new Pop3Client())
            {
                client.Connect("pop.myserver.net", 995, true);
                client.Authenticate("user", "pass", AuthenticationMethod.UsernameAndPassword);
                for (var i = client.GetMessageCount(); i > 0; i--)
                {
                    var message = client.GetMessage(i);
                    messages.Add(message);
#if (!DEBUG)
                    client.DeleteMessage(i);
#endif
                }
            }

            _parser.Parse(messages);
        }
    }
}