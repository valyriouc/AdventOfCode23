#include<iostream>
#include<fstream>
#include <istream>
#include <vector>

int main() {
    // reading the input 
    std::ifstream inputFile("./input.txt");
    std::vector<int> numbers = {};

    if(inputFile.is_open()){
        std::string line;
        while(!inputFile.eof()) {
            inputFile >> line;
            int first = -1;
            int last = -1;

            for (const char character : line) {
                int ichar = (int)character;
                if (48 <= ichar && 57 >= ichar) {
                    if (first == -1) {
                        first = ichar - 48;
                    }
                    else {
                        last = ichar - 48;
                    }
                }
            }

            if (last == -1) {
                last = first;
            }

            numbers.push_back((first * 10 + last));
            // finding the first and last digit 
        }

        inputFile.close();
    }

    int result = 0;
    for(const int var : numbers) {
        result += var;
    }

    std::ofstream output("./result.txt");
    output << result;
}