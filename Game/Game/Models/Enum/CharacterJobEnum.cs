using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Models
{
    /// <summary>
    /// The Types of Jobs a character can have
    /// Used in Character Crudi, and in Battles.
    /// </summary>
    public enum CharacterJobEnum
    {
        // Not specified
        Unknown = 0,

        // Trainer with additional chance to capture the Pokemon
        PetLover = 10,

        // Trainer with special ability with Pokedex 
        DojoMaster = 12,

        // Trainer with 50% chance to do double damage
        QuickAttacker = 14

    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class CharacterJobEnumExtensions
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this CharacterJobEnum value)
        {
            // Default String
            var Message = "Player";

            switch (value)
            {
                case CharacterJobEnum.PetLover:
                    Message = "Pet Lover";
                    break;

                case CharacterJobEnum.DojoMaster:
                    Message = "Dojo Master";
                    break;

                case CharacterJobEnum.QuickAttacker:
                    Message = "Quick Attacker";
                    break;

                case CharacterJobEnum.Unknown:
                default:
                    break;
            }

            return Message;
        }
    }

    /// <summary>
    /// Helper for Character Jobs
    /// </summary>
    public static class CharacterJobEnumHelper
    {
        /// <summary>
        /// Gets the list of jobs that an Character can have.
        /// Not include the unknown
        /// </summary>
        public static List<string> GetList
        {
            get
            {
                var myList = Enum.GetNames(typeof(CharacterJobEnum)).ToList();
                var myReturn = myList.Where(a => a.ToString() != CharacterJobEnum.Unknown.ToString())
                                     .OrderBy(a => a)
                                     .ToList();
                return myReturn;
            }
        }

        /// <summary>
        /// Given the String for an enum, return its value. That allows for the enums to be numbered 2,4,6 rather than 1,2,3 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CharacterJobEnum ConvertStringToEnum(string value)
        {
            return (CharacterJobEnum)Enum.Parse(typeof(CharacterJobEnum), value);
        }

        /// <summary>
        /// If asked for a character job number, return a character job.
        /// This compsenstates for the enum #s not being sequential, 
        /// but allows for calls to the postion for random allocation etc... 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static CharacterJobEnum GetCharacterJobByPosition(int position)
        {
            switch (position)
            {
                case 1:
                    return CharacterJobEnum.PetLover;

                case 2:
                    return CharacterJobEnum.DojoMaster;

                case 3:
                default:
                    return CharacterJobEnum.QuickAttacker;
            }
        }
    }
}