// LICENSE: The Unlicense

#pragma warning disable RCS1170 // Use read-only auto-implemented property.

using System.ComponentModel.DataAnnotations;

namespace RepositoryPatternQuickSample.Models
{
    public class Cat
    {
        [Key]
        public int Id { get; private set; }

        [StringLength(maximumLength: 100)]
        public string Name { get; set; } = default!;

        [StringLength(maximumLength: 100)]
        public string Color { get; set; } = default!;
    }
}
