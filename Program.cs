using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree tree = new Tree();     //实例化二叉树
            string Input = Console.ReadLine();
            Input = new Regex("[\r\n ]").Replace(Input, "");

            //判断输入数据是否合法
            if (new Regex("[^A-Za-z#]").IsMatch(Input))
            {
                Console.WriteLine("Error input");
            }
            else
            {
                tree.RecursiveCreate(Input);            //递归构建二叉树
                tree.RecursivePreOrderTraversal();      //递归前序遍历
                Console.WriteLine();
                tree.RecursiveInvert();                 //递归反转二叉树
                tree.NonRecursivePreOrderTraversal();   //非递归后序遍历
                Console.ReadLine();
            }
        }
    }

    //Node类 表示二叉树的的二叉链表节点
    class Node
    {
        public Node Left;       //左子树
        public Node Right;      //右子树
        public char Content;    //节点标识       

        //构造方法
        public Node()
        {
            Left = null;
            Right = null;
            Content = default(char);
        }
    }

    //Tree类 表示二叉树的二叉链表
    class Tree
    {
        public Node Root;       //二叉树根节点
        private Queue<char> Operators = new Queue<char>();  //用于存储待处理节点的队列

        //构造方法
        public Tree()
        {
            Root = null;
        }

        //Recursive_Create方法 递归构建二叉树的递归体
        private void Recursive_Create(ref Node node)
        {
            char c;
            c = Operators.Dequeue();
            if (c == '#')
            {
                node = null;
            }
            else
            {
                //构建当前节点
                node = new Node();
                node.Content = c;

                Recursive_Create(ref node.Left);    //递归构建左子树
                Recursive_Create(ref node.Right);   //递归构建右子树
            }
        }

        //RecursiveCreate方法 递归构建二叉树的调用体
        public void RecursiveCreate(string s)
        {
            //将输入串解析为待处理节点并入队
            foreach (char c in s)
            {
                Operators.Enqueue(c);
            }

            Recursive_Create(ref Root);     //自根节点开始递归构建二叉树
        }

        //Recursive_PreOrder_Traversal方法 递归前序遍历二叉树的递归体
        private void Recursive_PreOrder_Traversal(Node node, int level)
        {
            if (node != null)
            {
                //按节点层级输出相应数量的缩进空格
                for (int i = 0; i < level; i++)
                {
                    Console.Write(" ");
                }

                Console.WriteLine(node.Content);    //输出当前节点的标识

                //递归遍历左子树
                if (node.Left != null)
                {
                    Recursive_PreOrder_Traversal(node.Left, level + 1);
                }

                //递归遍历右子树
                if (node.Right != null)
                {
                    Recursive_PreOrder_Traversal(node.Right, level + 1);
                }
            }
        }

        //RecursivePreOrderTraversal方法 递归前序遍历二叉树的调用体
        public void RecursivePreOrderTraversal()
        {
            Recursive_PreOrder_Traversal(Root, 0);  //自根节点开始底柜前序遍历二叉树
        }

        //Recursive_Invert方法 递归反转二叉树的递归体
        private void Recursive_Invert(ref Node node)
        {
            if (node != null)
            {
                //反转当前节点的左右子树
                Node temp = node.Left;
                node.Left = node.Right;
                node.Right = temp;
            }
            if (node.Left != null)
            {
                Recursive_Invert(ref node.Left);    //递归反转左子树的左右子树
            }
            if (node.Right != null)
            {
                Recursive_Invert(ref node.Right);   //递归反转右子树的左右子树
            }
        }

        //RecursiveInvert方法 递归反转二叉树的调用体
        public void RecursiveInvert()
        {
            Recursive_Invert(ref Root);
        }

        //NonRecursivePreOrderTraversal方法 非递归前序遍历二叉树
        public void NonRecursivePreOrderTraversal()
        {
            int Blanks = 1;     //缩进空格数量
            bool sgn = true;
            Stack<Node> stack = new Stack<Node>();  //用于遍历二叉树的堆栈
            Node Current = Root.Left;
            Console.WriteLine(Root.Content);    //输出当前节点
            stack.Push(Root);
            while (stack.Count > 0 || Current != null)
            {
                while (Current != null)
                {
                    for (int i = 0; i < Blanks; i++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine(Current.Content);
                    stack.Push(Current);
                    Current = Current.Left;     //转进左子树
                    Blanks += 1;
                }
                Current = stack.Pop().Right;    //转进右子树
                if (Current == null)
                {
                    Blanks -= 1;
                }
                if (sgn && stack.Count == 0)
                {
                    Blanks -= 1;
                    sgn = false;
                }
            }
        }
    }
}
