using System.Collections.Generic;
using GiveCRM.Models;

namespace GiveCRM.BusinessLogic.ExcelImport
{
    /// <summary>
    /// Defines an interface for creating <see cref="Member"/> objects from various serialised representations
    /// of that class.
    /// </summary>
    // AS: I'm uncomfortable with this being public; I think it should be internal. If this is done, 
    // GiveCRM.BusinessLogic must expose its internals to InternalsVisible.ToDynamicProxyGenAssembly2.
    public interface IMemberFactory
    {
        /// <summary>
        /// Creates a <see cref="Member"/> from an <see cref="IDictionary{TKey,TValue}"/>, where each
        /// key in the dictionary is the name of a property on Member, and the property values are the 
        /// dictionary's values.
        /// </summary>
        /// <param name="memberData">The member data.</param>
        /// <returns>A fully-populated <see cref="Member"/>, using the values from the dictionary.</returns>
        Member CreateMember(IDictionary<string, object> memberData);
    }
}