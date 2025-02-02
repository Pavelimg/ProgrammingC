#include <SFML/Graphics.hpp>
#include <cmath>
#include <vector>
#include <iostream>
#include <sstream>
#include <unordered_map>
#include <queue>
#include <limits>
#include <fstream>
#include <iomanip> 

const float PI = 3.14159265358979323846f;

struct Edge {
    std::string from;
    std::string to;
    int weight;
};

std::vector<Edge> parseEdges(const std::vector<std::string>& edgesStr) {
    std::vector<Edge> edges;
    for (const auto& edge : edgesStr) {
        std::istringstream edgeStream(edge);

        std::string from, to;
        int weight;
        std::getline(edgeStream, from, '-');
        std::getline(edgeStream, to, '-');
        edgeStream >> weight;
        edges.push_back({ from, to, weight });
    }
    return edges;
}

int getCityIndex(const std::string& city, const std::vector<std::string>& cityNames) {
    auto it = std::find(cityNames.begin(), cityNames.end(), city);
    if (it != cityNames.end()) {
        return std::distance(cityNames.begin(), it);
    }
    return -1;
}

void buildAdjacencyMatrix(const std::vector<Edge>& edges, std::vector<std::vector<int>>& adjacencyMatrix, const std::vector<std::string>& cityNames) {
    const int n = cityNames.size();
    adjacencyMatrix.assign(n, std::vector<int>(n, 0));

    for (const auto& edge : edges) {
        int fromIndex = getCityIndex(edge.from, cityNames);
        int toIndex = getCityIndex(edge.to, cityNames);

        if (fromIndex != -1 && toIndex != -1) {
            adjacencyMatrix[fromIndex][toIndex] = edge.weight;
            adjacencyMatrix[toIndex][fromIndex] = edge.weight;
        }
    }
}

void printAdjacencyMatrix(const std::vector<std::vector<int>>& matrix, const std::vector<std::string>& cities) {
    std::cout << "Matrix of adjacency:\n\n";

    std::cout << std::setw(12) << "     ";
    for (const auto& city : cities) {
        std::cout << std::setw(6) << city.substr(0, 3);
    }
    std::cout << "\n";

    for (int i = 0; i < matrix.size(); ++i) {
        std::cout << std::setw(12) << cities[i].substr(0, 3);
        for (int j = 0; j < matrix[i].size(); ++j) {
            std::cout << std::setw(6) << matrix[i][j];
        }
        std::cout << "\n";
    }
}

void drawRectangle(sf::RenderWindow& window, const sf::Vector2f& start, const sf::Vector2f& end, const sf::Color& color, float thickness) {
    sf::Vector2f direction = end - start;
    float length = std::sqrt(direction.x * direction.x + direction.y * direction.y);
    float angle = std::atan2(direction.y, direction.x) * 180 / PI;

    sf::RectangleShape rectangle(sf::Vector2f(length, thickness));
    rectangle.setPosition(start);
    rectangle.setRotation(angle);
    rectangle.setFillColor(color);

    window.draw(rectangle);
}

void drawWeight(sf::RenderWindow& window, const sf::Vector2f& start, const sf::Vector2f& end, int weight, const sf::Font& font) {
    sf::Vector2f midPoint = (start + end) / 2.0f;
    sf::Text weightText;
    weightText.setFont(font);
    weightText.setString(std::to_string(weight));
    weightText.setCharacterSize(15);
    weightText.setFillColor(sf::Color::White);
    weightText.setPosition(midPoint);
    window.draw(weightText);
}

