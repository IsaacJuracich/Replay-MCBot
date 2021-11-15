using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MinecraftClient.Replay {
    public class Init {
        public static Var v = JsonConvert.DeserializeObject<Var>(File.ReadAllText("settings.json"));
        public static void refresh() {
            v = JsonConvert.DeserializeObject<Var>(File.ReadAllText("settings.json"));
        }
    }
}
