using System.Collections.Generic;

using Game.Models;
using Game.ViewModels;

namespace Game.GameRules
{
    public static class DefaultData
    {
        /// <summary>
        /// Load the Default data
        /// </summary>
        /// <returns></returns>
        public static List<ItemModel> LoadData(ItemModel temp)
        {
            var datalist = new List<ItemModel>()
            {
                new ItemModel {
                    Name = "PrimaryHand01",
                    Description = "May the force be with you!",
                    ImageURI = "item_sword.png",
                    Range = 5,
                    Damage = 10,
                    Value = 9,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack
                },
                new ItemModel {
                    Name = "PrimaryHand02",
                    Description = "A sharp sword",
                    ImageURI = "item_sword.png",
                    Range = 2,
                    Damage = 15,
                    Value = 20,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack
                },
                new ItemModel {
                    Name = "Head01",
                    Description = "Cute aviator hat",
                    ImageURI = "item_hat.png",
                    Value = 9,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense
                },
                new ItemModel {
                    Name = "Head02",
                    Description = "A hat that looks like a nest",
                    ImageURI = "item_hat.png",
                    Value = 2,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense
                },
                new ItemModel {
                    Name = "Neck01",
                    Description = "Shiny Wakanda Forever necklace",
                    ImageURI = "item_necklace.png",
                    Value = 9,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Speed
                },
                new ItemModel {
                    Name = "Neck02",
                    Description = "Warm red light",
                    ImageURI = "item_necklace.png",
                    Value = 15,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Defense
                },
                new ItemModel {
                    Name = "OffHand01",
                    Description = "Some nice glove",
                    ImageURI = "item_glove.png",
                    Value = 9,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense
                },
                new ItemModel {
                    Name = "OffHand02",
                    Description = "Paper glove",
                    ImageURI = "item_glove.png",
                    Value = 1,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Defense
                },
                new ItemModel {
                    Name = "Finger01",
                    Description = "Green Power ring",
                    ImageURI = "item_ring.png",
                    Value = 9,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.Attack
                },
                new ItemModel {
                    Name = "Finger02",
                    Description = "You feel very light with the ring",
                    ImageURI = "item_ring.png",
                    Value = 15,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.Speed
                },
                new ItemModel {
                    Name = "Feet01",
                    Description = "Nike Air Max",
                    ImageURI = "item_airmax.png",
                    Value = 9,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed
                },
                new ItemModel {
                    Name = "Feet02",
                    Description = "Boot your trip",
                    ImageURI = "item_airmax.png",
                    Value = 9,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Defense
                },
                new ItemModel {
                    Name = "Pokeball01",
                    Description = "Master ball",
                    ImageURI = "item_pokeball.png",
                    Damage = 15,
                    Value = 9,
                    Location = ItemLocationEnum.Pokeball,
                    Attribute = AttributeEnum.Attack
                },
                new ItemModel {
                    Name = "Pokeball02",
                    Description = "Newbie ball",
                    ImageURI = "item_pokeball.png",
                    Damage = 1,
                    Value = 1,
                    Location = ItemLocationEnum.Pokeball,
                    Attribute = AttributeEnum.Attack
                },
            };

            return datalist;
        }

        /// <summary>
        /// Load Example Scores
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<ScoreModel> LoadData(ScoreModel temp)
        {
            var datalist = new List<ScoreModel>()
            {
                new ScoreModel {
                    Name = "Trainer Bob",
                    Description = "Bob the Pokemon master",
                },

                new ScoreModel {
                    Name = "Trainer Tom",
                    Description = "I should be one number!",
                }
            };

            return datalist;
        }

