#include <iostream>
#include <vector>
#include <list>
#include <queue>
using namespace std;

// BFS : Breadth First Search 너비우선탐색(길찾기) - 예약시스템(queue)
struct Vertex {
    int data;
};
vector<Vertex> vertices;    
vector<vector<int> > adjacent;  // 인접행렬

void CreateGraph() {
    vertices.resize(6);
    adjacent = vector<vector<int> >(6, vector<int>(6,-1));

    // 인접 행렬 version - 연결된 정점에 대한 가중치를 저장
    adjacent[0][1] = 15;
    adjacent[0][3] = 35;
    adjacent[1][0] = 15;
    adjacent[1][2] = 5;
    adjacent[1][3] = 10;
    adjacent[3][4] = 5;
    adjacent[5][4] = 5;

    // 인접 리스트 version
    // adjacent[0].push_back(1);
    // adjacent[0].push_back(3);
    // adjacent[1].push_back(0);
    // adjacent[1].push_back(2);
    // adjacent[1].push_back(3);
    // adjacent[3].push_back(4);
    // adjacent[5].push_back(4);
}

struct  VertexCost{
    int cost;
    int vertex;
};
void Dijikstra(int here){
    list<VertexCost> discovered;    // 발견목록, BFS에서는 먼저 찾은 정점을 먼저 꺼내 사용했는데 지금은 그래도 되지 않기떄문에
    vector<int> best(6, INT32_MAX); // 각 정점별로 지금까지 발견한 최소 거리
    vector<int> parent(6, -1);       // 난 누구에 의해 발견되었는가?
    VertexCost thing;
    thing.cost=0; thing.vertex=here;
    discovered.push_back(thing);
    best[here] = 0;
    parent[here] = here;
    while (!discovered.empty()) {
        // 제일 좋은 후보를 찾는다 (best Case)
        list<VertexCost>::iterator bestIt;
        int bestCost = INT32_MAX;   // 일단 시작값은 어마하게 큰 값을 설정
        for (list<VertexCost>::iterator it = discovered.begin(); it != discovered.end(); it++){
            if(bestCost > it->cost){
                bestCost = it->cost;
                bestIt = it;
            }
        }
        int cost = bestIt->cost;
        here = bestIt->vertex;
        discovered.erase(bestIt);   // list라 erase의 시간이 보다 느리지 않음
        // 방문? 더 짧은 경로를 뒤늦게 찾았다면 스킵.
        if(best[here] < cost) continue;

        // 방문!
        for (int there = 0; there < adjacent.size(); there++){
            // 연결되지 않았으면 스킵.
            if(adjacent[here][there] == -1) continue;
            // 더 좋은 경로를 과거에 찾았다면 스킵.
            int nextCost = best[here] + adjacent[here][there];
            if(nextCost > best[there]) continue;

            best[there] = nextCost; // 제일 좋은 경우로 갱신
            parent[there] = here;   // 현재 there은 here에 의해 찾아졌다
            thing.vertex = there; thing.cost = nextCost;
            discovered.push_back(thing);
        }
    }
    int a = 3;
}

int main(){
    CreateGraph();
    Dijikstra(0);
    return 0;
}