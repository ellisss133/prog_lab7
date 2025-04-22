using System;
using System.Collections.Generic;

class Program {
  static void Main() {
    var tree = new BinaryTree<int>();
    tree.Add(5);
    tree.Add(3);
    tree.Add(8);
    tree.Add(1);
    tree.Add(4);
    tree.Add(7);
    tree.Add(10);

    Console.WriteLine("foreach:");
    foreach (var val in tree)
      Console.Write(val + " ");

    Console.WriteLine("\n\nлямбда-итератор (центральный обход):");
    Func<BinaryTree<int>, IEnumerable<int>> lambdaIter = t => t.InOrderTraversal();
    foreach (var val in lambdaIter(tree))
      Console.Write(val + " ");

    Console.WriteLine("\n\nлямбда-итератор (прямой обход):");
    Func<BinaryTree<int>, IEnumerable<int>> preOrder = t => t.PreOrderTraversal();
    foreach (var val in preOrder(tree))
      Console.Write(val + " ");

    Console.WriteLine("\n\nлямбда-итератор (обратный обход):");
    Func<BinaryTree<int>, IEnumerable<int>> postOrder = t => t.PostOrderTraversal();
    foreach (var val in postOrder(tree))
      Console.Write(val + " ");

    Console.WriteLine("\n\nручной итератор (вперёд):");
    var it = tree.GetIterator();
    do {
      Console.Write(it.Current() + " ");
    } while (it.Next());

    Console.WriteLine("\n\nручной итератор (назад):");
    do {
      Console.Write(it.Current() + " ");
    } while (it.Previous());
  }
}
