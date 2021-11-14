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
        public override void GetText(string text) {
            text = GetVerbatim(text);
            // Too lazy to add a command handler
            if (text.Contains(Init.v.m_prefix + "replay_save")) {
                if (ReplayCapture.replay == null) {
                    SendText("[ReplayBot] | No Current Replay Running");
                } else {
                    ReplayCapture.replay.CreateBackupReplay(@"replay_recordings\" + ReplayCapture.replay.GetReplayDefaultName());
                    SendText("[ReplayBot] | ReplayBot Saving Capture");
                }
            }
            if (text.Contains(Init.v.m_prefix + "replay_stop")) {
                if (ReplayCapture.replay == null) {
                    SendText("[ReplayBot] | No Current Replay Running");
                } else {
                    ReplayCapture.replay.OnShutDown();
                    SendText("[ReplayBot] | ReplayBot Stopping Capture");
                }   
            }
            if (text.Contains(Init.v.m_prefix + "replay_start")) {
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
    }
}
