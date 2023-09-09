using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace AiSD8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        BinarySearchTree tree; //Объявление дерева
        string filePath = "C:\\Users\\Александр\\source\\repos\\text.txt"; //Инициализация пути до файла с текстом
        List<string> wordsList1; //Объявление списка слов в порядке следования в файле
        List<string> wordsList2; //Объявление списка слов в противоположном порядке следования в файле


        private void button2_Click(object sender, EventArgs e) //Обновление элемента TreeView (Update Diagramm)
        {
            treeView1.Nodes.Clear();
            BinarySearchTree.Node node = tree.GetRoot();
            TreeNode rootNode = new TreeNode(node.word);
            treeView1.Nodes.Add(rootNode);
            AddChildNodes(rootNode, node);
        }


        static void AddChildNodes(TreeNode parentNode, BinarySearchTree.Node node) //Рекурентная функция добавляющая элементы дерева в TreeView
        {
            if (node.left != null)
            {
                TreeNode leftNode = new TreeNode(node.left.word); 
                parentNode.Nodes.Add(leftNode); //Добавление левого узла
                AddChildNodes(leftNode, node.left); //Вызов функции для левого поддерева
            }

            if (node.right != null)
            {
                TreeNode rightNode = new TreeNode(node.right.word); 
                parentNode.Nodes.Add(rightNode); //Добавление правого узла
                AddChildNodes(rightNode, node.right); //Вызов функции для правого поддерева
            }
        }


        private void Form1_Load(object sender, EventArgs e) //Загрузка формы
        {
            wordsList1 = GetWordsFromFile(filePath); //Инициализация списка слов с помощью вспомогательной функции
            wordsList2 = new List<string>(wordsList1); //Инициализация 2го списка слов
            wordsList2.Reverse(); //Смена порядка следования слов во 2ом списке
            tree = new BinarySearchTree(); //Инициализация дерева

            // Вставка слов в дерево
            foreach (string word in wordsList1)
            {
                tree.Insert(word);
            }

            textBox1.Text += "Список слов" + Environment.NewLine; //Вывод списка слов

            foreach(string word in wordsList1)
            {
                textBox1.Text += word + "; ";
            }
        }


        static List<string> GetWordsFromFile(string filePath) //Вспомогательная функция
        {
            List<string> wordsList = new List<string>();
            // Считывание содержимого файла
            string fileContent = File.ReadAllText(filePath);

            // Разделители слов
            char[] separators = { ' ', '\t', '\n', '\r', ',', '.', ';', ':', '!', '?', '\"', '\'' };

            // Разделение текста на слова
            string[] words = fileContent.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            // Добавление слов в список
            foreach (string word in words)
            {
                if (!wordsList.Contains(word))
                {
                    wordsList.Add(word);
                }
            }
            return wordsList;

        }


        private void button3_Click(object sender, EventArgs e) //Поиск элемента в дереве (Tree Search)
        {
            Stopwatch sw = new Stopwatch();
            string input = textBox4.Text;

            sw.Start();
            bool result = tree.Search(input);
            sw.Stop();

            if (result == true)
            {
                textBox1.Text += Environment.NewLine + Environment.NewLine + "Слово " + '"' + input + '"' + " найдено за (в дереве): " + sw.ElapsedTicks.ToString() + " тиков" + Environment.NewLine;
            }
            else
            {
                textBox1.Text += Environment.NewLine + Environment.NewLine + "Слово " + '"' + input + '"' + " НЕ найдено (в дереве), на попытку поиска ушло " + sw.ElapsedTicks.ToString() + " тиков" + Environment.NewLine;
            }
        }


        private void button1_Click(object sender, EventArgs e) //Поиск элемента в списке (List Search)
        {
            Stopwatch sw = new Stopwatch();
            string input = textBox5.Text;
            bool result = false;

            sw.Start();
            for(int i = 0; i < wordsList1.Count; i++)
            {
                int comparison = string.Compare(wordsList1[i], input, StringComparison.CurrentCultureIgnoreCase);

                if (comparison == 0)
                {
                    result = true;
                    break;
                }
            }
            sw.Stop();

            if (result == true){
                textBox1.Text += Environment.NewLine + Environment.NewLine + "Слово " + '"' + input + '"' + " найдено за (в списке): " + sw.ElapsedTicks.ToString() + " тиков" + Environment.NewLine; 
            }
            else
            {
                textBox1.Text += Environment.NewLine + Environment.NewLine + "Слово " + '"' + input + '"' + " НЕ найдено (в списке), на попытку поиска ушло " + sw.ElapsedTicks.ToString() + " тиков" + Environment.NewLine;
            }
        }


        private void button6_Click(object sender, EventArgs e) //Вывод элементов в порядке возрастания (In Order)
        {
            string[] s = tree.InOrderTraversal();

            textBox1.Text += Environment.NewLine + Environment.NewLine;

            foreach (string s2 in s)
            {
                textBox1.Text += s2 + "; ";
            }
            textBox1.Text += Environment.NewLine + Environment.NewLine;
        }


        private void button7_Click(object sender, EventArgs e) //Вывод элементов в порядке убывания (Reverse Order)
        {
            string[] s = tree.ReverseOrderTraversal();

            textBox1.Text += Environment.NewLine + Environment.NewLine;

            foreach (string s2 in s)
            {
                textBox1.Text += s2 + "; ";
            }
            textBox1.Text += Environment.NewLine + Environment.NewLine;
        }
    }
}
