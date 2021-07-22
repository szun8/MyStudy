#include <iostream>
#include <queue>
using namespace std;

queue<int> pushStat(int speed);
int setSpeed();
int addPlayers();

class Player {				// Player Class
public:
	int speed;
	int name;
	queue<int> playerQ;

public:
	Player() : speed(setSpeed()), name(addPlayers()) {				// 생성자
		cout << name << "번째 player's speed : " << speed << endl;
		playerQ = pushStat(speed);
	}	
};

int findName(int speed, Player* player, int playerNum) {

	for (int i = 0; i < playerNum; i++) {
		if (player[i].speed == speed)
			return player[i].name;
	}
	return 0;
}

int addPlayers() {
	static int players = 1;
	return players++;
}

int setSpeed() {	// 2 ~ 11
	static vector<int> numbers;		// 배열처럼 (벡터) 접근

	int _speed = rand() % 10 + 2;
	bool isSame = false;
	while (!isSame)
	{
		int index = 0;
		for (index = 0; index < numbers.size(); index++)
		{
			if (numbers[index] == _speed)
			{
				_speed = rand() % 10 + 2;
				break;
			}
		}
		if (index == numbers.size())
		{
			isSame = true;
		}
	}
	numbers.push_back(_speed);

	return _speed;
}

void playOrder(Player* player, int playerNum);

int main() {
	srand((unsigned)time(nullptr));

	cout << "---------------------" << endl;
	cout << "-----시작합니다------" << endl;
	cout << "---------------------" << endl;

	int playerNum;
	cout << "소환할 player 수를 입력하시오 (limit : 1~6)" << endl;
	cout << "> ";
	cin >> playerNum;

	Player* player = new Player[playerNum];	// Player 생성

	cout << endl;
	playOrder(player, playerNum);
	cout << endl;

	cout << "---------------------" << endl;
	cout << "--실행을 종료합니다--" << endl;
	cout << "---------------------" << endl;

	return 0;
}

queue<int> pushStat(int speed) { //개별 플레이어 큐 생성
	queue<int> player;
	int stat = 0;

	while (stat != 20) {
		for (int i = 0; i < speed - 1; i++) {
			if (stat == 20) break;
			player.push(0);
			++stat;
		}
		if (stat == 20) break;
		player.push(speed);
		++stat;
	}
	return player;
}

void playOrder(Player* player, int playerNum) {
	priority_queue<int, vector<int>, greater<int>> orderQ;

	// player Q가 empty()가 될때까지(현재 조건) 반복 진행 
	while ((!player->playerQ.empty())) {
		for (int i = 0; i < playerNum; i++) {
			if (player[i].playerQ.front() == 0)
				player[i].playerQ.pop();

			else if (player[i].playerQ.front() != 0) {
				orderQ.push(player[i].playerQ.front());
				player[i].playerQ.pop();
			}
		}
		while (!orderQ.empty()) {
			cout << findName(orderQ.top(), player, playerNum) << "번째 player의 turn입니다. (speed : " << orderQ.top() << ")" << endl;
			orderQ.pop();
		}
	}
}