﻿using Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TheArena
{
    public class MyPacket
    {
        public string gameName;
        public string session;
        public gameSettings gameSettings;

        /*public string status;
        public string winReason;
        public string loseReason;
        public string logUrl;
        public Winner winner;
        public Loser loser;*/
    }

    public class gameSettings
    {
        public string[] playerNames;
    }
        

    public class Winner
    {
        public string teamName;
        public string version;
    }

    public class Loser
    {
        public string teamName;
        public string version;
    }

    public class HTTP
    {
        public static void HTTPPostStartGame(string gameName, string session, gameSettings gameSettings, string[] playerNames)
        {
            MyPacket p = new MyPacket() { gameName = gameName, session = session, gameSettings = gameSettings};
            string serialized = JsonConvert.SerializeObject(p);
            Log.TraceMessage(Log.Nav.NavIn, "Serialized data: " + serialized, Log.LogType.Info);
            using (var client = new HttpClient())
            {
                try
                {
                    List<Task> allGames = new List<Task>();
                    Console.WriteLine("Wat");
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6IklMaWtlU29ja3NPblN1bmRheXMiLCJpZCI6Mywicm9sZSI6InVzZXIiLCJpYXQiOjE1NDE4NjY1NTcsImV4cCI6MTU0MjI5ODU1N30.XWaWB_cWhUFEC1m0GxFJ4ln8uq5h086gXGxRmOLVXA0");
                    allGames.Add(Task.Run(async () => {var x = await client.PostAsync(
                        "http://35.190.137.139:3080/setup", new StringContent(serialized, Encoding.UTF8, "application/json"));
                         
                         Console.WriteLine(await x.Content.ReadAsStringAsync());
                         Log.TraceMessage(Log.Nav.NavIn, "Server Response Content: " + await x.Content.ReadAsStringAsync(), Log.LogType.Info);
                         Log.TraceMessage(Log.Nav.NavIn, "Server Response: " + x, Log.LogType.Info);
                         }));
                    Task.WaitAll(allGames.ToArray());
                }
                catch (Exception ex)
                {
                    string idk = ex.Message;
                    Log.TraceMessage(Log.Nav.NavIn, "Except: " + idk, Log.LogType.Info);
                }
            }
        }

        public static void HTTPPostSendToWeb(string status, string winReason, string loseReason, string logURL, string winnerTeamName, string winnerVersion, string loserTeamName, string loserVersion)
        {
            /*MyPacket p = new MyPacket() { status = status, loseReason = loseReason, winReason = winReason, logUrl = logURL, winner=new Winner() { teamName = winnerTeamName, version = winnerVersion },loser=new Loser() { teamName = loserTeamName, version = loserVersion } };
            string serialized = JsonConvert.SerializeObject(p);
            Console.WriteLine(serialized);
            using (var client = new HttpClient())
            {
                try
                {
                    List<Task> allGames = new List<Task>();
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6IklMaWtlU29ja3NPblN1bmRheXMiLCJpZCI6Mywicm9sZSI6InVzZXIiLCJpYXQiOjE1NDE4NjY1NTcsImV4cCI6MTU0MjI5ODU1N30.XWaWB_cWhUFEC1m0GxFJ4ln8uq5h086gXGxRmOLVXA0");
                    allGames.Add(Task.Run(() => client.PostAsync(
                        "https://mmai-server.siggame.io/games/",
                         new StringContent(serialized, Encoding.UTF8, "application/json"))));
                    Task.WaitAll(allGames.ToArray());
                }
                catch (Exception ex)
                {
                    string idk = ex.Message;
                }
            }*/
        }

    }
}
