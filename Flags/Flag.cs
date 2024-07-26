namespace FlagsNET
{
    /// <summary>
    /// Static class to manage flags.
    /// Provides methods to initialize, set, get status, reset, and wait for flags.
    /// </summary>
    public static class Flag
    {
        private static Dictionary<string, bool> _flags = new Dictionary<string, bool>();

        /// <summary>
        /// Initializes a flag with the given name and sets its value to false.
        /// </summary>
        /// <param name="flag">The name of the flag to initialize.</param>
        /// <exception cref="ArgumentException">Thrown when the flag is already initialized.</exception>
        public static void Init(string flag)
        {
            if (_flags.ContainsKey(flag))
            {
                throw new ArgumentException($"Flag '{flag}' is already initialized.");
            }

            _flags.Add(flag, false);
        }

        /// <summary>
        /// Resets the value of the specified flag to false.
        /// </summary>
        /// <param name="flag">The name of the flag to reset.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the flag does not exist.</exception>
        public static void Reset(string flag)
        {
            if (!_flags.ContainsKey(flag))
            {
                throw new KeyNotFoundException($"Flag '{flag}' does not exist.");
            }

            _flags[flag] = false;
        }

        /// <summary>
        /// Sets the value of the specified flag.
        /// </summary>
        /// <param name="flag">The name of the flag to set.</param>
        /// <param name="value">The value to set the flag to.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the flag does not exist.</exception>
        public static void Set(string flag, bool value)
        {
            if (!_flags.ContainsKey(flag))
            {
                throw new KeyNotFoundException($"Flag '{flag}' does not exist.");
            }

            _flags[flag] = value;
        }

        /// <summary>
        /// Gets the status of the specified flag.
        /// </summary>
        /// <param name="flag">The name of the flag to get the status of.</param>
        /// <returns>The current value of the flag.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the flag does not exist.</exception>
        public static bool Get(string flag)
        {
            if (!_flags.ContainsKey(flag))
            {
                throw new KeyNotFoundException($"Flag '{flag}' does not exist.");
            }

            return _flags[flag];
        }

        /// <summary>
        /// Blocks the calling thread until the specified flag is set to true.
        /// </summary>
        /// <param name="flag">The name of the flag to wait for.</param>
        /// <exception cref="KeyNotFoundException">Thrown when the flag does not exist.</exception>
        public static async Task WaitTill(string flag)
        {
            if (!_flags.ContainsKey(flag))
            {
                throw new KeyNotFoundException($"Flag '{flag}' does not exist.");
            }

            // Wait next frame if flag is not set
            while (!_flags[flag])
            {
                await Task.Yield();
            }
        }

        /// <summary>
        /// Checks if the specified flag exists.
        /// </summary>
        /// <param name="flag">The name of the flag to check.</param>
        /// <returns>True if the flag exists, false otherwise.</returns>
        public static bool Exists(string flag)
        {
            return _flags.ContainsKey(flag);
        }
    }
}
