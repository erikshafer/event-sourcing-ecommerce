using JasperFx.Core;

namespace Ecommerce.Core.Extensions;

public static class ListExtensions
{
    public static IList<T> Replace<T>(this IList<T> list, T existingElement, T replacement)
    {
        var indexOfExistingItem = list.IndexOf(existingElement);

        if (indexOfExistingItem == -1)
            throw new ArgumentOutOfRangeException(nameof(existingElement), "Element was not found");

        list[indexOfExistingItem] = replacement;

        return list;
    }

    public static IList<T> ClearAndReplace<T>(this IList<T> list, IEnumerable<T>? replacements)
    {
        if (replacements == null || !replacements.Any())
            throw new ArgumentNullException(nameof(replacements), "Replacements were empty");

        list.Clear();

        list.AddRange(replacements);

        return list;
    }
}
