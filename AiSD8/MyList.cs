using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiSD5
{
    public class MyList //Двухсвязаный список
    {
        private ListNode top;
        private ListNode bottom;
        private int counter;
        public int getCounter() //Получение количества элементов
        {
            return this.counter;
        }
        public MyList(object data) //Конструктор
        {
            top = new ListNode(data);
            counter = 1;
            bottom = top;
            top.setPrevNode(null);
            top.setNextNode(null);
        }
        public void addEnd(object data) //Добавление в конец
        {
            ListNode node = new ListNode(data);
            bottom.setNextNode(node);
            node.setPrevNode(bottom);
            node.setNextNode(null);
            counter++;
            bottom = node;
        }
        public void addStart(object data) //Добавление в начало
        {
            ListNode node = new ListNode(data);
            node.setPrevNode(null);
            node.setNextNode(top);
            counter++;
            top = node;
        }
        public void insertElement(int pos, object data) //Добавление по индексу
        {
            if (pos <= 0)
            {
                this.addStart(data);
            }
            else if((pos>0)&&(pos < counter))
            {
                ListNode currentNode = top;
                for (int i = 1; i < pos; i++)
                {
                    currentNode = currentNode.getNextNode();
                }
                ListNode newNode = new ListNode(data);
                currentNode.getNextNode().setPrevNode(newNode);
                newNode.setNextNode(currentNode.getNextNode());
                currentNode.setNextNode(newNode);
                newNode.setPrevNode(currentNode);
                counter++;
            }
            else
            {
                this.addEnd(data);
            }
        }
        public void setData(int pos, object data) //Установка значения в элемент по индексу
        {
            ListNode node = getElement(pos);
            node.setData(data);
        }
        public ListNode getElement(int pos) //Получение элемента по индексу
        {
            ListNode res = top;
            for (int i = 0; i < pos; i++)
            {
                res = res.getNextNode();
            }
            return res;
        }
        public void Delete(object element)
        {
            ListNode deleted = top;
            if(top.getData() == element)
            {
                top = top.getNextNode();
                top.getPrevNode().setNextNode(null);
                top.setPrevNode(null);
                counter--;
            }
            for(int i =0; i < counter - 1; i++)
            {
                deleted = deleted.getNextNode();
                if (element == deleted.getData())
                {
                    ListNode curr = this.getElement(i);
                    curr.getPrevNode().setNextNode(curr.getNextNode());
                    curr.getNextNode().setPrevNode(curr.getPrevNode());
                    curr.setPrevNode(null);
                    curr.setNextNode(null);
                    counter--;
                    break;
                }
            }
            if(element == bottom.getData())
            {
                bottom = bottom.getPrevNode();
                bottom.getNextNode().setPrevNode(null);
                bottom.setNextNode(null);
                counter--;
            }
            GC.Collect(4, GCCollectionMode.Forced);
        }
        public void removeElement(int pos) //Удаление элемента
        {
            if (pos <= 0)
            {
                top = top.getNextNode();
                top.getPrevNode().setNextNode(null);
                top.setPrevNode(null);
                counter--;
            }
            if((pos > 0) && (pos < counter - 1))
            {
                ListNode curr = this.getElement(pos);
                curr.getPrevNode().setNextNode(curr.getNextNode());
                curr.getNextNode().setPrevNode(curr.getPrevNode());
                curr.setPrevNode(null);
                curr.setNextNode(null);
                counter--;
            }
            else
            {
                bottom = bottom.getPrevNode();
                bottom.getNextNode().setPrevNode(null);
                bottom.setNextNode(null);
                counter--;
            }
            GC.Collect(4, GCCollectionMode.Forced);
        }
    }
    public class ListNode //Элемент
    {
        public ListNode(object data) //Конструктор
        {
            this.data = data;
        }
        private object data;
        public object getData() //Получение данных
        {
            return this.data;
        }
        public void setData(object data) //Установка данных
        {
            this.data = data;
        }
        private MyList parList;
        public MyList GetMyList() //Получение ссылки на сам список
        {
            return this.parList;
        }
        public void setParList(MyList parList) //Установка ссылки на сам список
        {
            this.parList = parList;
        }
        private ListNode prevNode;
        public void setPrevNode(ListNode prevNode) //Установка ссылки на пред элемент
        {
            this.prevNode = prevNode;
        }
        public ListNode getPrevNode() //Получение ссылки на пред элемент
        {
            return this.prevNode;
        }
        private ListNode nextNode;
        public void setNextNode(ListNode nextNode) //Установка ссылки на след элемент
        {
            this.nextNode = nextNode;
        }
        public ListNode getNextNode() //Получение ссылки на след элемент
        {
            return this.nextNode;
        }
    }
}
