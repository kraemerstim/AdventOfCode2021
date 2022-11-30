using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;

namespace AdventOfCode2021.solutions
{
    public class Day16 : ProjectDay
    {
        public static long GetLongFromBitArray(List<bool> bits)
        {
            var returnValue = 0;
            var multiplier = 1;
            for (var i = bits.Count - 1; i >= 0; i--)
            {
                returnValue += bits[i] ? multiplier : 0;
                multiplier *= 2;
            }

            return returnValue;
        }

        public class PackageFactory
        {
            public static Package Create(List<bool> bits, Package predecessor)
            {
                var version = GetLongFromBitArray(bits.GetRange(0, 3));
                var type = GetLongFromBitArray(bits.GetRange(3, 3));
                var payLoad = bits.GetRange(6, bits.Count - 6);

                if (type == 4)
                {
                    return new ValuePackage(version, type, payLoad, predecessor);
                }
                else
                {
                    return new OparatorPackage(version, type, payLoad, predecessor);
                }
            }
        }

        public abstract class Package
        {
            private int _size;
            public Package Predecessor { get; init; }
            public long Version { get; }
            public long Type { get; }

            public abstract long GetVersionSum();

            public abstract long GetCalculatedValue();

            public int Size
            {
                get => _size + 6;
                protected init => _size = value;
            }

            public List<bool> Payload { get; }

            public Package(long version, long type, List<bool> payload)
            {
                Version = version;
                Type = type;
                Payload = payload;
            }
        }

        public class ValuePackage : Package
        {
            public long Value { get; }

            public ValuePackage(long version, long type, List<bool> payload, Package predecessor) : base(version, type, payload)
            {
                (Value, Size) = CalculateValue(payload);
                Predecessor = predecessor;
            }

            private (long finalValue, int Size) CalculateValue(List<bool> payload)
            {
                var index = 0;
                var numbers = new List<long>();
                do
                {
                    numbers.Add(GetLongFromBitArray(Payload.GetRange(index +1, 4)));
                    index += 5;
                } while (Payload[index-5]);

                var finalValue = 0l;
                var multiplier = 1l;
                for (int i = numbers.Count - 1; i >= 0; i--)
                {
                    finalValue += numbers[i] * multiplier;
                    multiplier *= 16;
                }
                
                return (finalValue, index);
            }

            public override long GetVersionSum()
            {
                return Version;
            }

            public override long GetCalculatedValue()
            {
                return Value;
            }
        }

        public class OparatorPackage : Package
        {
            public bool Identifier { get; }
            public long Length { get; }
            public List<Package> Children { get; }

            public OparatorPackage(long version, long type, List<bool> payload, Package predecessor) : base(version, type, payload)
            {
                Children = new List<Package>();
                Identifier = payload[0];
                Predecessor = predecessor;
                if (Identifier)
                {
                    Length = GetLongFromBitArray(payload.GetRange(1, 11));
                    var index = 12;
                    for (var i = 0; i < Length; i++)
                    {
                        var createdPackage = PackageFactory.Create(payload.GetRange(index, payload.Count - index), this);
                        Children.Add(createdPackage);
                        index += createdPackage.Size;
                        Size = index;
                    }
                }
                else
                {
                    Length = GetLongFromBitArray(payload.GetRange(1, 15));
                    var index = 16;
                    var remaining = (int)Length;
                    Size = (int)Length + 16;
                    while (remaining > 0)
                    {
                        var createdPackage = PackageFactory.Create(payload.GetRange(index, (int)remaining), this);
                        Children.Add(createdPackage);
                        remaining -= createdPackage.Size;
                        index += createdPackage.Size;
                    }
                }
            }

            public override long GetVersionSum()
            {
                return Children.Sum(package => package.GetVersionSum()) + Version;
            }

            public override long GetCalculatedValue()
            {
                return Type switch
                {
                    0 => Children.Sum(package => package.GetCalculatedValue()),
                    1 => Children.Aggregate<Package, long>(1, (current, child) => current * child.GetCalculatedValue()),
                    2 => Children.Min(package => package.GetCalculatedValue()),
                    3 => Children.Max(package => package.GetCalculatedValue()),
                    5 => Children[0].GetCalculatedValue() > Children[1].GetCalculatedValue() ? 1 : 0,
                    6 => Children[0].GetCalculatedValue() < Children[1].GetCalculatedValue() ? 1 : 0,
                    7 => Children[0].GetCalculatedValue() == Children[1].GetCalculatedValue() ? 1 : 0,
                    _ => throw new Exception("Yo Mr. White, that is not a valid type, bitch!")
                };
            }
        }


        public static List<bool> ConvertHexToBitArray(string hexData)
        {
            if (hexData == null)
                return null;

            var ba = new BitArray(4 * hexData.Length);
            for (int i = 0; i < hexData.Length; i++)
            {
                byte b = byte.Parse(hexData[i].ToString(), NumberStyles.HexNumber);
                for (int j = 0; j < 4; j++)
                {
                    ba.Set(i * 4 + j, (b & (1 << (3 - j))) != 0);
                }
            }

            return ba.Cast<bool>().ToList();
        }

        public override void Run()
        {
            var lines = AOCUtility.FileLines(@"resources/day16/input.txt");
            var bitsLists = lines.Select(ConvertHexToBitArray).ToList();


            foreach (var bitsList in bitsLists)
            {
                var outputString = "";
                foreach (bool bit in bitsList)
                {
                    outputString += bit ? "1" : "0";
                }

                Log($"{outputString}");

                var package = PackageFactory.Create(bitsList, null);
                if (package.GetType() == typeof(ValuePackage))
                {
                    Log($"Version = {package.Version}; Typ = {package.Type}; Value = {((ValuePackage)package).Value}, Size = {package.Size}");
                }
                Log($"Versionsum = {package.GetVersionSum()}");
                Log($"{package.GetVersionSum()}", Main.LogLevel.Result1);
                Log($"CalculatedValue = {package.GetCalculatedValue()}");
                Log($"{package.GetCalculatedValue()}", Main.LogLevel.Result2);
            }
        }
    }
}