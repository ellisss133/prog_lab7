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
