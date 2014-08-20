using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SlackQueueReader.Handlers {
    public class SlackMessageHandler : IMessageHandler {
        public Task Handle(SlackMessage message) {
            Console.WriteLine(JsonConvert.SerializeObject(message, Formatting.Indented));
            return Task.FromResult(true);
        }

        public bool CanHandle(SlackMessage message) {
            return true;
        }
    }
}