using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCL.Net;

namespace Inkubus.Engine
{
    using Physics;
    class PhysicsEngine
    {
        private Context context;
        private Device device;
        public PhysicsEngine()
        {
            ErrorCode err;
            Platform[] platforms = Cl.GetPlatformIDs(out err);
            List<Device> devices = new List<Device>();

            CheckErr(err, "GetPlatformIDs");

            foreach (Platform platform in platforms)
            {
                string platformName = Cl.GetPlatformInfo(platform, PlatformInfo.Name, out err).ToString();

                Console.WriteLine("Platform: " + platformName);
                CheckErr(err, "Cl.GetPlatformInfo");

                foreach (Device device in Cl.GetDeviceIDs(platform, DeviceType.Gpu, out err))
                {
                    CheckErr(err, "Cl.GetDeviceIDs");
                    Console.WriteLine("Device: " + device.ToString());
                    devices.Add(device);
                }
            }

            if (devices.Count <= 0)
            {
                Console.WriteLine("No OpenCL devices found.");
                System.Environment.Exit(1);
            }

            device = devices[0];

            if (Cl.GetDeviceInfo(device, DeviceInfo.ImageSupport, out err).CastTo<Bool>() == Bool.False)
            {
                Console.WriteLine("No image support.");
                System.Environment.Exit(1);
            }
            context = Cl.CreateContext(null, 1, new[] { device }, ContextNotify,
            IntPtr.Zero, out err);
            CheckErr(err, "Cl.CreateContext");
        }

        private void CheckErr(ErrorCode err, string name)
        {
            if (err != ErrorCode.Success)
            {
                Console.WriteLine("ERROR: " + name + " (" + err.ToString() + ")");
            }
        }
        private void ContextNotify(string errInfo, byte[] data, IntPtr cb, IntPtr userData)
        {
            Console.WriteLine("OpenCL Notification: " + errInfo);
        }
    }
}
