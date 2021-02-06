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
    }
}