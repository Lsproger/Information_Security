#pragma once
#include <ctime>


struct Timer 
{

private:

	
	clock_t start;
	clock_t end;
	long delta;
	long overalTime = 0;

	
public:

	const unsigned int N = 1000;

	Timer()
	{
		start = end = delta = 0;
	};

	void Start() 
	{
		start = clock();
	}

	void Stop() 
	{
		end = clock();
	}

	long Delta() 
	{
		overalTime += (end - start);
		return end - start;
	}

	long AverageTime() 
	{
		return overalTime / N;
	}
};