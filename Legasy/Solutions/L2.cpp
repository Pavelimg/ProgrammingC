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


double secantMethod(double x0, double x1, double eps) {
    double x_new = x1;
    double x_old = x0;
    double fx_new = f(x_new);
    double fx_old = f(x_old);

    while (std::abs(fx_new) > eps) {
        double denominator = fx_new - fx_old;
        if (denominator == 0) {
            std::cout << "Делитель равен нулю! Итерация остановлена." << std::endl;
            break;
        }

        double x_next = x_new - (fx_new * (x_new - x_old)) / denominator;
        x_old = x_new;
        x_new = x_next;
        fx_old = fx_new;
        fx_new = f(x_new);
    }

    return x_new;
}

int main() {
    double a = 2.0; // Левая граница интервала
    double b = 3.0; // Правая граница интервала
    double eps = 0.00001; // Желаемая точность

    double root1 = bisectionMethod(a, b, eps);
    double root2 = bisectionMethod(a, b, eps);
    
    std::cout << "                          e = 0.1 | e = 0.01 | e = 0.0001" << std::endl,
    std::cout << "Метд половинного деления:  " << bisectionMethod(a, b, 0.1) << "    " << bisectionMethod(a, b, 0.01) << "   " << bisectionMethod(a, b, 0.0001) << "    " << std::endl;
    std::cout << "Метод секущей           :  " << secantMethod(a, b, 0.1) << "   " << secantMethod(a, b, 0.01) << "   " << secantMethod(a, b, 0.0001) << "    " << std::endl;
    

    return 0;
}