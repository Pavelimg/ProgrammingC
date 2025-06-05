#include <iostream>
#include <string>

using namespace std;

class Queue {
private:
    struct Node {
        string data;
        Node* next;
        Node(const string& data) : data(data), next(nullptr) {}
    };

    Node* head;

public:
    Queue() : head(nullptr) {}

    // Конструктор копирования 
    Queue(const Queue& other) : head(nullptr) {
        Node* current = other.head;
        while (current) {
            this->enqueue(current->data);
            current = current->next;
        }

    }

    // Деструктор
    ~Queue() {
        while (head) {
            Node* temp = head;
            head = head->next;
            delete temp;
        }
    }

    // Метод добавления элемента (в конец списка)
    void enqueue(const string& data) {
        Node* newNode = new Node(data);
        if (!head) {
            head = newNode;
        }
        else {
            Node* temp = head;
            while (temp->next) {
                temp = temp->next;
            }
            temp->next = newNode;
        }
    }

    // Метод извлечения элемента (из начала)
    string dequeue() {
        if (!head) {
            throw runtime_error("Queue is empty");
        }
        Node* temp = head;
        string data = head->data;
        head = head->next;
        delete temp;
        return data;
    }

    // Метод для просмотра элемента (в начале)
    string peek() const {
        if (!head) {
            throw runtime_error("Queue is empty");
        }
        return head->data;
    }

    // Поиск элемента a (вернуть True/False)
    bool find(const string& data) const {
        Node* current = head;
        while (current) {
            if (current->data == data) {
                return true;
            }
            current = current->next;
        }
        return false;
    }

    // Вычисление веса элемента a (номер в очереди если есть либо -1)
    int weight(const string& data) const {
        Node* current = head;
        int position = 1;
        while (current) {
            if (current->data == data) {
                return position;
            }
            current = current->next;
            position++;
        }
        return -1;
    }

    // +
    Queue operator+(const Queue& other) const {
        Queue result = *this;
        Node* current = other.head;
        while (current) {
            result.enqueue(current->data);
            current = current->next;
        }
        return result;
    }

    // *
    Queue operator*(const Queue& other) const {
        Queue result;
        Node* current1 = head;
        Node* current2 = other.head;
        while (current1 || current2) {
            if (current1) {
                result.enqueue(current1->data);
                current1 = current1->next;
            }
            if (current2) {
                result.enqueue(current2->data);
                current2 = current2->next;
            }
        }
        return result;
    }

    //  – для разворота очереди
    Queue operator-() const {
        Queue result;
        Node* current = head;
        while (current) {
            Node* newNode = new Node(current->data);
            newNode->next = result.head;
            result.head = newNode;
            current = current->next;
        }
        return result;
    }

    // Вывод очереди для проверки
    void print() const {
        Node* current = head;
        while (current) {
            cout << current->data << " ";
            current = current->next;
        }
        cout << endl;
    }
};

int main() {
    setlocale(LC_ALL, "RU");

    Queue q1;
    q1.enqueue("a1");
    q1.enqueue("a2");
    q1.enqueue("a3");
    cout << "Очередь 1: ";
    q1.print();

    Queue q2;
    q2.enqueue("b1");
    q2.enqueue("b2");
    cout << "Очередь 2: ";
    q2.print();

    q1.enqueue("a4");
    cout << "Добавление элемента (в конец списка): ";
    q1.print();

    cout << "Извлечение элемента из начала очереди : " << q1.dequeue() << endl;
    cout << "Очередь после извлечения : ";
    q1.print();
    cout << "Найти 'a3' в Очереди 1: " << (q1.find("a3") ? "True" : "False") << endl;
    cout << "Вес 'a3' в Очереди 1: " << q1.weight("a3") << endl;

    // Проверка +
    Queue q3 = q1 + q2;
    cout << "Очередь 1 + Очередь 2: ";
    q3.print();

    // Проверка *
    Queue q4 = q1 * q2;
    cout << "Очередь 1 * Очередь 2: ";
    q4.print();

    // Проверка унарного оператора –
    Queue q5 = -q1;
    cout << "Обратная очередь 1: ";
    q5.print();
    return 0;
}

