using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinecraftClient.Replay {
    public class Var {
        public string replayName;
        public bool replayBot;
        public List<string> whitelisted;
        public string d_prefix;
        public string m_prefix;
        public ulong d_logChannelID;
        public string d_token;
        
        public Var() {
            this.replayName = null;
            this.replayBot = true;
            this.whitelisted = new List<string>();
            this.d_prefix = "";
            this.m_prefix = "";
            this.d_logChannelID = 0;
            this.d_token = "discord_token";
        }
    }
}
