﻿using OpenTibiaUnity.Core.Metaflags;
using System;
using System.Collections.Generic;

namespace OpenTibiaUnity.Core.Assets
{
    public enum FrameGroupType : byte
    {
        Idle = 0,
        Moving = 1,
        Default = Idle,
    }

    public class FrameGroupDuration
    {
        public int Minimum { get; set; }
        public int Maximum { get; set; }
    }


    public sealed class FrameGroupAnimator
    {
        public byte AnimationPhases { get; private set; } = 0;
        public bool Async { get; private set; } = false;
        public int LoopCount { get; private set; } = 0;
        public sbyte StartPhase { get; private set; } = -1;
        public List<FrameGroupDuration> FrameGroupDurations { get; private set; } = new List<FrameGroupDuration>();
        
        public static void SerializeLegacy(ThingType thingType, IO.BinaryStream binaryWriter, int startPhase, int phasesLimit) {
            binaryWriter.WriteUnsignedByte(1);
            binaryWriter.WriteInt(thingType.HasAttribute(AttributesUniform.AnimateAlways) || thingType.Category == ThingCategory.Item ? 0 : 1);
            binaryWriter.WriteUnsignedByte(0);

            int duration;
            if (thingType.Category == ThingCategory.Effect)
                duration = 75;
            else
                duration = phasesLimit > 0 ? 1000 / phasesLimit : 40;

            for (int i = 0; i < phasesLimit; i++) {
                binaryWriter.WriteInt(duration); // force legacy animation
                binaryWriter.WriteInt(duration);
            }
        }

        public void Serialize(IO.BinaryStream binaryWriter, int startPhase, int phasesLimit) {
            binaryWriter.WriteUnsignedByte(Async ? (byte)1 : (byte)0);
            binaryWriter.WriteInt(LoopCount);

            int minPhase = startPhase;
            int maxPhase = startPhase = phasesLimit;
            if (StartPhase > 0 && (StartPhase < minPhase || StartPhase > maxPhase))
                binaryWriter.WriteSignedByte((sbyte)minPhase);
            else
                binaryWriter.WriteSignedByte(StartPhase);

            for (int i = 0; i < phasesLimit; i++) {
                var frameGroupDuration = FrameGroupDurations[startPhase + i];
                binaryWriter.WriteInt(frameGroupDuration.Minimum);
                binaryWriter.WriteInt(frameGroupDuration.Maximum);
            }
        }

        public void Unserialize(byte animationPhases, IO.BinaryStream binaryReader) {
            AnimationPhases = animationPhases;
            Async = binaryReader.ReadUnsignedByte() == 0;
            LoopCount = binaryReader.ReadInt();
            StartPhase = binaryReader.ReadSignedByte();

            for (int i = 0; i < animationPhases; i++) {
                var duration = new FrameGroupDuration();
                duration.Minimum = binaryReader.ReadInt();
                duration.Maximum = binaryReader.ReadInt();

                FrameGroupDurations.Add(duration);
            }
        }
    }

    public sealed class FrameGroup
    {
        public byte Width { get; private set; }
        public byte Height { get; private set; }
        public byte ExactSize { get; private set; }
        public byte Layers { get; private set; }
        public byte PatternWidth { get; private set; }
        public byte PatternHeight { get; private set; }
        public byte PatternDepth { get; private set; }
        public byte Phases { get; private set; }
        public FrameGroupAnimator Animator { get; private set; }
        public List<uint> Sprites { get; private set; } = new List<uint>();

        public void Serialize(ThingType thingType, IO.BinaryStream binaryWriter, int fromVersion, int newVersion, sbyte startPhase, byte phasesLimit) {
            binaryWriter.WriteUnsignedByte(Width);
            binaryWriter.WriteUnsignedByte(Height);
            if (Width > 1 || Height > 1)
                binaryWriter.WriteUnsignedByte(ExactSize);

            binaryWriter.WriteUnsignedByte(Layers);
            binaryWriter.WriteUnsignedByte(PatternWidth);
            binaryWriter.WriteUnsignedByte(PatternHeight);
            if (newVersion >= 755)
                binaryWriter.WriteUnsignedByte(PatternDepth);
            
            binaryWriter.WriteUnsignedByte(phasesLimit);

            if (fromVersion < 1050) {
                if (phasesLimit > 1 && newVersion >= 1050)
                    FrameGroupAnimator.SerializeLegacy(thingType, binaryWriter, startPhase, phasesLimit);
            } else {
                if (phasesLimit > 1 && newVersion >= 1050)
                    Animator.Serialize(binaryWriter, startPhase, phasesLimit);
            }
            
            int spritesPerPhase = Width * Height * Layers * PatternWidth * PatternHeight * PatternDepth;
            int totalSprites = phasesLimit * spritesPerPhase;
            int offset = startPhase * spritesPerPhase;
            for (int j = 0; j < totalSprites; j++) {
                uint spriteId = Sprites[offset + j];
                if (newVersion >= 960)
                    binaryWriter.WriteUnsignedInt(spriteId);
                else
                    binaryWriter.WriteUnsignedShort((ushort)spriteId);
            }
        }

        public void Unserialize(IO.BinaryStream binaryReader, int clientVersion) {
            Width = binaryReader.ReadUnsignedByte();
            Height = binaryReader.ReadUnsignedByte();
            if (Width > 1 || Height > 1)
                ExactSize = binaryReader.ReadUnsignedByte();
            else
                ExactSize = 32;

            Layers = binaryReader.ReadUnsignedByte();
            PatternWidth = binaryReader.ReadUnsignedByte();
            PatternHeight = binaryReader.ReadUnsignedByte();
            PatternDepth = clientVersion >= 755 ? binaryReader.ReadUnsignedByte() : (byte)1;
            Phases = binaryReader.ReadUnsignedByte();

            if (Phases > 1 && clientVersion >= 1050) {
                Animator = new FrameGroupAnimator();
                Animator.Unserialize(Phases, binaryReader);
            }

            int totalSprites = Width * Height * Layers * PatternWidth * PatternHeight * PatternDepth * Phases;
            for (int j = 0; j < totalSprites; j++)
                Sprites.Add(clientVersion >= 960 ? binaryReader.ReadUnsignedInt() : binaryReader.ReadUnsignedShort());
        }
    }
}
