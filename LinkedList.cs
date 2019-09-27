using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class MyLinkedList<T>
    {
        public Node<T> firstNode, lastNode;
        public int Count;

        public MyLinkedList()
        {
            firstNode = null;
            lastNode = null;
            Count = 0;
        }

        public void Add(T obj)
        {
            if (firstNode == null)
            {
                firstNode = new Node<T>(obj);
                goto end;
            }
            if (lastNode == null)
            {
                lastNode = new Node<T>(obj);
                firstNode.next = lastNode;
                goto end;
            }
            Node<T> node = new Node<T>(obj);
            lastNode.next = node;
            lastNode = node;

        end:
            Count++;
        }

        public T this[int key]
        {
            get
            {
                if (key < 0 || key >= Count) throw new IndexOutOfRangeException();
                if (key == 0) return firstNode.data;
                if (key == Count - 1) return lastNode.data;
                Node<T> curr = firstNode.next;
                for (int i = 1; i <= key; i++)
                {
                    if (i == key)
                        return curr.data;
                    curr = curr.next;
                }
                return default;
            }
            set
            {
                if (key < 0 || key >= Count) throw new IndexOutOfRangeException();
                if (key == 0) firstNode.data = value;
                if (key == Count - 1) lastNode.data = value;
                Node<T> curr = firstNode.next;
                for (int i = 1; i <= key; i++)
                {
                    if (i == key)
                    {
                        curr.data = value;
                        return;
                    }
                    curr = curr.next;
                }
            }
        }
    }

    class Node<T>
    {
        public Node(T _obj, Node<T> _next = null)
        {
            data = _obj;
            next = _next;
        }

        public Node<T> next;
        public T data;
    }
}