        /// <summary>
        /// Load Characters
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<CharacterModel> LoadData(CharacterModel temp)
        {
            var HeadString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Head);
            var NecklassString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Necklass);
            var PrimaryHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.PrimaryHand);
            var OffHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.OffHand);
            var FeetString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Feet);
            var RightFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);
            var LeftFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);
            var PokeballString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Pokeball);

            var datalist = new List<CharacterModel>()
            {
                new CharacterModel {
                    Name = "Jessie",
                    Description = "The first Team Rocket member",
                    Level = 5,
                    MaxHealth = 49,
                    CurrentHealth = 40,
                    Job = CharacterJobEnum.DojoMaster,
                    ImageURI = "character_01.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                    Pokeball = PokeballString,
                    Pokedex = new List<MonsterModel>()
                    {
                        RandomPlayerHelper.GetRandomMonster(20),
                        RandomPlayerHelper.GetRandomMonster(20),
                        RandomPlayerHelper.GetRandomMonster(20)
                    }
                },

                new CharacterModel {
                    Name = "Giovanni",
                    Description = "An exper in ground-type Pokémon",
                    Level = 10,
                    MaxHealth = 98,
                    CurrentHealth = 40,
                    Job = CharacterJobEnum.DojoMaster,
                    ImageURI = "character_02.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                    Pokeball = PokeballString,
                },

                new CharacterModel {
                    Name = "Gideon",
                    Description = "Head team rocket scientist",
                    Level = 1,
                    MaxHealth = 5,
                    CurrentHealth = 4,
                    Job = CharacterJobEnum.QuickAttacker,
                    ImageURI = "character_03.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                    Pokeball = PokeballString,
                    Pokedex = new List<MonsterModel>()
                },

                new CharacterModel {
                    Name = "Archer",
                    Description = "Team rocket admin",
                    Level = 8,
                    MaxHealth = 15,
                    CurrentHealth = 13,
                    Job = CharacterJobEnum.QuickAttacker,
                    ImageURI = "character_04.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                    Pokeball = PokeballString,
                },

                new CharacterModel {
                    Name = "Proton",
                    Description = "Most ruthless of the trainer",
                    Level = 20,
                    MaxHealth = 150,
                    CurrentHealth = 150,
                    Job = CharacterJobEnum.PetLover,
                    ImageURI = "character_02.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                    Pokeball = PokeballString,
                },

                new CharacterModel {
                    Name = "Petrel",
                    Description = "Master of disguise",
                    Level = 15,
                    MaxHealth = 132,
                    CurrentHealth = 131,
                    Job = CharacterJobEnum.PetLover,
                    ImageURI = "character_03.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    PrimaryHand = PrimaryHandString,
                    OffHand = OffHandString,
                    Feet = FeetString,
                    RightFinger = RightFingerString,
                    LeftFinger = LeftFingerString,
                    Pokeball = PokeballString,
                },

                new CharacterModel {
                    Name = "Ariana",
                    Description = "Interim leader",
                    Level = 2,
                    MaxHealth = 13,
                    CurrentHealth = 12,
                    Job = CharacterJobEnum.PetLover,
                    ImageURI = "character_01.png",
                    Head = HeadString,
                    Necklass = NecklassString,
                    Pokeball = PokeballString,
                },

                new CharacterModel {
                    Name = "Poor",
                    Description = "Has Nothing",
                    Level = 1,
                    MaxHealth = 10,
                    CurrentHealth = 10,
                    Job = CharacterJobEnum.PetLover,
                    ImageURI = "character_01.png",
                    OffHand = OffHandString,
                },
            };

            return datalist;
        }

        /// <summary>
        /// Load Characters
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<MonsterModel> LoadData(MonsterModel temp)
        {
            var datalist = new List<MonsterModel>()
            {
                new MonsterModel {
                    Name = "Squirtle",
                    Description = "Squirtle is a small Pokémon that resembles a light-blue turtle",
                    ImageURI = "squirtle.png",
                    MonsterType = MonsterTypeEnum.Water,
                    Attack = 5,
                    SpecialAttack = 10,
                    Defense = 10,
                    Speed = 10
                },

                new MonsterModel {
                    Name = "Golduck",
                    Description = "Golduck is a blue, bipedal, platypus-like Pokémon.",
                    ImageURI = "golduck.png",
                    MonsterType = MonsterTypeEnum.Water,
                    Attack = 8,
                    SpecialAttack = 9,
                    Defense = 50,
                    Speed = 1
                },

                new MonsterModel {
                    Name = "Vaporeon",
                    Description = "Vaporeon is a Pokémon that shares physical traits with both aquatic and land animals.",
                    ImageURI = "vaporeon.png",
                    MonsterType = MonsterTypeEnum.Water,
                    Attack = 30,
                    SpecialAttack = 35,
                    Defense = 3,
                    Speed = 3
                },

                new MonsterModel {
                    Name = "Charmander",
                    Description = "Charmander is a bipedal, reptilian Pokémon with a primarily orange body and blue eyes.",
                    ImageURI = "charmander.png",
                    MonsterType = MonsterTypeEnum.Fire,
                    Attack = 10,
                    SpecialAttack = 15,
                    Defense = 1,
                    Speed = 10
                },

                new MonsterModel {
                    Name = "Entei",
                    Description = "Entei is a massive, leonine, quadrupedal Pokémon with some mastiff qualities.",
                    ImageURI = "entei.png",
                    MonsterType = MonsterTypeEnum.Fire,
                    Attack = 20,
                    SpecialAttack = 20,
                    Defense = 10,
                    Speed = 5
                },

                new MonsterModel {
                    Name = "Moltres",
                    Description = "Moltres is a large, avian Pokémon with golden plumage.",
                    ImageURI = "moltres.png",
                    MonsterType = MonsterTypeEnum.Fire,
                    Attack = 5,
                    SpecialAttack = 15,
                    Defense = 2,
                    Speed = 2
                },

                new MonsterModel {
                    Name = "Beedrill",
                    Description = "Beedrill is a Pokémon which mostly resembles a bipedal, yellow wasp",
                    ImageURI = "beedrill.png",
                    MonsterType = MonsterTypeEnum.Poison,
                    Attack = 5,
                    SpecialAttack = 30,
                    Defense = 0,
                    Speed = 50
                },
            };

            return datalist;
        }
    }
}