using MinecraftClient.ChatBots;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MinecraftClient.Replay {
    public class replayMain : ChatBot {
        public static EasyMatch matcher = new EasyMatch("{", "}");
        public static string player;
        public static string msg;
        public override void GetText(string text) {
            if (text == null || text.Replace(" ", "").Length < 1 || text.Trim().Length < 1) return;
            text = GetVerbatim(text);
            var m = matcher.match(text, Init.v.fchat_format);
            if (m.Count > 0) {
                foreach (string s in m) {
                    if (EasyMatch.isMatchValid(s, matcher)) {
                        if (s.Split('|')[0].Replace(":", "").TrimStart() == "player" && Program.client.GetOnlinePlayers().Contains(s.Split('|')[1].Replace("***", "").Replace("**", "").Replace("*", "").Replace(":", ""))) {
                            player = s.Split('|')[1].Replace("***", "").Replace("**", "").Replace("*", "").Replace(":", "");
                        }
                        if (s.Split('|')[0] == "msg") {
                            msg = s.Split('|')[1];
                        }
                    }
                }
            }
            // Command Handler Call
            CommandHandler(text);
        }
        public override void Initialize() {
            if (!File.Exists("settings.json")) {
                createVar();
                Console.WriteLine("Settings File Created, default values assumed: \n" +
                    "[settings.json]:\n" + File.ReadAllText("settings.json"));
            } else Console.WriteLine("Settings Loaded");
        }
        public static void createVar() {
            Var settings = new Var();
            JsonSerializerSettings settings1 = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
            string contents = JsonConvert.SerializeObject(settings, Formatting.Indented, settings1);
            File.WriteAllText("settings.json", contents); 
            
        }
        // Command Handler Function
        public void CommandHandler(string str) {
            if (player != null && msg != null) {
                if (msg.ToLower() == Init.v.m_prefix + "replay_save") {
                    if (ReplayCapture.replay == null) {
                        SendText("[ReplayBot] | No Current Replay Running");
                    } else {
                        ReplayCapture.replay.CreateBackupReplay(@"replay_recordings\" + ReplayCapture.replay.GetReplayDefaultName());
                        SendText("[ReplayBot] | ReplayBot Saving Capture");
                    }
                }
                if (msg.ToLower() == Init.v.m_prefix + "replay_stop") {
                    if (ReplayCapture.replay == null) {
                        SendText("[ReplayBot] | No Current Replay Running");
                    } else {
                        ReplayCapture.replay.OnShutDown();
                        SendText("[ReplayBot] | ReplayBot Stopping Capture");
                    }
                }
                if (msg.ToLower() == Init.v.m_prefix + "replay_start") {
                    if (ReplayCapture.replay.RecordRunning) {
                        SendText("[ReplayBot] | Current Replay Running");
                    } else {
                        var proc = new Process();
                        proc.StartInfo.FileName = "MinecraftClient.exe";
                        proc.Start();
                        Environment.Exit(0);
                        SendText("[ReplayBot] | ReplayBot Starting Capture");
                    }
                }
                player = null;
                msg = null;
            }
        }
    }
}
