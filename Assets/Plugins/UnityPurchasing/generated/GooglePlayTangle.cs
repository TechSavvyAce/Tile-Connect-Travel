#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("MXBl5XAKt0U0KTSFmSZglnGN/HZBiZ0hm/9jTGmt2eePWgRFoqNvhJc4LSGdMyGl7X3FhCvmXLK3DqiyAn6q04MWHcYuYGwfLzxsgXDH8pKN6g9d1A4Dmi7hxsN/UICmdCscipjfhn+Ujjs81SI7nyaKSOY1TyEGvD8xPg68PzQ8vD8/Ppw6T9ZX1JhuIwWA5xqjDUnkGNLoIHBC0pqrhttIbt8EXUKTNNiRhpIKS/RYLiytDrw/HA4zODcUuHa4yTM/Pz87Pj2fYcGSWivg3lg9w2BlhWvKG4PIgHKOL6+tqZ4TZzC4ZST4zsCFWC4yX59qFcbARYeG8WerR74uyZmnxhcfkdsMyjlevsP/1DUY97AxcByZJcWJKpanqtWfozw9Pz4/");
        private static int[] order = new int[] { 4,11,12,10,13,5,11,12,11,13,12,13,12,13,14 };
        private static int key = 62;

        public static byte[] Data() {
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
