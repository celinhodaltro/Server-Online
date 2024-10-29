﻿using System;
using System.Collections.Generic;

namespace OpenTibiaUnity.Core.Assets
{
    using ThingTypesDict = Dictionary<ushort, ThingType>;

    public enum ThingCategory : byte
    {
        Item = 0,
        Creature,
        Effect,
        Missile,
        InvalidCategory,
        LastCategory = InvalidCategory
    };

    public sealed class ContentData
    {
        IO.BinaryStream m_BinaryReader;
        int m_ClientVersion;

        public uint DatSignature { get; private set; }
        public ushort ContentRevision { get; private set; }
        public ThingTypesDict[] ThingTypeDictionaries { get; private set; } = new ThingTypesDict[(int)ThingCategory.LastCategory];

        public ContentData(byte[] buffer, int clientVersion) {
            m_BinaryReader = new IO.BinaryStream(buffer);
            m_ClientVersion = clientVersion;

            Parse();
        }

        private void Parse() {
            DatSignature = m_BinaryReader.ReadUnsignedInt();
            ContentRevision = (ushort)DatSignature;

            int[] counts = new int[(int)ThingCategory.LastCategory];
            for (int category = 0; category < (int)ThingCategory.LastCategory; category++) {
                int count = m_BinaryReader.ReadUnsignedShort() + 1;
                counts[category] = count;
            }

            for (int category = 0; category < (int)ThingCategory.LastCategory; category++) {
                ushort firstId = 1;
                if (category == (int)ThingCategory.Item) {
                    firstId = 100;
                }
                
                ThingTypeDictionaries[category] = new ThingTypesDict();
                for (ushort id = firstId; id < counts[category]; id++) {
                    ThingType thingType = new ThingType() {
                        ID = id,
                        Category = (ThingCategory)category,
                    };

                    thingType.Unserialize(m_BinaryReader, m_ClientVersion);
                    ThingTypeDictionaries[category][id] = thingType;
                }
            }
        }

        public static uint ClientVersionToDatSignature(int version) {
            switch (version) {
                case 770: return 0x439D5A33;
                case 1098: return 0x42A3;

                default: return 0;
            }
        }

        public byte[] ConvertTo(int newVersion) {
            var binaryWriter = new IO.BinaryStream();

            binaryWriter.WriteUnsignedInt(ClientVersionToDatSignature(newVersion));

            int[] counts = new int[(int)ThingCategory.LastCategory];
            for (int category = 0; category < (int)ThingCategory.LastCategory; category++) {
                int toWrite = ThingTypeDictionaries[category].Count;
                if (category == (int)ThingCategory.Item)
                    toWrite += 99;

                counts[category] = toWrite + 1;
                binaryWriter.WriteUnsignedShort((ushort)toWrite);
            }

            for (int category = 0; category < (int)ThingCategory.LastCategory; category++) {
                ushort firstId = 1;
                if (category == (int)ThingCategory.Item) {
                    firstId = 100;
                }
                
                for (ushort id = firstId; id < counts[category]; id++) {
                    ThingTypeDictionaries[category][id].Serialize(binaryWriter, m_ClientVersion, newVersion);
                }
            }

            return binaryWriter.GetBuffer();
        }
    }
}