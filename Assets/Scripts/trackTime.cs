using UnityEngine;
using System;
using System.IO;
using Movement;

namespace TrackTime
{
    public class trackTime : MonoBehaviour
    {
        public float time;
        public int trackNum;
        CarMove carMove;
        public bool timerRunning = false;
        bool lapFinished = false; // ends when you cross the finish line, using RayCasting on this script, lap finished = true
        public void Update()
        {
            time = carMove.time;
            if(lapFinished)
            {
                timerRunning = false;
                File.WriteAllText($"Assets/Resources/track{trackNum}.txt", time.ToString("F2"));
            }
        }
    }
}