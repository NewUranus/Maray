﻿using CommunityToolkit.Mvvm.ComponentModel;

using Maray.Helpers;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Maray.ViewModels
{
    public partial class NetSpeedItemVM : ObservableObject
    {
        private readonly NetworkInterface _interface;
        private DateTime _lastUpdate;

        [ObservableProperty]
        private string speedSentHuman;

        [ObservableProperty]
        private string speedReceivedHuman;

        public string InterfaceName { get; set; }
        public string PhysicalAddress { get; private set; }
        public long LastBytesSent { get; set; }
        public long LastBytesReceived { get; set; }

        //public string UploadSpeed => HumanReadableSpeed(_speedSent);
        //public string DownloadSpeed => HumanReadableSpeed(_speedReceived);

        public event EventHandler<NetSpeedEventArgs> SpeedChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        public NetSpeedItemVM(NetworkInterface networkInterface, int updateInterval = 1000, CancellationToken cancellationToken = default(CancellationToken))
        {
            _interface = networkInterface;
            _lastUpdate = DateTime.Now;

            InterfaceName = _interface.Name;
            PhysicalAddress = networkInterface.GetPhysicalAddress().ToString();

            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        if (cancellationToken.IsCancellationRequested)
                            return;

                        UpdateSpeed();
                        await Task.Delay(updateInterval);
                    }
                    catch (TaskCanceledException)
                    {
                        NLogHelper.WriteExceptionLog($"Task canceled.");

                        break;
                    }
                    catch (Exception e)
                    {
                        NLogHelper.WriteExceptionLog(e.ToString() + "Failed to update net speed.");
                    }
                }
            });
        }

        public override string ToString()
        {
            return $"{InterfaceName}({PhysicalAddress})";
        }

        private void UpdateSpeed()
        {
            // Check if the interface is up
            if (!_interface.OperationalStatus.Equals(OperationalStatus.Up))
            {
                NLogHelper.WriteLog($"Net interface {ToString()} is {_interface.OperationalStatus}");
                return;
            }

            // Get the current bytes sent and received
            var bytesSent = _interface.GetIPStatistics().BytesSent;
            var bytesReceived = _interface.GetIPStatistics().BytesReceived;
            NLogHelper.WriteLog($"Sent: {bytesSent}, Received: {bytesReceived}");

            // Calculate the speed
            var speedSent = (bytesSent - LastBytesSent) / (DateTime.Now - _lastUpdate).TotalSeconds;
            var speedReceived = (bytesReceived - LastBytesReceived) / (DateTime.Now - _lastUpdate).TotalSeconds;

            // Update the last bytes sent and received
            LastBytesSent = bytesSent;
            LastBytesReceived = bytesReceived;

            // Update the speed
            SpeedSentHuman = HumanReadableSpeed(speedSent);
            SpeedReceivedHuman = HumanReadableSpeed(speedReceived);

            // Update the last update time
            _lastUpdate = DateTime.Now;

            // Raise the speed changed event
            OnSpeedChanged();
        }

        protected virtual void OnSpeedChanged()
        {
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SpeedSent)));
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SpeedReceived)));
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UploadSpeed)));
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DownloadSpeed)));
            //SpeedChanged?.Invoke(this, new NetSpeedEventArgs(SpeedSent, SpeedReceived));
        }

        public static string HumanReadableSpeed(double bytesPerSecond)
        {
            if (bytesPerSecond < 1024)
            {
                return $"{bytesPerSecond:0.00} B/s";
            }
            else if (bytesPerSecond < 1024 * 1024)
            {
                return $"{(bytesPerSecond / 1024.0):0.00} KB/s";
            }
            else if (bytesPerSecond < 1024 * 1024 * 1024)
            {
                return $"{(bytesPerSecond / 1024.0 / 1024.0):0.00} MB/s";
            }
            else
            {
                return $"{(bytesPerSecond / 1024.0 / 1024.0 / 1024.0):0.00} GB/s";
            }
        }
    }

    public class NetSpeedEventArgs
    {
        public double SpeedSent { get; set; }
        public double SpeedReceived { get; set; }

        public NetSpeedEventArgs(double speedSent, double speedReceived)
        {
            SpeedSent = speedSent;
            SpeedReceived = speedReceived;
        }
    }
}