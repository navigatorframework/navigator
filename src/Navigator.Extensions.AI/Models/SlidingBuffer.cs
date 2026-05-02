using System.Collections;

namespace Navigator.Extensions.AI.Models;

public class SlidingBuffer<T> : IReadOnlyList<T>
{
    private readonly List<T> _items = [];

    public SlidingBuffer(int maxLength)
    {
        if (maxLength <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxLength), "Sliding buffer length must be greater than zero.");
        }

        MaxLength = maxLength;
    }

    public int MaxLength { get; }

    public int Count => _items.Count;

    public T this[int index] => _items[index];

    public void Add(T item)
    {
        if (_items.Count == MaxLength)
        {
            _items.RemoveAt(0);
        }

        _items.Add(item);
    }

    public void AddRange(IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            Add(item);
        }
    }

    public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
