using NUnit.Framework;

using Game.Services;
using Game.Models;
using Game.Helpers;
using System;
using System.Linq;
using System.Collections.Generic;

namespace UnitTests.Helpers
{
    [TestFixture]
    public class CharacterJobEnumHelperTests
    {

        [Test]
        public void CharacterJobEnumHelper_CharacterJobEnum_GetList_Should_Pass()
        {
            // Arrange

            // Act
            var myDataList = CharacterJobEnumHelper.GetList;
            var myExpectedList = new List<string>()
            {
                "DojoMaster",
                "PetLover",
                "QuickAttacker"
            };

            // Reset

            // Assert
            // Make sure each item is in the list
            foreach (var item in myDataList)
            {
                var found = false;
                foreach (var expected in myExpectedList)
                {
                    if (item == expected)
                    {
                        found = true;
                        break;
                    }
                }

                // Assert
                Assert.AreEqual(true, found, "item : " + item + TestContext.CurrentContext.Test.Name);
            }

            // reverse it, to make sure the list has each item
            // Make sure each item is in the list
            foreach (var expected in myExpectedList)
            {
                var found = false;
                {
                    foreach (var item in myDataList)
                        if (item == expected)
                        {
                            found = true;
                            break;
                        }
                }

                // Assert
                Assert.AreEqual(true, found, "expected : " + expected + TestContext.CurrentContext.Test.Name);
            }
        }

        [Test]
        public void CharacterJobEnumHelper_CharacterJobEnum_GetListMessage_Should_Pass()
        {
            // Arrange

            // Act
            var myDataList = CharacterJobEnumHelper.GetListMessage;
            var myExpectedList = new List<string>()
            {
                "Dojo Master",
                "Pet Lover",
                "Quick Attacker",
            };

            // Reset

            // Assert
            // Make sure each item is in the list
            foreach (var item in myDataList)
            {
                var found = false;
                foreach (var expected in myExpectedList)
                {
                    if (item == expected)
                    {
                        found = true;
                        break;
                    }
                }

                // Assert
                Assert.AreEqual(true, found, "item : " + item + TestContext.CurrentContext.Test.Name);
            }

            // reverse it, to make sure the list has each item
            // Make sure each item is in the list
            foreach (var expected in myExpectedList)
            {
                var found = false;
                {
                    foreach (var item in myDataList)
                        if (item == expected)
                        {
                            found = true;
                            break;
                        }
                }

                // Assert
                Assert.AreEqual(true, found, "expected : " + expected + TestContext.CurrentContext.Test.Name);
            }
        }

        [Test]
        public void CharacterJobEnumHelper_ConvertStringToEnum_Should_Pass()
        {
            // Arrange
            var myList = Enum.GetNames(typeof(CharacterJobEnum)).ToList();

            CharacterJobEnum myActual;
            CharacterJobEnum myExpected;

            // Act
            foreach (var item in myList)
            {
                myActual = CharacterJobEnumHelper.ConvertStringToEnum(item);
                myExpected = (CharacterJobEnum)Enum.Parse(typeof(CharacterJobEnum), item);

                // Assert
                Assert.AreEqual(myExpected, myActual, "string: " + item + TestContext.CurrentContext.Test.Name);
            }
        }

        [Test]
        public void CharacterJobEnumHelper_ConvertMessageToEnum_Should_Pass()
        {
            // Arrange
            var myList = new List<string>()
            {
                "Dojo Master",
                "Pet Lover",
                "Quick Attacker",
                "Some Random String"
            };
            var myExpectedList = new List<CharacterJobEnum>()
            {
                CharacterJobEnum.DojoMaster,
                CharacterJobEnum.PetLover,
                CharacterJobEnum.QuickAttacker,
                CharacterJobEnum.Unknown
            };

            // Act
            for (int i = 0; i < myList.Count; i++)
            {
                CharacterJobEnum myActual = CharacterJobEnumHelper.ConvertMessageToEnum(myList[i]);

                // Assert
                Assert.AreEqual(myExpectedList[i], myActual);
            }
        }

        [Test]
        public void CharacterJobEnumHelper_GetCharacterJobEnumByPosition_1_Should_Pass()
        {
            // Arrange
            var value = 1;

            // Act
            var Actual = CharacterJobEnumHelper.GetCharacterJobByPosition(value);
            var Expected = CharacterJobEnum.PetLover;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CharacterJobEnumHelper_GetCharacterJobEnumByPosition_2_Should_Pass()
        {
            // Arrange
            var value = 2;

            // Act
            var Actual = CharacterJobEnumHelper.GetCharacterJobByPosition(value);
            var Expected = CharacterJobEnum.DojoMaster;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CharacterJobEnumHelper_GetCharacterJobEnumByPosition_3_Should_Pass()
        {
            // Arrange
            var value = 3;

            // Act
            var Actual = CharacterJobEnumHelper.GetCharacterJobByPosition(value);
            var Expected = CharacterJobEnum.QuickAttacker;

            // Assert
            Assert.AreEqual(Expected, Actual, TestContext.CurrentContext.Test.Name);
        }

    }
}