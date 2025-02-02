#include <SFML/Graphics.hpp>
#include <cmath>
#include <vector>
#include <iostream>
#include <random>
#include <ctime>
#include <memory>

const float PI = 3.14159265358979323846f;

struct Edge {
    int from;
    int to;
};

// Функция для вычисления расстояния между цветами
float colorDistance(const sf::Color& c1, const sf::Color& c2) {
    return std::sqrt(
        std::pow(c1.r - c2.r, 2) +
        std::pow(c1.g - c2.g, 2) +
        std::pow(c1.b - c2.b, 2)
    );
}

// Функция для генерации случайного цвета с учетом окрестности
sf::Color generateRandomColor(const std::vector<sf::Color>& existingColors, float minDistance) {
    sf::Color newColor;
    bool isValid;
    do {
        newColor = sf::Color(std::rand() % 256, std::rand() % 256, std::rand() % 256);
        isValid = true;
        for (const auto& color : existingColors) {
            if (colorDistance(newColor, color) < minDistance) {
                isValid = false;
                break;
            }
        }
    } while (!isValid);
    return newColor;
}

// Функция для рисования стрелки между двумя вершинами с заданной толщиной линии
void drawArrow(sf::RenderWindow& window, sf::Vector2f start, sf::Vector2f end, sf::Color color, float vertexRadius, float lineThickness, bool doubleSided = false) {
    // Вычисляем направление и нормализуем его
    sf::Vector2f direction = end - start; // Вектор направления
    float length = std::sqrt(direction.x * direction.x + direction.y * direction.y);
    sf::Vector2f unitDirection = direction / length; // Нормализованный вектор направления

    // Смещаем начало и конец стрелки так, чтобы они не заходили в круги
    sf::Vector2f adjustedStart = start + unitDirection * vertexRadius; // Начало стрелки
    sf::Vector2f adjustedEnd = end - unitDirection * vertexRadius; // Конец стрелки

    // Создаем линии (тело стрелки)
    sf::RectangleShape line(sf::Vector2f(length, lineThickness));
    line.setPosition(adjustedStart);
    line.setFillColor(color);
    line.setRotation(std::atan2(direction.y, direction.x) * 180 / PI);
    window.draw(line);

    // Размер стрелки
    float arrowSize = 15.0f;

    // Рисуем треугольник стрелки
    // Вычисляем точки для треугольника (основание и концы)
    sf::Vector2f tip = adjustedEnd; // Вершина стрелки
    sf::Vector2f base1 = adjustedEnd - unitDirection * arrowSize + sf::Vector2f(-unitDirection.y, unitDirection.x) * (arrowSize / 2.0f);
    sf::Vector2f base2 = adjustedEnd - unitDirection * arrowSize - sf::Vector2f(-unitDirection.y, unitDirection.x) * (arrowSize / 2.0f);

    // Создаем треугольник стрелки
    sf::ConvexShape arrowHead;
    arrowHead.setPointCount(3);
    arrowHead.setPoint(0, tip);
    arrowHead.setPoint(1, base1);
    arrowHead.setPoint(2, base2);
    arrowHead.setFillColor(color);

    // Рисуем треугольник
    window.draw(arrowHead);

    // Если требуется двойной наконечник, рисуем его с другой стороны
    if (doubleSided) {
        sf::Vector2f reverseTip = adjustedStart; // Вершина обратной стрелки
        sf::Vector2f reverseBase1 = adjustedStart + unitDirection * arrowSize + sf::Vector2f(-unitDirection.y, unitDirection.x) * (arrowSize / 2.0f);
        sf::Vector2f reverseBase2 = adjustedStart + unitDirection * arrowSize - sf::Vector2f(-unitDirection.y, unitDirection.x) * (arrowSize / 2.0f);

        sf::ConvexShape reverseArrowHead;
        reverseArrowHead.setPointCount(3);
        reverseArrowHead.setPoint(0, reverseTip);
        reverseArrowHead.setPoint(1, reverseBase1);
        reverseArrowHead.setPoint(2, reverseBase2);
        reverseArrowHead.setFillColor(color);

        window.draw(reverseArrowHead);
    }
}

// Функция для вывода списка рёбер
void printEdgeList(const std::vector<Edge>& edges) {
    std::cout << "Array reber:\n";
    for (const auto& edge : edges) {
        std::cout << "{" << edge.from << ", " << edge.to << "} ";
    }
    std::cout << "\n";
}// Функция для вывода матрицы смежности
void printAdjacencyMatrix(const std::unique_ptr<std::unique_ptr<int[]>[]>& matrix, int numVertices) {
    std::cout << "Matrix smejnosti:\n";
    for (int i = 0; i < numVertices; ++i) {
        for (int j = 0; j < numVertices; ++j) {
            std::cout << matrix[i][j] << " ";
        }
        std::cout << "\n";
    }
}
/*
void printAdjacencyMatrix(int** matrix, int numVertices) {
    std::cout << "Matrix smejnosti:\n";
    for (int i = 0; i < numVertices; ++i) {
        for (int j = 0; j < numVertices; ++j) {
            std::cout << matrix[i][j] << " ";
        }
        std::cout << "\n";
    }
}
*/

