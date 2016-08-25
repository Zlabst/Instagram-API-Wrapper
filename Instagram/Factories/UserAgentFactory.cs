using System;

namespace Ninja.Instagram.API.Factories
{
    internal sealed class UserAgentFactory
    {
        private static string[] _versions;
        private static string[] _screens;
        private static string[] _models;
        private static Random rnd;

        static UserAgentFactory()
        {
            rnd = new Random();
            _screens = new string[]
            {
                "480dpi; 1080x1920",
                "326dpi; 750x1334",
                "326dpi; 640x1136",
                "326dpi; 640x960",
                "163dpi; 320x480",
                "538dpi; 1440x2560",
                "318dpi; 768x1280",
                "267dpi; 720x1280",
                "285dpi; 800x1280",
                "441dpi; 1080x1920",
                "445dpi; 1080x1920"
            };
            _versions = new string[]
            {
                "13/3.2.0",
                "13/3.2.1",
                "13/3.2.2",
                "14/4.0.1",
                "14/4.0.2",
                "15/4.0.3",
                "15/4.0.4",
                "16/4.1.0",
                "16/4.1.1",
                "16/4.1.2",
                "16/4.1.3",
                "17/4.2.0",
                "17/4.2.1",
                "17/4.2.2",
                "17/4.2.3",
                "18/4.3.0",
                "18/4.3.1",
                "18/4.3.2",
                "18/4.3.3",
                "19/4.4",
                "19/4.4.4",
                "21/5.0",
                "22/5.1",
                "23/6.0"
            };
            _models = new string[]
            {
                "GT-I9100",
                "GT-I9220",
                "GT-I5700",
                "GT-S5660",
                "GT-5550",
                "GT-I9003",
                "GT-S5830",
                "GT-S5830i",
                "GT-S5570",
                "SPH-M820",
                "SGH-T759",
                "GT-S5360",
                "SGH-T679",
                "GT-I9103",
                "GT-I8150",
                "GT-S5690",
                "GT-i9250"
            };
        }

        public static string GetRandom(string version)
        {
            return $"Instagram {version} Android ({_versions[rnd.Next(0, _versions.Length)]}; {_screens[rnd.Next(0, _screens.Length)]}; samsung; {_models[rnd.Next(0, _models.Length)]}; klte; qcom; en_US)";
        }
    }
}
