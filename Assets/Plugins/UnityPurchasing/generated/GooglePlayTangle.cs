#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("Ngq+76IlLuusWoRgtZubsK4PN7vNZvwziwk43v+K0ZcEiMMppUzsqZIgo4CSr6SriCTqJFWvo6Ojp6KhFswBuOz1KmfqAt1MijK4OXAyrGi/OY1Z5xM8Qr8IXaXY2ZX/Vw+yxc0wsuBhuDRct15sc48r/0mfWAMIjuRf2304+sHZrLl5TsfFyhxBnk090TXd6b3gpa0Sw49iav00gimBqr4oxtRnvsC+EMHIl5I4BHODZs2IVpWdjK566Ll/sOdE/FzNCFxblMQgo62ikiCjqKAgo6OiJ9AGIQvzQhFIsVLgDJ3DrkGIpHFoF1TpxL9T9XoYi0JIG0KDEeXzBseCMEiGLbP5Dwa0byV6f3ACkwFBWtgJXP3DgV7RY3KyyX4w4aCho6Kj");
        private static int[] order = new int[] { 13,6,13,8,12,9,13,12,12,11,13,11,12,13,14 };
        private static int key = 162;

        public static byte[] Data() {
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
