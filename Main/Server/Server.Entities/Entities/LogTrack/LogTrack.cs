using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Entities
{
    public class LogTrack : DefaultDb
    {
        public static explicit operator String(LogTrack logTrack) => logTrack.Message;
        public static explicit operator LogTrack(string Message) => new LogTrack(Message);
        public LogTrack(LogLevelEnum level, string message, string details)
        {
            Level = level;
            Message = message;
            Details = details;
        }

        public LogTrack(string message)
        {
            Message = message;
        }

        public LogLevelEnum Level { get; set; } = LogLevelEnum.Info;
        public string Message { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;

        public override bool Validate()
        {
            if (Level.Equals(null))
                throw new InvalidDataException("Level is Required");
            if (Message.Equals(String.Empty))
                throw new InvalidDataException("Message is Required");
            if (Details.Equals(String.Empty))
                throw new InvalidDataException("Details is Required");
            return base.Validate();
        }
    }

    public enum LogLevelEnum
    {
        Info = 0,
        Warn = 1,
        Error = 2,
        Fatal = 3
    }
}
