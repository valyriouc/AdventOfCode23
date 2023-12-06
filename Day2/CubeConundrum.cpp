#include<iostream>
#include <vector>
#include<string>
#include<fstream>
#include<sstream>
#include<tuple>
#include<cmath>

struct Set {
private:
    unsigned int m_red = { 0 };
    unsigned int m_green = { 0 };
    unsigned int m_blue = { 0 };

public:
    Set(unsigned int red, unsigned int green, unsigned int blue)  {
        m_red = red;
        m_green = green;
        m_blue = blue;
    }

    auto isValidSet(
        unsigned int maxRed, 
        unsigned int maxGreen, 
        unsigned int maxBlue) -> bool {
        return m_red <= maxRed && m_green <= maxGreen && m_blue <= maxBlue; 
    }
};

struct Game {
private:
    unsigned int m_number = { 0 };
    std::vector<Set> m_sets;

public:
    Game(unsigned int gameNumber) {
        m_number = gameNumber;
    }

public:
    auto getGameNumber() -> unsigned int { return m_number; }

    auto addSet(Set set) -> void {
        m_sets.push_back(set);
    }

    auto isValidGame(unsigned int mR, unsigned int mG, unsigned int mB) -> bool {
        for (Set& s : m_sets) {
            if (!s.isValidSet(mR, mG, mB)) {
                return false;
            }
        }

        return true;
    }
};

class GameManager {
private:
    std::vector<Game*> m_games;

    unsigned int m_max_red;
    unsigned int m_max_green;
    unsigned int m_max_blue;

public:
    GameManager(unsigned int maxRed, unsigned int maxGreen, unsigned int maxBlue) {
        m_max_red = maxRed;
        m_max_green = maxGreen;
        m_max_blue = maxBlue;
    }

    ~GameManager() {
        for(const Game* ptr : m_games) {
            delete ptr;
            ptr = nullptr;
        }
    }

private:
    enum ParsingState { game, set };

    auto parseLine(std::string line) -> Game* {
        // Game 1: 3 red, 4 blue, 4 green, 8 red; 2 green; 6 blue;...

        std::stringstream lineStream(line);
        ParsingState state = game;
        std::string block;

        Game* gamePtr = nullptr;

        while(lineStream.good()) {
            std::string word;
            lineStream >> word;

            if (state == game) {
                if (word.back() == ':') {
                    unsigned int gameNumber = std::stoi(word.substr(0, word.length() - 1));
                    gamePtr = new Game(gameNumber);
                    state = set;
                }
            } 
            else {
                if (word.back() == ';') {
                    Set set = parseBlock(block + word);
                    gamePtr->addSet(set);
                    block = "";
                }
                else {
                    block += word;
                }
            }
        }

        Set set = parseBlock(block);
        gamePtr->addSet(set);

        return gamePtr;
    }

    auto generateNumber(std::vector<unsigned int> vec) -> unsigned int{
        unsigned int number = 0;
        for (int i = vec.size() - 1; i >= 0; i--) {
            number += vec[i] * std::pow(10, i);
        }
        
        return number;
    }
    
    auto parseBlock(std::string block) -> Set {
        unsigned int r = 0, g = 0, b = 0;
        std::vector<unsigned int> tmps;
        for (int i=0; i < block.length(); i++) {
            switch(block[i]) {
                case ',':
                    tmps.clear();
                    break;
                case 'r':
                    r += generateNumber(tmps);
                    i += 2;
                    break;
                case 'g':
                    g += generateNumber(tmps);
                    i += 4;
                    break;
                case 'b':
                    b += generateNumber(tmps);
                    i += 3;
                    break;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    tmps.emplace(tmps.begin(), (int)block[i] - 48);
                    break;
                default:
                    break;
            }

        }

        return Set(r, g, b);
    }
    
public:
    auto loadGamesFrom(std::string inputPath) -> void {
        std::ifstream inputStream(inputPath);

        if (inputStream.is_open()) {
            while(inputStream.good()) {
                std::string line;
                std::getline(inputStream, line);
                // Parsing the line 
                Game* game = parseLine(line);
                m_games.push_back(game);
            }
        }

        inputStream.close();
    }

    auto calculateResult(std::string outputPath) -> void {
        int result = 0;
        for (Game* game : m_games) {
            if (game->isValidGame(m_max_red, m_max_green, m_max_blue)) {
                result += game->getGameNumber();
            }
        }

        std::ofstream outputStream(outputPath);
        if (outputStream.is_open()) {
            outputStream << result;
        }
    }
};

// TODO: Skip empty line
int main() {
    GameManager manager(12, 13, 14);

    manager.loadGamesFrom("./inputs/input.txt");

    manager.calculateResult("./inputs/result.txt");

    return 0;
}