using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using Random = java.util.Random;

public class SupportClass
{
    /// <summary>
    /// Writes the exception stack trace to the received stream
    /// </summary>
    /// <param name="throwable">Exception to obtain information from</param>
    /// <param name="stream">Output sream used to write to</param>
    public static void WriteStackTrace(Exception throwable, TextWriter stream)
    {
        stream.Write(throwable.StackTrace);
        stream.Flush();
    }

    /*******************************/

    /// <summary>
    /// Creates a new positive random number 
    /// </summary>
    /// <param name="random">The last random obtained</param>
    /// <returns>Returns a new positive random number</returns>
    public static long NextLong(Random random)
    {
        return random.nextLong();
    }

    /*******************************/

    /// <summary>
    /// This method returns the literal value received
    /// </summary>
    /// <param name="literal">The literal to return</param>
    /// <returns>The received value</returns>
    public static long Identity(long literal)
    {
        return literal;
    }

    /// <summary>
    /// This method returns the literal value received
    /// </summary>
    /// <param name="literal">The literal to return</param>
    /// <returns>The received value</returns>
    public static ulong Identity(ulong literal)
    {
        return literal;
    }

    /// <summary>
    /// This method returns the literal value received
    /// </summary>
    /// <param name="literal">The literal to return</param>
    /// <returns>The received value</returns>
    public static float Identity(float literal)
    {
        return literal;
    }

    /// <summary>
    /// This method returns the literal value received
    /// </summary>
    /// <param name="literal">The literal to return</param>
    /// <returns>The received value</returns>
    public static double Identity(double literal)
    {
        return literal;
    }

    /*******************************/


    /*******************************/

    /// <summary>
    /// Converts an array of sbytes to an array of bytes
    /// </summary>
    /// <param name="sbyteArray">The array of sbytes to be converted</param>
    /// <returns>The new array of bytes</returns>
    public static byte[] ToByteArray(sbyte[] sbyteArray)
    {
        byte[] byteArray = null;

        if (sbyteArray != null)
        {
            byteArray = new byte[sbyteArray.Length];
            for (int index = 0; index < sbyteArray.Length; index++)
                byteArray[index] = (byte) sbyteArray[index];
        }
        return byteArray;
    }

    /// <summary>
    /// Converts a string to an array of bytes
    /// </summary>
    /// <param name="sourceString">The string to be converted</param>
    /// <returns>The new array of bytes</returns>
    public static byte[] ToByteArray(string sourceString)
    {
        return Encoding.UTF8.GetBytes(sourceString);
    }

    /// <summary>
    /// Converts a array of object-type instances to a byte-type array.
    /// </summary>
    /// <param name="tempObjectArray">Array to convert.</param>
    /// <returns>An array of byte type elements.</returns>
    public static byte[] ToByteArray(Object[] tempObjectArray)
    {
        byte[] byteArray = null;
        if (tempObjectArray != null)
        {
            byteArray = new byte[tempObjectArray.Length];
            for (int index = 0; index < tempObjectArray.Length; index++)
                byteArray[index] = (byte) tempObjectArray[index];
        }
        return byteArray;
    }


    /*******************************/

    /// <summary>
    /// Performs an unsigned bitwise right shift with the specified number
    /// </summary>
    /// <param name="number">Number to operate on</param>
    /// <param name="bits">Ammount of bits to shift</param>
    /// <returns>The resulting number from the shift operation</returns>
    public static int URShift(int number, int bits)
    {
        if (number >= 0)
            return number >> bits;
        else
            return (number >> bits) + (2 << ~bits);
    }

    /// <summary>
    /// Performs an unsigned bitwise right shift with the specified number
    /// </summary>
    /// <param name="number">Number to operate on</param>
    /// <param name="bits">Ammount of bits to shift</param>
    /// <returns>The resulting number from the shift operation</returns>
    public static int URShift(int number, long bits)
    {
        return URShift(number, (int) bits);
    }

    /// <summary>
    /// Performs an unsigned bitwise right shift with the specified number
    /// </summary>
    /// <param name="number">Number to operate on</param>
    /// <param name="bits">Ammount of bits to shift</param>
    /// <returns>The resulting number from the shift operation</returns>
    public static long URShift(long number, int bits)
    {
        if (number >= 0)
            return number >> bits;
        else
            return (number >> bits) + (2L << ~bits);
    }

    /// <summary>
    /// Performs an unsigned bitwise right shift with the specified number
    /// </summary>
    /// <param name="number">Number to operate on</param>
    /// <param name="bits">Ammount of bits to shift</param>
    /// <returns>The resulting number from the shift operation</returns>
    public static long URShift(long number, long bits)
    {
        return URShift(number, (int) bits);
    }

    /*******************************/

