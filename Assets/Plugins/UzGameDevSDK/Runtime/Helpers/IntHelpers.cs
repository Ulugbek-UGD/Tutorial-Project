using System;

namespace UzGameDev.Helpers
{
    public static class IntHelpers
    {
        //============================================================
        /// <summary>
        /// Возвращает минимальное число из двух
        /// </summary>
        /// <param name="a">любое число</param>
        /// <param name="b">любое число</param>
        /// <returns>int – минимальный</returns>
        public static int MinimumOf(int a, int b)
        {
            return a < b ? a : b;
        }
        //============================================================
        /// <summary>
        /// Возвращает остаток от price
        /// </summary>
        /// <param name="pay">плата, при котором "price" будет отнять</param>
        /// <param name="price">цена, любое число</param>
        /// <returns>int – оставшийся</returns>
        public static int RemainderOf(int pay, int price)
        {
            if (pay < price) return 0;
            var remainder = pay - price;
            return remainder;
        }
        //============================================================
        /// <summary>
        /// Возвращает последний индекс перечисления
        /// </summary>
        /// <typeparam name="T">Enum</typeparam>
        /// <returns>int – последний индекс перечисления</returns>
        public static int LastEnumIndex<T>() where T: Enum
        {
            return Enum.GetValues(typeof(T)).Length -1;
        }
        //============================================================
    }
}