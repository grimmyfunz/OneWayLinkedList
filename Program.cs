using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneWayLinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<string> linkedList = new LinkedList<string>();
            linkedList.Add("Tom");
            linkedList.Add("Alice");
            linkedList.Add("Bob");
            linkedList.Add("Sam");
            linkedList.Print();
            linkedList.Contains("Sam");
            linkedList.Remove("Alice");
            linkedList.Print();
            linkedList.Contains("Alice");
            linkedList.AppendFirst("Bill");
            linkedList.Add("Bill");
            Console.WriteLine(linkedList.FirstIndexOf("Bill"));
            Console.WriteLine(linkedList.LastIndexOf("Bill"));
            linkedList.Print();
            linkedList.RemoveAt(1);
            linkedList.Insert("You", 2);
            linkedList.Print();
            Console.WriteLine(linkedList.Count());
            linkedList.Clear();
            linkedList.Print();
            Console.WriteLine(linkedList.Count());
        }

        public class Node<T>
        {
            public T Data { get; set; }
            public Node<T> Next { get; set; }
            public int Index { get; set; }

            public Node(T data)
            {
                Data = data;
            }
        }

        public class LinkedList<T> : IEnumerable<T>
        {
            Node<T> head; // FIRST NODE
            Node<T> tail; // LAST NODE
            int count;

            // ADDS NODE
            public void Add(T data)
            {
                Node<T> node = new Node<T>(data);

                if (head == null)
                    head = node;
                else
                    tail.Next = node;
                tail = node;

                count++;
                ReIndex();
            }

            // REMOVES NODE
            public bool Remove(T data)
            {
                Node<T> current = head;
                Node<T> previous = null;

                while (current != null)
                {
                    if (current.Data.Equals(data))
                    {
                        if (previous != null)
                        {
                            // SET REFERENCE TO NEXT NODE
                            previous.Next = current.Next;

                            // IF REMOVED NODE WAS LAST NODE PREVIOUS NODE BECOMES TAIL
                            if (current.Next == null)
                                tail = previous;
                        }
                        else
                        {
                            // IF REMOVED NODE VAS FIRST NODE SET NEXT NODE AS FIRST ONE
                            head = head.Next;

                            // IF LIST IS EMPTY RESET TAIL
                            if (head == null)
                                tail = null;
                        }
                        count--;
                        ReIndex();
                        return true;
                    }

                    previous = current;
                    current = current.Next;
                }
                return false;
            }

            public int Count { get { return count; } }

            public bool IsEmpty { get { return count == 0; } }

            public void Clear()
            {
                head = null;
                tail = null;
                count = 0;
                ReIndex();
            }
            
            // CHECK EXISTANCE OF OBJECT
            public bool Contains(T data)
            {
                Node<T> current = head;
                while (current != null)
                {
                    if (current.Data.Equals(data))
                    {
                        Console.WriteLine($"{data} exists!");
                        return true;
                    }
                    current = current.Next;
                }
                Console.WriteLine($"{data} not exists!");
                return false;
            }
            
            // ADD NOTE AS FIRST ELEMENT
            public void AppendFirst(T data)
            {
                Node<T> node = new Node<T>(data);
                node.Next = head;
                head = node;
                if (count == 0)
                    tail = head;
                count++;
                ReIndex();
            }

            public void Print()
            {
                Console.WriteLine();

                Node<T> current = head;

                while (current != null)
                {
                    Console.WriteLine($"{current.Data} {current.Index}");
                    current = current.Next;
                }
            }

            public void ReIndex()
            {
                Node<T> current = head;
                int counter = 0;

                while (current != null)
                {
                    current.Index = counter;
                    counter++;
                    current = current.Next;
                }
            }

            public bool RemoveAt(int index)
            {
                if (index < count)
                {
                    Node<T> current = head;
                    Node<T> previous = null;

                    while (current != null)
                    {
                        if (index == current.Index)
                        {
                            if (previous != null)
                            {
                                // SET REFERENCE TO NEXT NODE
                                previous.Next = current.Next;

                                // IF REMOVED NODE WAS LAST NODE PREVIOUS NODE BECOMES TAIL
                                if (current.Next == null)
                                    tail = previous;
                            }
                            else
                            {
                                // IF REMOVED NODE VAS FIRST NODE SET NEXT NODE AS FIRST ONE
                                head = head.Next;

                                // IF LIST IS EMPTY RESET TAIL
                                if (head == null)
                                    tail = null;
                            }
                            count--;
                            ReIndex();
                            return true;
                        }

                        previous = current;
                        current = current.Next;
                    }
                    return false;
                }
                return false;
            }

            public T ElementAt(int index)
            {
                if (index < count)
                {
                    Node<T> current = head;

                    while (current != null)
                    {

                        if (index == current.Index)
                        {
                            return current.Data;
                        }

                        current = current.Next;
                    }
                }

                return default(T);
            }

            public Node<T> NodeAt(int index)
            {
                if (index < count)
                {
                    Node<T> current = head;

                    while (current != null)
                    {

                        if (index == current.Index)
                        {
                            return current;
                        }

                        current = current.Next;
                    }
                }

                return null;
            }

            // FINDS FIRST INDEX OF THIS DATA
            public int FirstIndexOf(T data)
            {
                Node<T> current = head;

                while (current != null)
                {
                    if (current.Data.Equals(data))
                    {
                        return current.Index;
                    }

                    current = current.Next;
                }

                return -1;
            }

            // FINDS LAST INDEX OF THIS DATA
            public int LastIndexOf(T data)
            {
                Node<T> current = head;

                int lastIndex = -1;

                while (current != null)
                {
                    if (current.Data.Equals(data))
                    {
                        lastIndex = current.Index;
                    }

                    current = current.Next;
                }

                return lastIndex;
            }

            // INSERTS DATA AT THE INDEX
            public void Insert(T data, int index)
            {
                Node<T> node = new Node<T>(data);
                if (index <= count && index > -1)
                {
                    Node<T> previous = NodeAt(index - 1);
                    // IF PREVIOUS NODE AND NEXT NODE EXISTS
                    if (previous != null && previous.Next != null)
                    {
                        node.Next = previous.Next;
                        previous.Next = node;
                    }
                    else if (previous != null)
                    {
                        previous.Next = node;
                    }
                    else
                    {
                        head = node;
                        tail = node;
                    }
                    count++;
                    ReIndex();
                }
            }

            // REALISATION OF IEnumerable INTERFACE
            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)this).GetEnumerator();
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                Node<T> current = head;
                while (current != null)
                {
                    yield return current.Data;
                    current = current.Next;
                }
            }
        }
    }
}