std::vector<int> dijkstra(const std::vector<std::vector<int>>& adjacencyMatrix, int start, int end) {
    int numVertices = adjacencyMatrix.size();
    std::vector<int> distances(numVertices, std::numeric_limits<int>::max());
    std::vector<int> previous(numVertices, -1);
    distances[start] = 0;

    using Pair = std::pair<int, int>;
    std::priority_queue<Pair, std::vector<Pair>, std::greater<Pair>> queue;
    queue.push({ 0, start });

    while (!queue.empty()) {
        int currentDistance = queue.top().first;
        int currentVertex = queue.top().second;
        queue.pop();

        if (currentDistance > distances[currentVertex]) continue; // Вершина уже обработана с меньшим расстоянием
        for (int neighbor = 0; neighbor < numVertices; ++neighbor) {
            if (adjacencyMatrix[currentVertex][neighbor] > 0) {
                int newDistance = currentDistance + adjacencyMatrix[currentVertex][neighbor];

                if (newDistance < distances[neighbor]) {
                    distances[neighbor] = newDistance;
                    previous[neighbor] = currentVertex;
                    queue.push({ newDistance, neighbor });
                }
            }
        }
        std::cout << std::endl;

        //поэтапный вывод массива
        for (int i = 0; i < distances.size(); i++) {
            std::cout << distances[i] << " ";
        }
    }


    std::vector<int> path;
    for (int at = end; at != -1; at = previous[at]) {
        path.push_back(at);
    }
    std::reverse(path.begin(), path.end());

    if (path.size() == 1 && path[0] != start) {
        return {};
    }
    return path;
}

std::vector<int> findPathThroughC(std::vector<std::vector<int>>& adjacencyMatrix, int start, int via, int end) {
    // Найти путь из A в C
    std::vector<int> pathToVia = dijkstra(adjacencyMatrix, start, via);
    if (pathToVia.empty() || pathToVia.back() != via) return {};

    // Удалить найденный путь из графа, чтобы не повторяться
    for (size_t i = 1; i < pathToVia.size(); ++i) {
        int from = pathToVia[i - 1];
        int to = pathToVia[i];
        adjacencyMatrix[from][to] = 0;
        adjacencyMatrix[to][from] = 0;
    }

    // Найти путь из C в B
    std::vector<int> pathFromVia = dijkstra(adjacencyMatrix, via, end);
    if (pathFromVia.empty() || pathFromVia.back() != end) return {};

    // Соединить пути
    pathToVia.insert(pathToVia.end(), pathFromVia.begin() + 1, pathFromVia.end());
    return pathToVia;
}



