#include<iostream>
#include<fstream>
#include <istream>
#include <vector>

class Day1Handler {
private:
    std::vector<std::string> inputs;

    std::ifstream m_input_stream;
    std::ofstream m_output_stream;

public:
    Day1Handler(std::string inputPath, std::string resultPath, bool handleSecond) {

        m_input_stream.open(inputPath);
        m_output_stream.open(resultPath);

        while(!m_input_stream.eof()) {
            std::string line;
            m_input_stream >> line;
            inputs.push_back(line);
        }

        if (handleSecond) {
            replaceWordsWithNumber();
        }
    }

    ~Day1Handler() {
        m_input_stream.close();
        m_output_stream.close();
    }

private:
    auto replaceWordsWithNumber() -> void {
        for(int i = 0; i < inputs.size(); i++) {
            std::string newLine;
            for(int l = 0; l < inputs[i].size(); l++) {
                std::string sub;
                switch(inputs[i][l]) {
                    case 'o':
                        sub = inputs[i].substr(l+1, 2);
                        if (sub == "ne") {
                            newLine += '1';
                            l += 1;
                        }
                        break;
                    case 't':
                        sub = inputs[i].substr(l+1, 2);
                        if (sub == "wo") {
                            newLine += '2';
                            l += 1;
                        }
                        else {
                            sub = inputs[i].substr(l+1, 4);
                            if (sub == "hree") {
                                newLine += '3';
                                l += 3;
                            }
                        }
                        break;
                    case 'f':
                        sub = inputs[i].substr(l+1, 3);
                        if (sub == "our") {
                            newLine += '4';
                            l += 2;
                        }
                        else {
                            sub = inputs[i].substr(l+1, 3);
                            if (sub == "ive") {
                                newLine += '5';
                                l += 2;
                            }
                        }
                        break;
                    case 's':
                        sub = inputs[i].substr(l+1, 2);
                        if (sub == "ix") {
                            newLine += '6';
                            l += 1;
                        }
                        else {
                            sub = inputs[i].substr(l+1, 4); 
                            if (sub == "even") {
                                newLine += '7';
                                l += 3;
                            }
                        }
                        break;
                    case 'e': 
                        sub = inputs[i].substr(l+1, 4);
                        if (sub == "ight") {
                            newLine += '8';
                            l += 3;
                        }
                        break;
                    case 'n':
                        sub = inputs[i].substr(l+1, 3);
                        if (sub == "ine") {
                            newLine += '9';
                            l += 2;
                        }
                        break;
                    case 'z':
                        sub = inputs[i].substr(l+1, 3);
                        if (sub == "ero") {
                            newLine += '0';
                            l += 2;
                        }
                        break;
                    default:
                        newLine += inputs[i][l];
                        break;
                }
            }

            inputs[i] = newLine;
        }
    }

public:
    auto generateResult() -> void {
        std::vector<int> numbers;
        for(const std::string line : inputs) {
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
        }

        int result = 0;
        for(const int var : numbers) {
            result += var;
        }

        m_output_stream << result;
    } 
};
 
int main() {
    // reading the input 
    Day1Handler handler("./inputs/input.txt", "./inputs/result2.txt", true);

    handler.generateResult();
}