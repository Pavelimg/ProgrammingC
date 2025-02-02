#include <iostream>
#include <cmath>

// Функция, которую мы хотим решить методом половинного деления
double f(double x) {
    // Пример функции: x^3 - 2x - 5 = 0
    return pow(x, 3) - 2 * x - 5;
}

// Метод половинного деления
double bisectionMethod(double a, double b, double eps) {
    if (f(a) * f(b) >= 0) {
        std::cout << "Функция должна иметь разные знаки на концах отрезка!" << std::endl;
        return NAN; // Возвращаем NaN, если функция не меняет знак на интервале
    }
    
    double c = a;
    while ((b - a) > eps) {
        c = (a + b) / 2; // Находим середину интервала

        // Проверяем, где находится корень
        if (f(c) == 0.0) {
            break; // Нашли точный корень
        } else if (f(c) * f(a) < 0) {
            b = c; // Корень лежит между a и c
        } else {
            a = c; // Корень лежит между c и b
        }
    }

    return c; // Возвращаем найденный корень
}

int main() {
    double a = 2.0; // Левая граница интервала
    double b = 3.0; // Правая граница интервала
    double eps = 0.00001; // Желаемая точность

    double root = bisectionMethod(a, b, eps);

    if (!std::isnan(root)) {
        std::cout << "Корень уравнения: " << root << std::endl;
    }

    return 0;
}