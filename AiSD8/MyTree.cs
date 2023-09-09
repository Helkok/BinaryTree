using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AiSD8
{
    public class BinarySearchTree //Класс дерева
    {
        public class Node //Класс вершины
        {
            public string word; //Значение вершины
            public Node left; //Ссылка на левую дочернюю вершину (левое поддерево)
            public Node right; //Ссылка на правую дочернюю вершину (правое поддерево)

            public Node(string word) //Конструктор вершины
            {
                this.word = word; //Инициализация значения в вершине
                left = null;
                right = null;
            }
        }

        private Node root;
        private List<Node> nodes; //Список ссылок на вершины для данного дерева

        public BinarySearchTree() //Пустой конструктор
        {
            root = null;
            nodes = new List<Node>();
        }

        public void Insert(string word) //Вставка значения
        {
            root = InsertRec(root, word); //Первый вызов рекурентной функции
        }

        private Node InsertRec(Node root, string word) //Рекурентная функция (рекурсия - вызов сам себя)
        {
            if (root == null) //Если текущаяя вершина (root) == null
            {
                root = new Node(word); //Инициализация нового узла
                nodes.Add(root); //Добавление узла
                return root; //Выход из рекурсии
            }

            int comparison = string.Compare(word, root.word, StringComparison.CurrentCultureIgnoreCase); //Сравнение слов

            if (comparison < 0)
                root.left = InsertRec(root.left, word); //Вызов рекурентной функции для левого дочернего узла
            else if (comparison > 0)
                root.right = InsertRec(root.right, word); //Вызов рекурентной функции для правого дочернего узла

            return root; //В случае равенства слов, добавления не происходит, происходит возвращение к вершине
        }

        public bool Search(string word) //Поиск
        {
            return SearchRec(root, word); //Первый вызов рекурентного поиска
        }

        private bool SearchRec(Node root, string word) //Рекурентная функция поиска
        {
            if (root == null || root.word.Equals(word)) //Если слова нет или оно найдено, возвращает соответствующее булевское значение
                return root != null;

            int comparison = string.Compare(word, root.word, StringComparison.CurrentCultureIgnoreCase); //Сравнение слов

            if (comparison < 0)
                
                return SearchRec(root.left, word); //Переход в левое поддерево

            return SearchRec(root.right, word); //Переход в правое поддерево
        }


        public string[] ReverseOrderTraversal() //Функция вывода элементов дерева в порядке убывания
        {
            List<string> words = new List<string>(); //Инициализация списка, в который будут помещаться элементы
            ReverseOrderTraversalRec(root, words); //Вызов рекурентной функции
            return words.ToArray(); //Возврацение списка, преобразованного в массив
        }

        private void ReverseOrderTraversalRec(Node root, List<string> words) //Рекурентная функция вывода элементов в порядке убывания
        {
            if (root != null) //Добавление ненулевых элементов
            {
                ReverseOrderTraversalRec(root.right, words); //Вызов функции для элементов справа (вначале добавит все элементы из крайней правой ветви)
                words.Add(root.word); //Добавление слова в список
                ReverseOrderTraversalRec(root.left, words); //Вызов функции для элементов слева (в самом конце добавит элементы в самой крайней левой ветви)
            }
        }

        public string[] InOrderTraversal() //Аналогичные функции для функций выше, но для вывода элементов в поряке возрастаниия
        {
            List<string> words = new List<string>();
            InOrderTraversalRec(root, words);
            return words.ToArray();
        }

        private void InOrderTraversalRec(Node root, List<string> words)
        {
            if (root != null)
            {
                InOrderTraversalRec(root.left, words);
                words.Add(root.word);
                InOrderTraversalRec(root.right, words);
            }
        }

        public List<Node> GetAllNodes() //Возврат всех вершин дерева
        {
            return nodes;
        }

        public Node GetRoot() //Возврат корневого элемента
        {
            return this.root;
        }
    }
}
