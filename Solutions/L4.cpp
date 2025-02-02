#include <iostream>
#include <string>
#include <stack>

class Node {
public:
    std::string data;
    Node* next;
    Node* prev;

    Node(const std::string& d) : data(d), next(nullptr), prev(nullptr) {}

    ~Node() {
        next = nullptr;
        prev = nullptr;
    }
};

class Stack {
private:
    Node* top;
    Node* base;

public:
    Stack() {
        base = nullptr; // Изначально база не установлена
        top = nullptr;  // Изначально стек пуст
    }

    // не хватает   строчки и в нутри цикла
    ~Stack() {
        while (top != nullptr) { 
            Node* temp = top;
            top = top->prev;

            if (top != nullptr) {
                top->next = nullptr;
            }

            delete temp;
        }
        base = nullptr; // Сбрасываем указатель на базу
        top = nullptr; // Сбрасываем указатель на вершину ????
    
    }

    // Добавление элемента в стек
    void Add(const std::string& value) {
        Node* newNode = new Node(value);
        if (!top) {
            base = newNode; // Первый добавленный элемент становится базой
        }
        else {
            newNode->prev = top;
            top->next = newNode;
        }
        top = newNode;
    }

    // Добавление элемента под крышку
    void AddKr(const std::string& value) {
        if (!top) {
            Add(value); // Если стек пуст, добавляем элемент как обычно
        }
        else if (!top->prev) {
            Node* newNode = new Node(value);
            newNode->next = top;
            top->prev = newNode;
            base = newNode; // Новый элемент становится базой
        }
        else {
            Node* newNode = new Node(value);
            newNode->next = top;
            newNode->prev = top->prev;
            top->prev->next = newNode;
            top->prev = newNode;
        }
    }

    // Просмотр элемента
    std::string Peek() const {
        if (top) {
            return top->data;
        }
        return ""; // Если стек пустой
    }

    // Извлечение элемента (из начала)
    std::string Pop() {
        if (!top) {
            return ""; // Стек пуст
        }
        std::string value = top->data;
        Node* temp = top;
        top = top->prev; // Сдвигаем верхний элемент
        if (top) {
            top->next = nullptr;
        }
        else {
            base = nullptr; // Если стек стал пустым, сбрасываем базу
        }
        delete temp;
        return value;
    }

    // Поиск элемента
    bool Search(const std::string& value) const {
        Node* current = top;
        while (current) {
            if (current->data == value) {
                return true;
            }
            current = current->prev;
        }
        return false;
    }

    // Вычисление веса элемента (номер в стеке)
    int Weight(const std::string& value) const {
        Node* current = top;
        int index = 1; // Индекс начинается с 1, так как top это 1-й элемент
        while (current) {
            if (current->data == value) {
                return index;
            }
            current = current->prev;
            index++;
        }
        return -1; // Элемент не найден
    }

    // Переопределение оператора +
    Stack operator+(const Stack& other) const {
        Stack result;
        std::stack<std::string> tempStack;
        Node* current = top;

        // Копируем элементы текущего стека в временный стек
        while (current) {
            tempStack.push(current->data);
            current = current->prev;
        }

        // Переносим элементы из временного стека в результирующий стек
        while (!tempStack.empty()) {
            result.Add(tempStack.top());
            tempStack.pop();
        }

        current = other.top;
        // Копируем элементы другого стека в временный стек
        while (current) {
            tempStack.push(current->data);
            current = current->prev;
        }
// Переносим элементы из временного стека в результирующий стек
        while (!tempStack.empty()) {
            result.Add(tempStack.top());
            tempStack.pop();
        }

        return result;
    }

    // Переопределение операции *
    Stack operator*(const Stack& other) {
        Stack result;
        Node* a = top;
        Node* b = other.top;
        while (a || b) {
            if (a) {
                result.Add(a->data);
                a = a->prev;
            }
            if (b) {
                result.Add(b->data);
                b = b->prev;
            }
        }
        return -result;
    }

    // Переопределение унарного оператора -
    Stack operator-() const {
        Stack reversed;
        Node* current = top;

        // Переносим элементы из текущего стека в результирующий стек в обратном порядке
        while (current) {
            reversed.Add(current->data);
            current = current->prev;
        }

        return reversed;
    }

    // Метод для печати стека для проверки
    void printStack() const {
        Node* current = top;
        std::cout << "Stack:" << std::endl;
        while (current) {
            std::cout << "  [" << current->data << "]" << std::endl;
            current = current->prev;
        }
    }
};

int main() {
    setlocale(LC_ALL, "RU");
    Stack stackA;
    stackA.Add("A");
    stackA.Add("B");
    stackA.Add("C");

    std::cout << "Stack 1: ";
    stackA.printStack();
    std::cout << std::endl;

    Stack stackB;
    stackB.Add("X");
    stackB.Add("Y");
    std::cout << "Stack 2: ";
    stackB.printStack();
    std::cout << std::endl;

    Stack stackC = stackA + stackB;
    std::cout << "A + B: ";
    stackC.printStack();
    std::cout << std::endl;

    Stack stackD = stackA * stackB;
    std::cout << "A * B: ";
    stackD.printStack();
    std::cout << std::endl;

    Stack stackE = -stackA;
    std::cout << "-A: ";
    stackE.printStack();
    std::cout << std::endl;

    return 0;
}