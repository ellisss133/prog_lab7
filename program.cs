using System;
using System.Collections;
using System.Collections.Generic;

public class TreeNode<T> where T : IComparable<T>
{
  public T Value;
  public TreeNode<T> Left, Right, Parent;

  public TreeNode(T value, TreeNode<T> parent = null)
  {
    Value = value;
    Parent = parent;
  }
}

public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
{
  private TreeNode<T> root;

  public void Add(T value)
  {
    root = Add(root, value, null);
  }

  private TreeNode<T> Add(TreeNode<T> node, T value, TreeNode<T> parent)
  {
    if (node == null)
      return new TreeNode<T>(value, parent);

    if (value.CompareTo(node.Value) < 0)
      node.Left = Add(node.Left, value, node);
    else
      node.Right = Add(node.Right, value, node);

    return node;
  }

  public TreeIterator GetIterator()
  {
    return new TreeIterator(root);
  }

  public IEnumerator<T> GetEnumerator()
  {
    return InOrderTraversal(root).GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }

  private IEnumerable<T> InOrderTraversal(TreeNode<T> node)
  {
    if (node == null) yield break;

    foreach (var v in InOrderTraversal(node.Left))
      yield return v;

    yield return node.Value;

    foreach (var v in InOrderTraversal(node.Right))
      yield return v;
  }

  public class TreeIterator
  {
    private TreeNode<T> current;
    private TreeNode<T> root;

    public TreeIterator(TreeNode<T> root)
    {
      this.root = root;
      this.current = FindMin(root);
    }

    private TreeNode<T> FindMin(TreeNode<T> node)
    {
      while (node?.Left != null)
        node = node.Left;
      return node;
    }

    private TreeNode<T> FindMax(TreeNode<T> node)
    {
      while (node?.Right != null)
        node = node.Right;
      return node;
    }

    public T Current()
    {
      if (current == null) throw new InvalidOperationException();
      return current.Value;
    }

    public bool Next()
    {
      if (current == null) return false;

      if (current.Right != null)
      {
        current = current.Right;
        while (current.Left != null)
          current = current.Left;
        return true;
      }

      while (current.Parent != null && current == current.Parent.Right)
        current = current.Parent;

      current = current.Parent;
      return current != null;
    }

    public bool Previous()
    {
      if (current == null) return false;

      if (current.Left != null)
      {
        current = current.Left;
        while (current.Right != null)
          current = current.Right;
        return true;
      }

      while (current.Parent != null && current == current.Parent.Left)
        current = current.Parent;

      current = current.Parent;
      return current != null;
    }

    public static TreeIterator operator ++(TreeIterator it)
    {
      it.Next();
      return it;
    }

    public static TreeIterator operator --(TreeIterator it)
    {
      it.Previous();
      return it;
    }
  }

  public IEnumerable<T> TraverseWith(Func<TreeNode<T>, IEnumerable<T>> traversal)
  {
    return traversal(root);
  }

  public static IEnumerable<T> CentralTraversal(TreeNode<T> node)
  {
    if (node == null) yield break;

    foreach (var val in CentralTraversal(node.Left))
      yield return val;

    yield return node.Value;

    foreach (var val in CentralTraversal(node.Right))
      yield return val;
  }
}