using System;
using System.Threading.Tasks;
using Nest;

namespace SlackQueueReader.Handlers {
    public class IndexHandler : IMessageHandler {
        private readonly ElasticClient _client;

        public IndexHandler(Uri nodeUri) {
            var settings = new ConnectionSettings(nodeUri, "slack-message");
            _client = new ElasticClient(settings);
        }

        public async Task Handle(SlackMessage message) {
            await _client.IndexAsync(message);
        }

        public bool CanHandle(SlackMessage message) {
            return true;
        }
    }
}