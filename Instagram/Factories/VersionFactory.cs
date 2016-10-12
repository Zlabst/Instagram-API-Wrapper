using System;
using System.Collections.Generic;
using System.Linq;

namespace Ninja.Instagram.API.Factories
{
    internal sealed class VersionFactory
    {
        public static Dictionary<string, string> Versions
        {
            get
            {
                return _versions;
            }
        }

        private static Dictionary<string, string> _versions;
        private static Random _rnd;

        static VersionFactory()
        {
            _versions = new Dictionary<string, string>()
            {
                {"9b3b9e55988c954e51477da115c58ae82dcae7ac01c735b4443a3c5923cb593a", "8.0.0"},
                {"3d3d669cedc38f2ea7d198840e0648db3738224a0f661aa6a2c1e77dfa964a1e", "8.4.0"},
                {"b5d839444818714bdab3e288e6da9b515f85b000b6e6b452552bfd399cb56cf0", "8.5.1"},
                {"012a54f51c49aa8c5c322416ab1410909add32c966bbaa0fe3dc58ac43fd7ede", "9.2.0"}
            };
            _rnd = new Random(Environment.TickCount);
        }

        public static KeyValuePair<string, string> GetRandom()
        {
            return _versions.ElementAt(_rnd.Next(0, _versions.Count));
        }
    }
}
