using FlagsNET;

namespace Flags.Tests
{
    public class FlagsTest
    {
        [SetUp]
        public void Setup()
        {
            // Réinitialiser l'état des drapeaux avant chaque test
            typeof(Flag).GetField("_flags", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                        .SetValue(null, new Dictionary<string, bool>());
        }

        [Test]
        public void Flag_Init_ShouldInitializeFlag()
        {
            Flag.Init("Test_Flag");

            Assert.True(Flag.Exists("Test_Flag"));
        }

        [Test]
        public void Flag_Init_ShouldThrowExceptionWhenFlagAlreadyExists()
        {
            Flag.Init("Test_Flag");

            ArgumentException? ex = Assert.Throws<ArgumentException>(() => Flag.Init("Test_Flag"));
            Assert.AreEqual("Flag 'Test_Flag' is already initialized.", ex.Message);
        }

        [Test]
        public void Flag_Set_ShouldSetFlagValue()
        {
            Flag.Init("Test_Flag");

            Flag.Set("Test_Flag", true);

            Assert.True(Flag.Get("Test_Flag"));
        }

        [Test]
        public void Flag_Set_ShouldThrowExceptionWhenFlagDoesNotExist()
        {
            KeyNotFoundException? ex = Assert.Throws<KeyNotFoundException>(() => Flag.Set("NonExistent_Flag", true));
            Assert.AreEqual("Flag 'NonExistent_Flag' does not exist.", ex.Message);
        }

        [Test]
        public void Flag_GetStatut_ShouldReturnFlagValue()
        {
            Flag.Init("Test_Flag");

            Flag.Set("Test_Flag", true);

            Assert.True(Flag.Get("Test_Flag"));
        }

        [Test]
        public void Flag_GetStatut_ShouldThrowExceptionWhenFlagDoesNotExist()
        {
            KeyNotFoundException? ex = Assert.Throws<KeyNotFoundException>(() => Flag.Get("NonExistent_Flag"));
            Assert.AreEqual("Flag 'NonExistent_Flag' does not exist.", ex.Message);
        }

        [Test]
        public void Flag_Reset_ShouldResetFlagValue()
        {
            Flag.Init("Test_Flag");

            Flag.Set("Test_Flag", true);
            Flag.Reset("Test_Flag");

            Assert.False(Flag.Get("Test_Flag"));
        }

        [Test]
        public void Flag_Reset_ShouldThrowExceptionWhenFlagDoesNotExist()
        {
            KeyNotFoundException? ex = Assert.Throws<KeyNotFoundException>(() => Flag.Reset("NonExistent_Flag"));
            Assert.AreEqual("Flag 'NonExistent_Flag' does not exist.", ex.Message);
        }

        [Test]
        public async Task Flag_Waittil_ShouldWaitUntilFlagIsSetToTrue()
        {
            Flag.Init("Test_Flag");

            Task.Run(() =>
            {
                Task.Delay(500).Wait(); // Attendre 500ms avant de définir le drapeau à true
                Flag.Set("Test_Flag", true);
            });

            DateTime start = DateTime.Now;
            await Flag.WaitTill("Test_Flag");
            DateTime end = DateTime.Now;

            Assert.GreaterOrEqual((end - start).TotalMilliseconds, 500);
            Assert.True(Flag.Get("Test_Flag"));
        }

        [Test]
        public void Flag_Waittil_ShouldThrowExceptionWhenFlagDoesNotExist()
        {
            KeyNotFoundException? ex = Assert.ThrowsAsync<KeyNotFoundException>(async () => await Flag.WaitTill("NonExistent_Flag"));
            
            Assert.AreEqual("Flag 'NonExistent_Flag' does not exist.", ex.Message);
        }

        [Test]
        public void Flag_Exists_ShouldReturnTrueWhenFlagExists()
        {
            Flag.Init("Test_Flag");

            Assert.True(Flag.Exists("Test_Flag"));
        }

        [Test]
        public void Flag_Exists_ShouldReturnFalseWhenFlagDoesNotExist()
        {
            Assert.False(Flag.Exists("NonExistent_Flag"));
        }
    }
}
