using System.Collections;
using System.Collections.Generic;

namespace OrderHub.Collections;

public class CircularBuffer<T> : IEnumerable<T>
{
    // ЗАДАЧА1-3: внутренний массив, счётчики next / size / capacity и конструктор

    public int Count => 0;
    public bool IsFull => false;

    // ЗАДАЧА: запись элемента со сдвигом next по кольцу
    public void Add(T item) => throw new NotImplementedException();

    // ЗАДАЧА: обход от самого старого элемента к самому новому через yield return
    public IEnumerator<T> GetEnumerator() => throw new NotImplementedException();

    // ЗАДАЧА: обратный обход — от самого нового к самому старому
    public IEnumerable<T> Reverse() => throw new NotImplementedException();

    // ЗАДАЧА: явная реализация неуниверсального метода
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
