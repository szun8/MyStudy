#include <iostream>
#include <vector>
#include <queue>
using namespace std;

// BFS : Breadth First Search 너비우선탐색(길찾기) - 예약시스템(queue)
struct Vertex {
    int data;
};
vector<Vertex> vertices;    
vector<vector<int> > adjacent;
vector<bool> discovered;    // 발견여부

void CreateGraph() {
    vertices.resize(6);
    adjacent = vector<vector<int> >(6);

    // 인접 리스트 version
    adjacent[0].push_back(1);
    adjacent[0].push_back(3);
    adjacent[1].push_back(0);
    adjacent[1].push_back(2);
    adjacent[1].push_back(3);
    adjacent[3].push_back(4);
    adjacent[5].push_back(4);

    // 인접 행렬 version
    // vector<int> thing1 ;
    // thing1.push_back(0); thing1.push_back(1); thing1.push_back(0); thing1.push_back(1); thing1.push_back(0); thing1.push_back(0);
    // adjacent.push_back(thing1);
    
    // vector<int> thing2 ;
    // thing2.push_back(1); thing2.push_back(0); thing2.push_back(1); thing2.push_back(1); thing2.push_back(0); thing2.push_back(0);
    // adjacent.push_back(thing2);

    // vector<int> thing3 ;
    // thing3.push_back(0); thing3.push_back(0); thing3.push_back(0); thing3.push_back(0); thing3.push_back(0); thing3.push_back(0);
    // adjacent.push_back(thing3);

    // vector<int> thing4 ;
    // thing4.push_back(0); thing4.push_back(0); thing4.push_back(0); thing4.push_back(0); thing4.push_back(1); thing4.push_back(0);
    // adjacent.push_back(thing4);

    // vector<int> thing5 ;
    // thing5.push_back(0); thing5.push_back(0); thing5.push_back(0); thing5.push_back(0); thing5.push_back(0); thing5.push_back(0);
    // adjacent.push_back(thing5);

    // vector<int> thing6 ;
    // thing6.push_back(0); thing6.push_back(0); thing6.push_back(0); thing6.push_back(0); thing6.push_back(1); thing6.push_back(0);
    // adjacent.push_back(thing6);
}

void BFS(int here){
    // 누구에 의해 발견되었는지?
    vector<int> parent(6, -1);
    // 시작점에서 얼만큼 떨어져 있는지?
    vector<int> distance(6, -1);
    queue<int> q;
    q.push(here);
    discovered[here] = true;
    parent[here] = here;
    distance[here] = 0;

    while (!q.empty()) {
        here = q.front(); q.pop();
        for (int there = 0; there < adjacent[here].size(); there++){
            // 인접행렬 -> if(adjacent[here][there]==0) continue;
            if(discovered[there]) continue;

            q.push(there);
            discovered[there] = true;

            parent[there] = here;
            distance[there] = distance[here]+1;
        }
    }   
}

void BFSAll(){
    for (int i = 0; i < adjacent.size(); i++) {
        if(discovered[i]==false) BFS(i);
    }
}

int main(){
    CreateGraph();
    discovered.resize(6, false);
    BFSAll();
    return 0;
}