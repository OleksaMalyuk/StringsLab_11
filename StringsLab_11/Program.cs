using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringsLab_11
{
    class StringBase : IComparable                  // базовый класс Строка
    {
        // Поля класса
        protected string value;                     // содержит символы строки
        // Конструкторы класса
        public StringBase() { this.value = ""; }
        public StringBase(char ch) { this.value = Convert.ToString(ch); }
        public StringBase(string s) { this.value = s; }
        // переопределение для сравнения по значению строк
        override public bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            StringBase temp = (StringBase)obj;
            return this.value == temp.value;
        }
        override public int GetHashCode() { return value.GetHashCode(); }
        override public string ToString() { return this.value; }
        // реализация метода сравнения двух строк
        virtual public int CompareTo(object obj)
        {
            StringBase temp = (StringBase)obj;
            return this.value.CompareTo(temp.value);
        }
        // сложение двух строк
        virtual public object Addition(object obj)
        {
            StringBase temp = (StringBase)obj;
            return new StringBase(this.value + temp.value);
        }
        // Вычитание (не имеет смысла для строк, должен быть перекрыт в классе-потомке)
        virtual public object Subtraction(object obj) { return null; }

        // перегрузка операций +- <>
        public static StringBase operator +(StringBase s1, StringBase s2)
        {
            return (StringBase)s1.Addition(s2);
        }
        public static StringBase operator -(StringBase s1, StringBase s2)
        {
            return (StringBase)s1.Subtraction(s2);
        }
        public static bool operator >(StringBase s1, StringBase s2)
        {
            return s1.CompareTo(s2) > 0;
        }
        public static bool operator <(StringBase s1, StringBase s2)
        {
            return s1.CompareTo(s2) < 0;
        }
    }

    class StringDecimal : StringBase                        // производный класс "Десятичная строка" от класса "Строка"
    {
        // поля класса
        protected int number_value;                         // содержит числовое выражение строки (для совершения арифм. операций)
        // конструкторы
        public StringDecimal()                              // конструктор по умолчанию
        {
            this.number_value = 0;
            this.value = "0";
        }
        public StringDecimal(char ch)                       // конструктор из символа                       
        {
            this.number_value = (int)Char.GetNumericValue(ch);
            if (this.number_value == -1)
                this.number_value = 0;
            this.value = Convert.ToString(this.number_value);
        }
        public StringDecimal(string s)                      // конструктор из строки
        {
            try
            {
                this.number_value = Convert.ToInt32(s);
            }
            catch
            {
                this.number_value = 0;
            }
            this.value = Convert.ToString(number_value);
        }
        public StringDecimal(int number)                    // конструктор из числа
        {
            this.number_value = number;
            this.value = Convert.ToString(number);
        }

        override public bool Equals(object obj)             // проверяет арифметическое равенство строк (как два числа)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            StringDecimal temp = (StringDecimal)obj;
            return number_value == temp.number_value;
        }
        override public int GetHashCode() { return number_value.GetHashCode(); }
        override public int CompareTo(object obj)           // Сравнивает строку по значению с другой как два числа
        {
            StringDecimal temp = (StringDecimal)obj;
            if (this.number_value > temp.number_value) return 1;
            if (this.number_value < temp.number_value) return -1;
            return 0;
        }
        override public object Addition(object obj)         // Арифметическое сложение
        {
            try
            {
                StringDecimal temp = (StringDecimal)obj;
                return new StringDecimal(this.number_value + temp.number_value);
            }
            catch
            {
                return base.Addition(obj);                  // если не удалось сложить как числа - складываем как строки
            }
        }
        override public object Subtraction(object obj)      // Арифметическое вычитание
        {
            try
            {
                StringDecimal temp = (StringDecimal)obj;
                return new StringDecimal(this.number_value - temp.number_value);
            }
            catch
            {
                return base.Subtraction(obj);                  // если не удалось сложить как числа - складываем как строки
            }
        }
    }

    class Class1
    {
        static void Main()
        {
            List<StringBase> list = new List<StringBase>();
            list.Add(new StringDecimal("150"));
            list.Add(new StringDecimal(25));
            list.Add(new StringBase("abc"));

            Console.WriteLine("{0} == {1} is {2}", list[0], list[1], list[0] == list[1]);

            foreach(StringBase element in list)
            {
                Console.Write("{0}\t", element);
            }
            Console.WriteLine("");
        }
    }
}