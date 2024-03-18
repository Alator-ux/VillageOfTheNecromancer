using System;

public class PriorityQueue<T>
{
    private Tuple<T, float>[] _heap;
    private int _size;

    public PriorityQueue(int capacity)
    {
        _heap = new Tuple<T, float>[capacity];
        _size = 0;
    }

    public int Count
    {
        get { return _size; }
    }
    public bool IsEmpty
    {
        get { return _size == 0; }
    }
    public void Enqueue(T item, float priority)
    {
        if (_size == _heap.Length)
        {
            Array.Resize(ref _heap, _heap.Length * 2);
        }

        _heap[_size] = Tuple.Create(item, priority);
        HeapifyUp(_size);
        _size++;
    }

    public Tuple<T, float> Dequeue()
    {
        if (_size == 0)
        {
            throw new InvalidOperationException("Priority queue is empty.");
        }

        Tuple<T, float> root = _heap[0];
        _size--;
        _heap[0] = _heap[_size];
        HeapifyDown(0);
        return root;
    }

    private void HeapifyUp(int index)
    {
        int parent = (index - 1) / 2;

        while (index > 0 && _heap[index].Item2 < _heap[parent].Item2)
        {
            Swap(index, parent);
            index = parent;
            parent = (index - 1) / 2;
        }
    }

    private void HeapifyDown(int index)
    {
        int leftChild;
        int rightChild;
        int smallestChild;

        while (true)
        {
            leftChild = 2 * index + 1;
            rightChild = 2 * index + 2;
            smallestChild = index;

            if (leftChild < _size && _heap[leftChild].Item2 < _heap[smallestChild].Item2)
            {
                smallestChild = leftChild;
            }

            if (rightChild < _size && _heap[rightChild].Item2 < _heap[smallestChild].Item2)
            {
                smallestChild = rightChild;
            }

            if (smallestChild == index)
            {
                break;
            }

            Swap(index, smallestChild);
            index = smallestChild;
        }
    }

    private void Swap(int i, int j)
    {
        Tuple<T, float> temp = _heap[i];
        _heap[i] = _heap[j];
        _heap[j] = temp;
    }
}
