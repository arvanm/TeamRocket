using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Models
{
    /// <summary>
    /// The Types of Jobs a character can have
    /// Used in Character Crudi, and in Battles.
    /// </summary>
    public enum MonsterTypeEnum
    {
        // Not specified
        Unknown = 0,

        // Trainer with additional chance to capture the Pokemon
        Fire = 10,

        // Trainer with special ability with Pokedex 
        Water = 12,

        // Trainer with 50% chance to do double damage
        Posion = 14

    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class MonsterTypeEnumExtensions
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this MonsterTypeEnum value)
        {
            // Default String
            var Message = "Player";

            switch (value)
            {
                case MonsterTypeEnum.Fire:
                    Message = "Pet Lover";
                    break;

                case MonsterTypeEnum.Water:
                    Message = "Dojo Master";
                    break;

                case MonsterTypeEnum.Posion:
                    Message = "Quick Attacker";
                    break;

                case MonsterTypeEnum.Unknown:
                default:
                    break;
            }

            return Message;
        }
    }

    /// <summary>
    /// Helper for Character Jobs
    /// </summary>
    public static class MonsterTypeEnumHelper
    {
        /// <summary>
        /// Gets the list of jobs that a Character can have.
        /// Not include the unknown
        /// </summary>
        public static List<string> GetList
        {
            get
            {
                var myList = Enum.GetNames(typeof(MonsterTypeEnum)).ToList();
                var myReturn = myList.Where(a => a.ToString() != MonsterTypeEnum.Unknown.ToString())
                                     .OrderBy(a => a)
                                     .ToList();
                return myReturn;
            }
        }

        /// <summary>
        /// Gets the list of Message strings of jobs that a Character can have.
        /// Not include the unknown
        /// </summary>
        public static List<string> GetListMessage
        {
            get
            {
                var myList = new List<string>();
                foreach (MonsterTypeEnum job in Enum.GetValues(typeof(MonsterTypeEnum)))
                {
                    if (job != MonsterTypeEnum.Unknown)
                    {
                        myList.Add(job.ToMessage());
                    }
                }

                return myList;
            }
        }

        /// <summary>
        /// Given the String for an enum, return its value. That allows for the enums to be numbered 2,4,6 rather than 1,2,3 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MonsterTypeEnum ConvertStringToEnum(string value)
        {
            return (MonsterTypeEnum)Enum.Parse(typeof(MonsterTypeEnum), value);
        }

        /// <summary>
        /// Given the Message for an enum
        /// Look it up and return the enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MonsterTypeEnum ConvertMessageToEnum(string value)
        {
            // Get the Message, Determine Which enum has that message, and return that enum.
            foreach (MonsterTypeEnum job in Enum.GetValues(typeof(MonsterTypeEnum)))
            {
                if (job.ToMessage().Equals(value))
                {
                    return job;
                }
            }
            return MonsterTypeEnum.Unknown;
        }

        /// <summary>
        /// If asked for a character job number, return a character job.
        /// This compsenstates for the enum #s not being sequential, 
        /// but allows for calls to the postion for random allocation etc... 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static MonsterTypeEnum GetCharacterJobByPosition(int position)
        {
            switch (position)
            {
                case 1:
                    return MonsterTypeEnum.Fire;

                case 2:
                    return MonsterTypeEnum.Water;

                case 3:
                default:
                    return MonsterTypeEnum.Posion;
            }
        }
    }
}