    /// <summary>Reads a number of characters from the current source Stream and writes the data to the target array at the specified index.</summary>
    /// <param name="sourceStream">The source Stream to read from.</param>
    /// <param name="target">Contains the array of characteres read from the source Stream.</param>
    /// <param name="start">The starting index of the target array.</param>
    /// <param name="count">The maximum number of characters to read from the source Stream.</param>
    /// <returns>The number of characters read. The number will be less than or equal to count depending on the data available in the source Stream. Returns -1 if the end of the stream is reached.</returns>
    public static Int32 ReadInput(Stream sourceStream, sbyte[] target, int start, int count)
    {
        // Returns 0 bytes if not enough space in target
        if (target.Length == 0)
            return 0;

        var receiver = new byte[target.Length];
        int bytesRead = sourceStream.Read(receiver, start, count);

        // Returns -1 if EOF
        if (bytesRead == 0)
            return -1;

        for (int i = start; i < start + bytesRead; i++)
            target[i] = (sbyte) receiver[i];

        return bytesRead;
    }

    /// <summary>Reads a number of characters from the current source TextReader and writes the data to the target array at the specified index.</summary>
    /// <param name="sourceTextReader">The source TextReader to read from</param>
    /// <param name="target">Contains the array of characteres read from the source TextReader.</param>
    /// <param name="start">The starting index of the target array.</param>
    /// <param name="count">The maximum number of characters to read from the source TextReader.</param>
    /// <returns>The number of characters read. The number will be less than or equal to count depending on the data available in the source TextReader. Returns -1 if the end of the stream is reached.</returns>
    public static Int32 ReadInput(TextReader sourceTextReader, sbyte[] target, int start, int count)
    {
        // Returns 0 bytes if not enough space in target
        if (target.Length == 0) return 0;

        var charArray = new char[target.Length];
        int bytesRead = sourceTextReader.Read(charArray, start, count);

        // Returns -1 if EOF
        if (bytesRead == 0) return -1;

        for (int index = start; index < start + bytesRead; index++)
            target[index] = (sbyte) charArray[index];

        return bytesRead;
    }

    /*******************************/

    /// <summary>
    /// Verifies if a value exist in a NameValueCollection.
    /// </summary>
    /// <param name="collection">The NameValueCollection to look in.</param>
    /// <param name="key">The key to look for.</param>
    /// <returns>If key exist in the NameValueCollection returns true, otherwise false.</returns>
    public static bool ContainsKeySupport(NameValueCollection collection, string key)
    {
        bool exists = false;
        if (collection != null)
        {
            string[] keys = collection.AllKeys;
            exists = !(Array.IndexOf(keys, key) == -1);
        }
        return exists;
    }

    #region Nested type: ArraySupport

    /// <summary>
    /// This class manages array operations.
    /// </summary>
    public class ArraySupport
    {
        /// <summary>
        /// Compares the entire members of one array whith the other one.
        /// </summary>
        /// <param name="array1">The array to be compared.</param>
        /// <param name="array2">The array to be compared with.</param>
        /// <returns>True if both arrays are equals otherwise it returns false.</returns>
        /// <remarks>Two arrays are equal if they contains the same elements in the same order.</remarks>
        public static bool Equals(Array array1, Array array2)
        {
            bool result = false;
            if ((array1 == null) && (array2 == null))
                result = true;
            else if ((array1 != null) && (array2 != null))
            {
                if (array1.Length == array2.Length)
                {
                    int length = array1.Length;
                    result = true;
                    for (int index = 0; index < length; index++)
                    {
                        if (!(array1.GetValue(index).Equals(array2.GetValue(index))))
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Fills the array with an specific value from an specific index to an specific index.
        /// </summary>
        /// <param name="array">The array to be filled.</param>
        /// <param name="fromindex">The first index to be filled.</param>
        /// <param name="toindex">The last index to be filled.</param>
        /// <param name="val">The value to fill the array with.</param>
        public static void Fill(Array array, Int32 fromindex, Int32 toindex, Object val)
        {
            Object Temp_Object = val;
            Type elementtype = array.GetType().GetElementType();
            if (elementtype != val.GetType())
                Temp_Object = Convert.ChangeType(val, elementtype);
            if (array.Length == 0)
                throw (new NullReferenceException());
            if (fromindex > toindex)
                throw (new ArgumentException());
            if ((fromindex < 0) || (array).Length < toindex)
                throw (new IndexOutOfRangeException());
            for (int index = (fromindex > 0) ? fromindex-- : fromindex; index < toindex; index++)
                array.SetValue(Temp_Object, index);
        }

        /// <summary>
        /// Fills the array with an specific value.
        /// </summary>
        /// <param name="array">The array to be filled.</param>
        /// <param name="val">The value to fill the array with.</param>
        public static void Fill(Array array, Object val)
        {
            Fill(array, 0, array.Length, val);
        }
    }

    #endregion
}