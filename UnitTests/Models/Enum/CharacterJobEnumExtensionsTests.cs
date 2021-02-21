using NUnit.Framework;

using Game.Models;

namespace UnitTests.Models
{
    [TestFixture]
    public class CharacterJobEnumExtensionsTests
    {
        [Test]
        public void CharacterJobEnumExtensionsTests_Unknown_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.Unknown.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Player", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_DojoMaster_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.DojoMaster.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Dojo Master", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_PetLover_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.PetLover.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Pet Lover", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_QuickAttacker_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.QuickAttacker.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Quick Attacker", result);
        }
    }
}
