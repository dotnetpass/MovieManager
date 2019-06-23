// pch.cpp: 与预编译标头对应的源文件

#include "pch.h"
#include <stdio.h>

using namespace std;

// 当使用预编译的头时，需要使用此源文件，编译才能成功。

extern "C" __declspec(dllexport) int Add(int x, int y) {
	return x + y;
}

extern "C" __declspec(dllexport) double CalculateMeanScore(const double * score_array, int length) {
	double result = 0;
	for (int i = 0; i < length; i++) {
		
		result += *(score_array + i);
	}
	return result / length;
}

extern "C" __declspec(dllexport) double CalculateTotalScore(const double* score_array, int length) {
	double result = 0;
	for (int i = 0; i < length; i++) {

		result += *(score_array + i);
	}
	return result;
}
