// See https://aka.ms/new-console-template for more information

using DtoModelsLibrary;
using System.Net.Http.Json;
using System;
using System.Timers;
using ConsoleSimulator;


System.Timers.Timer timer = new System.Timers.Timer();
CreateFlightLogic.CreateFlight();

for (int i = 0; i < 10; i++)
{
    Thread.Sleep(10000);
    CreateFlightLogic.CreateFlight();
}


//timer.Stop();
//timer.Interval = 15000;
//timer.Elapsed += (s, e) => CreateFlightLogic.CreateFlight();
//timer.Start();

//timer.AutoReset = false; // Will run only once

Console.ReadKey();

