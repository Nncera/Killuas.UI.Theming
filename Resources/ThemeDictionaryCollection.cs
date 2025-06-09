using System.Collections.ObjectModel;

//FINISCH: ThemeDictionaryCollection
namespace Killuas.UI.Theming.Resources
{
    /// <summary>
    /// A collection of named <see cref="ThemeDictionary"/> instances.
    /// Used to group theme variants such as "Default", "Dark", "Light", etc.
    /// </summary>
    public sealed class ThemeDictionaryCollection : Collection<ThemeDictionary>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeDictionaryCollection"/> class.
        /// </summary>
        public ThemeDictionaryCollection() : base() { }
    }
}