// Функция для вывода списка смежности
void printAdjacencyList(const std::vector<std::vector<int>>& adjList) {
    std::cout << "Array smejnosti:\n";
    for (size_t i = 0; i < adjList.size(); ++i) {
        std::cout << i + 1 << ": ";
        for (int neighbor : adjList[i]) {
            std::cout << neighbor << " ";
        }
        std::cout << "\n";
    }
}

int main() {
    sf::RenderWindow window(sf::VideoMode(800, 600), "Визуализация графа");

    int numVertices = 6;
    sf::Vector2f center(400, 300);
    float radius = 150.0f; // Радиус окружности
    std::vector<sf::CircleShape> vertices(numVertices);
    std::vector<sf::Vector2f> positions(numVertices);

    sf::Font font;
    if (!font.loadFromFile("C:/Users/Pavel/source/repos/SFML-sourse/arial.ttf")) {
        std::cerr << "Ошибка загрузки шрифта\n";
        return -1;
    }

    std::vector<sf::Text> labels(numVertices);

    // Вычисление координат для вершин графа по окружности
    for (int i = 0; i < numVertices; ++i) {
        float angle = 2 * PI * i / numVertices;
        positions[i] = sf::Vector2f(center.x + radius * std::cos(angle), center.y + radius * std::sin(angle)); // Позиция вершины
    }

    // Установка вершин и меток
    for (int i = 0; i < numVertices; ++i) {
        vertices[i].setRadius(20); // Радиус вершины
        vertices[i].setOrigin(20, 20); // Центр вершины
        vertices[i].setPosition(positions[i]); // Позиция вершины
        vertices[i].setFillColor(sf::Color::Red);

        labels[i].setFont(font);
        labels[i].setString(std::to_string(i + 1)); // Номер вершины
        labels[i].setCharacterSize(20);
        labels[i].setFillColor(sf::Color::White);
        labels[i].setPosition(positions[i].x - 10, positions[i].y - 15); // Позиция метки
    }

    // Ребра графа
    std::vector<Edge> edges = { {1, 2}, {4, 1}, {1, 6}, {3, 2}, {5, 2}, {3, 4}, {5, 6}, {6, 1}, {4, 3} };

    // Генератор случайных чисел для цветов
    std::srand(std::time(nullptr)); // Инициализация генератора случайных чисел текущим временем

    // Вывод списка рёбер
    printEdgeList(edges);

    // Создание и вывод матрицы смежности
    std::unique_ptr<std::unique_ptr<int[]>[]> adjacencyMatrix = std::make_unique<std::unique_ptr<int[]>[]>(numVertices);
    for (int i = 0; i < numVertices; ++i) {
        adjacencyMatrix[i] = std::make_unique<int[]>(numVertices);
        std::fill(adjacencyMatrix[i].get(), adjacencyMatrix[i].get() + numVertices, 0);
    }
    for (const auto& edge : edges) {
        adjacencyMatrix[edge.from - 1][edge.to - 1] = 1;
    }
    printAdjacencyMatrix(adjacencyMatrix, numVertices);

    // Создание и вывод списка смежности
    std::vector<std::vector<int>> adjacencyList(numVertices);
    for (const auto& edge : edges) {
        adjacencyList[edge.from - 1].push_back(edge.to);
    }
    printAdjacencyList(adjacencyList);

    std::vector<sf::Color> usedColors;
    float minColorDistance = 80.0f; // Минимальное расстояние между цветами
    float lineThickness = 2.0f; // Толщина линии

    // Цвет для рёбер 1-6 и 3-4
    sf::Color specialColor = generateRandomColor(usedColors, minColorDistance);
    usedColors.push_back(specialColor);

    while (window.isOpen()) {
        sf::Event event;
        while (window.pollEvent(event)) {
            if (event.type == sf::Event::Closed)
                window.close();
        }

        window.clear();
        // Рисование рёбер
        for (const auto& edge : edges) {
            sf::Color arrowColor;
            if ((edge.from == 1 && edge.to == 6) || (edge.from == 3 && edge.to == 4)) {
                arrowColor = specialColor;
            }
            else {
                arrowColor = generateRandomColor(usedColors, minColorDistance); // Генерация случайного цвета с учетом окрестности
                usedColors.push_back(arrowColor);
            }
            drawArrow(window, positions[edge.from - 1], positions[edge.to - 1], arrowColor, vertices[0].getRadius(), lineThickness);
        }

        // Рисование вершин
        for (const auto& vertex : vertices) {
            window.draw(vertex);
        }

        // Рисование меток вершин
        for (const auto& label : labels) {
            window.draw(label);
        }

        window.display();
    }
}