int main() {
    sf::RenderWindow window(sf::VideoMode(800, 600), "Graph Visualization");

    std::vector<std::string> verticesNames = {
        "Baltiysk", "Primorsk", "Kaliningrad", "Kolosovka", "Yantarny", "Svetlogorsk",
        "Pionersky", "Zelenogradsk", "Guryevsk", "Gvardeisk"
    };
    int numVertices = verticesNames.size();
    sf::Vector2f center(400, 300);
    float radius = 250.0f;
    std::vector<sf::CircleShape> vertices(numVertices);
    std::vector<sf::Vector2f> positions(numVertices);

    sf::Font font;
    if (!font.loadFromFile("C:/Users/Pavel/source/repos/SFML-sourse/arial.ttf")) {
        std::cerr << "Ошибка загрузки шрифта\n";
        return -1;
    }

    std::vector<sf::Text> labels(numVertices);
    for (int i = 0; i < numVertices; ++i) {
        float angle = 2 * PI * i / numVertices;
        positions[i] = center + sf::Vector2f(radius * std::cos(angle), radius * std::sin(angle));
        vertices[i].setRadius(20);
        vertices[i].setFillColor(sf::Color::Red);
        vertices[i].setPosition(positions[i] - sf::Vector2f(20, 20));
        labels[i].setFont(font);
        labels[i].setString(verticesNames[i]);
        labels[i].setCharacterSize(20);//размер шрифта  
        labels[i].setFillColor(sf::Color::White);
        labels[i].setPosition(positions[i].x - 10, positions[i].y - 10);
    }

    std::vector<std::string> edgesStr = {
        "Baltiysk-Primorsk-14", "Baltiysk-Kaliningrad-45", "Primorsk-Kaliningrad-40",
        "Primorsk-Kolosovka-24", "Yantarny-Primorsk-20", "Yantarny-Svetlogorsk-24",
        "Svetlogorsk-Kolosovka-27", "Kolosovka-Kaliningrad-24", "Svetlogorsk-Pionersky-5",
        "Pionersky-Kaliningrad-44", "Pionersky-Zelenogradsk-24", "Zelenogradsk-Kaliningrad-33",
        "Zelenogradsk-Guryevsk-25", "Guryevsk-Kaliningrad-13", "Guryevsk-Gvardeisk-37",
        "Gvardeisk-Kaliningrad-53"
    };

    std::vector<Edge> edges = parseEdges(edgesStr);

    std::vector<std::vector<int>> adjacencyMatrix;
    buildAdjacencyMatrix(edges, adjacencyMatrix, verticesNames);
    printAdjacencyMatrix(adjacencyMatrix, verticesNames);

    std::vector<int> path;

    while (window.isOpen()) {
        sf::Event event;
        while (window.pollEvent(event)) {
            if (event.type == sf::Event::Closed) {
                window.close();
            }if (event.type == sf::Event::KeyPressed) {
                if (event.key.code == sf::Keyboard::Space) {
                    int start = getCityIndex("Guryevsk", verticesNames);
                    int via = getCityIndex("Kolosovka", verticesNames);
                    int end = getCityIndex("Kaliningrad", verticesNames);
                    path = findPathThroughC(adjacencyMatrix, start, via, end);

                    if (!path.empty()) {
                        // Вывод списка вершин маршрута
                        std::cout << "Array of peaks:\n";
                        for (int i : path) {
                            std::cout << verticesNames[i] << " ";
                        }
                        std::cout << std::endl;

                        // Вывод списка рёбер маршрута
                        std::cout << "\nArray of edges:\n";
                        for (size_t i = 1; i < path.size(); ++i) {
                            int from = path[i - 1];
                            int to = path[i];

                            // Найдём ребро между этими вершинами
                            for (const auto& edge : edges) {
                                if ((edge.from == verticesNames[from] && edge.to == verticesNames[to]) ||
                                    (edge.from == verticesNames[to] && edge.to == verticesNames[from])) {
                                    std::cout << verticesNames[from] << " - " << verticesNames[to] << " : " << edge.weight << "\n";
                                    break;
                                }
                            }
                        }
                    }
                    else {
                        std::cout << "Kuda edem???????.\n";
                    }
                }
            }
        }

        window.clear();

        // Отрисовка рёбер с учётом маршрута
        for (const auto& edge : edges) {
            int fromIndex = getCityIndex(edge.from, verticesNames);
            int toIndex = getCityIndex(edge.to, verticesNames);

            sf::Color edgeColor = sf::Color::White;

            if (!path.empty()) {
                for (size_t i = 1; i < path.size(); ++i) {
                    if ((path[i - 1] == fromIndex && path[i] == toIndex) || (path[i - 1] == toIndex && path[i] == fromIndex)) {
                        edgeColor = sf::Color::Green; // Рёбра на пути будут зелёными
                        break;
                    }
                }
            }

            drawRectangle(window, positions[fromIndex], positions[toIndex], edgeColor, 5);
            drawWeight(window, positions[fromIndex], positions[toIndex], edge.weight, font);
        }

        // Отрисовка вершин
        for (int i = 0; i < numVertices; ++i) {
            if (!path.empty()) {
                if (i == path.front()) {
                    vertices[i].setFillColor(sf::Color::Green); // Начальная вершина
                }
                else if (i == path.back()) {
                    vertices[i].setFillColor(sf::Color::Magenta); // Конечная вершина
                }
                else if (std::find(path.begin(), path.end(), i) != path.end()) {
                    if (i == getCityIndex(verticesNames[path[path.size() / 2]], verticesNames)) {
                        vertices[i].setFillColor(sf::Color::Yellow); // Вершина via
                    }
                    else {
                        vertices[i].setFillColor(sf::Color::Blue); // Промежуточные вершины
                    }
                }
                else {
                    vertices[i].setFillColor(sf::Color::Red); // Вершины вне маршрута
                }
            }
            else {
                vertices[i].setFillColor(sf::Color::Red); // Вершины по умолчанию
            }

            window.draw(vertices[i]);
            window.draw(labels[i]);
        }



        window.display();
    }

    return 0;
}
