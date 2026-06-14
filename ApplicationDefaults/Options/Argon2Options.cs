namespace ApplicationDefaults.Options
{
    // Argon2id work factors. Defaults follow the OWASP Password Storage Cheat Sheet minimum
    // for Argon2id (19 MiB memory, 2 iterations, 1 degree of parallelism). Raise MemoryKib /
    // Iterations on stronger hardware; existing hashes keep verifying because each hash embeds
    // the parameters it was created with.
    public class Argon2Options
    {
        // Memory cost in KiB. 19456 KiB = 19 MiB.
        public int MemoryKib { get; set; } = 19456;

        // Time cost (number of passes).
        public int Iterations { get; set; } = 2;

        // Degree of parallelism (lanes).
        public int DegreeOfParallelism { get; set; } = 1;
    }
}
