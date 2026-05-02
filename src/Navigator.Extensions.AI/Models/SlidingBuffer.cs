using System.Collections;

namespace Navigator.Extensions.AI.Models;

/// <summary>
///     Represents a fixed-length buffer that discards the oldest item when full.
/// </summary>
/// <typeparam name="T">The item type stored in the buffer.</typeparam>
public class SlidingBuffer<T> : IReadOnlyList<T>
{
    private readonly List<T> _items = [];

    /// <summary>
    ///     Initializes a new sliding buffer.
    /// </summary>
    /// <param name="maxLength">The maximum number of items to retain.</param>
    public SlidingBuffer(int maxLength)
    {
        if (maxLength <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxLength), "Sliding buffer length must be greater than zero.");
        }

        MaxLength = maxLength;
    }

    /// <summary>
    ///     Gets the maximum number of items retained by the buffer.
    /// </summary>
    public int MaxLength { get; }

    /// <summary>
    ///     Gets the current number of items in the buffer.
    /// </summary>
    public int Count => _items.Count;

    /// <summary>
    ///     Gets the item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based item index.</param>
    /// <returns>The item at the specified index.</returns>
    public T this[int index] => _items[index];

    /// <summary>
    ///     Adds an item to the buffer and discards the oldest item when the buffer is full.
    /// </summary>
    /// <param name="item">The item to add.</param>
    public void Add(T item)
    {
        if (_items.Count == MaxLength)
        {
            _items.RemoveAt(0);
        }

        _items.Add(item);
    }

    /// <summary>
    ///     Adds a sequence of items to the buffer in order.
    /// </summary>
    /// <param name="items">The items to add.</param>
    public void AddRange(IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            Add(item);
        }
    }

    /// <summary>
    ///     Returns an enumerator that iterates through the buffer.
    /// </summary>
    /// <returns>An enumerator for the buffer items.</returns>
    public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
