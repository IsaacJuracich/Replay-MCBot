# Replay-MCBot
 
> API - MCCTeam By Orelio
> 
> Minecraft Faction Bot-Addon with accessibility to ReplayMod Recording. The bot based off of commands will handle replaymod functions (save, stop, uptime, start).
>
> I plan to add more configurability and more features in the future, there is no bulit in command handler as I am lazy. 
>
> The bot will automatically create the settings.ini for the bot itself, and settings.json for the control of the replaymod name and other settings.
>
>The render distance of players and being able to render them will be picked up by render distance which is changeable in settings.ini
>
> ![image](https://user-images.githubusercontent.com/93289395/141695669-d634e1ed-015b-4ca5-b273-8f7a64bf242d.png)
> 
>            ```csharp 
>            if (text.Contains(Init.v.m_prefix + "replay_save")) {
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
            ```
