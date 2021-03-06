﻿using System;

namespace SlackQueueReader {
    public class SlackMessage {
        public string token { get; set; }
        public string team_id { get; set; }
        public string channel_id { get; set; }
        public string channel_name { get; set; }
        public string timestamp { get; set; }

        public DateTime when {
            get { return new DateTime(1970, 1, 1).AddSeconds(long.Parse(timestamp.Split('.')[0])); }
        }

        public string user_id { get; set; }
        public string user_name { get; set; }
        public string text { get; set; }
    }
}