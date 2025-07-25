﻿using System;

namespace EasyITSystemCenter.OSHelpers {

    internal class CPUUsage {
        public double CPU { get; private set; }

        public CPUUsage() {
            Update();
        }

        private static long ToLong(System.Runtime.InteropServices.ComTypes.FILETIME fileTime) {
            long returnedLong;
            // Convert 4 high-order bytes to a byte array
            byte[] highBytes = BitConverter.GetBytes(fileTime.dwHighDateTime);
            // Resize the array to 8 bytes (for a Long)
            Array.Resize(ref highBytes, 8);

            // Assign high-order bytes to first 4 bytes of Long
            returnedLong = BitConverter.ToInt64(highBytes, 0);
            // Shift high-order bytes into position
            returnedLong <<= 32;
            // Or with low-order bytes
            returnedLong |= fileTime.dwLowDateTime;
            // Return long
            return returnedLong;
        }

        public void Update() {
            PerformanceInfo.GetSystemTimes(out System.Runtime.InteropServices.ComTypes.FILETIME idleTime1, out System.Runtime.InteropServices.ComTypes.FILETIME krnlTime1, out System.Runtime.InteropServices.ComTypes.FILETIME userTime1);

            System.Threading.Thread.Sleep(100);

            PerformanceInfo.GetSystemTimes(out System.Runtime.InteropServices.ComTypes.FILETIME idleTime2, out System.Runtime.InteropServices.ComTypes.FILETIME krnlTime2, out System.Runtime.InteropServices.ComTypes.FILETIME userTime2);

            long idleTime = ToLong(idleTime2) - ToLong(idleTime1);
            long krnlTime = ToLong(krnlTime2) - ToLong(krnlTime1);
            long userTime = ToLong(userTime2) - ToLong(userTime1);
            long total = krnlTime + userTime;

            CPU = (double)(total - idleTime) / total * 100.0;
        }
    }
}