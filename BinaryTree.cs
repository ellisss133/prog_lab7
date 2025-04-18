using System;
using System.Collections;
using System.Collections.Generic;

public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T> {
  private TreeNode<T> root;

  public void Add(T data) {
    if (root == null)
      root = new TreeNode<T>(data);
    else
      AddChild(root, data);
  }

  private void AddChild(TreeNode<T> node, T data) {
    if (data.CompareTo(node.Data) < 0) {
      if (node.Left == null)
        node.Left = new TreeNode<T>(data) { Parent = node };
      else
        AddChild(node.Left, data);
    } else {
      if (node.Right == null)
        node.Right = new TreeNode<T>(data) { Parent = node };
      else
        AddChild(node.Right, data);
    }
  }

  public TreeIterator GetIterator() => new TreeIterator(root);

  public IEnumerator<T> GetEnumerator() {
    TreeNode<T> current = GetMostLeftNode(root);
    while (current != null) {
      yield return current.Data;
      current = Next(current);
    }
  }

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

  public TreeNode<T> Next(TreeNode<T> node) {
    if (node.Right != null) {
      node = node.Right;
      while (node.Left != null)
        node = node.Left;
      return node;
    }
    while (node.Parent != null && node == node.Parent.Right)
      node = node.Parent;
    return node.Parent;
  }

  public TreeNode<T> Previous(TreeNode<T> node) {
    if (node.Left != null) {
      node = node.Left;
      while (node.Right != null)
        node = node.Right;
      return node;
    }
    while (node.Parent != null && node == node.Parent.Left)
      node = node.Parent;
    return node.Parent;
  }

  private TreeNode<T> GetMostLeftNode(TreeNode<T> node) {
    while (node != null && node.Left != null)
      node = node.Left;
    return node;
  }

  public IEnumerable<T> InOrderTraversal() {
    return Traverse(root);

    IEnumerable<T> Traverse(TreeNode<T> node) {
      if (node == null) yield break;
      foreach (var n in Traverse(node.Left)) yield return n;
      yield return node.Data;
      foreach (var n in Traverse(node.Right)) yield return n;
    }
  }

  public class TreeIterator {
    private TreeNode<T> current;

    public TreeIterator(TreeNode<T> root) {
      current = root;
      if (current != null)
        while (current.Left != null)
          current = current.Left;
    }

    public bool Next() {
      var next = FindNext(current);
      if (next != null) {
        current = next;
        return true;
      }
      return false;
    }

    public bool Previous() {
      var prev = FindPrev(current);
      if (prev != null) {
        current = prev;
        return true;
      }
      return false;
    }

    public T Current() => current != null ? current.Data : default;

    private TreeNode<T> FindNext(TreeNode<T> node) {
      if (node.Right != null) {
        node = node.Right;
        while (node.Left != null)
          node = node.Left;
        return node;
      }
      while (node.Parent != null && node == node.Parent.Right)
        node = node.Parent;
      return node.Parent;
    }

    private TreeNode<T> FindPrev(TreeNode<T> node) {
      if (node.Left != null) {
        node = node.Left;
        while (node.Right != null)
          node = node.Right;
        return node;
      }
      while (node.Parent != null && node == node.Parent.Left)
        node = node.Parent;
      return node.Parent;
    }
  }
}
